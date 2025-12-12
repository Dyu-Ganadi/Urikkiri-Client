using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public class Networking : MonoBehaviour
    {
        private const string BaseUrl = "https://urikkiri-be.thinkinggms.com/";
        private static Networking _networking;
        [SerializeField] private WebSocketClient websocketClient;
        [CanBeNull] public static string AccessToken;

        private string _password;

        private void Awake()
        {
            Application.runInBackground = true;

            // _baseUrl = baseUrl;
            if (_networking != null)
                Destroy(_networking);
            _networking = this;
        }

        public void SetAccessToken(string accessToken)
        {
            AccessToken = accessToken;
            Debug.Log($"토큰 정상 수신함: {accessToken}");
            websocketClient.ConnectOn();
        }

        public abstract class Request<T> where T : class
        {
            private readonly Dictionary<string, string> _headers;
            private readonly LinkedList<string> _params;

            private readonly string _path;

            [CanBeNull] private Action<ErrorBody> _errorAction;
            [CanBeNull] private Action<T> _responseAction;
            [CanBeNull] private Action _successAction;

            protected Request(string path)
            {
                _params = new LinkedList<string>();
                _headers = new Dictionary<string, string>();
                _path = path;
            }

            public Request<T> AddParam(string key, string value)
            {
                _params.AddLast($"{key}={HttpUtility.UrlEncode(value)}");
                return this;
            }

            public Request<T> AddHeader(string key, string value)
            {
                _headers.Add(key, value);
                return this;
            }

            public Request<T> OnError(Action<ErrorBody> action)
            {
                _errorAction = action;
                return this;
            }

            public Request<T> OnSuccess(Action action)
            {
                _successAction = action;
                return this;
            }

            public Request<T> OnResponse(Action<T> action)
            {
                _responseAction += action;
                return this;
            }

            protected abstract UnityWebRequest WebRequest(string url);

            private IEnumerator _Request(string url)
            {
                using var webRequest = WebRequest(url);
                webRequest.timeout = 15;
                foreach (var (key, value) in _headers)
                    webRequest.SetRequestHeader(key, value);
                if (AccessToken != null) webRequest.SetRequestHeader("Authorization", AccessToken);
                yield return webRequest.SendWebRequest();

                if (webRequest.downloadHandler == null) webRequest.downloadHandler = new DownloadHandlerBuffer();
                var bodyText = webRequest.downloadHandler.text;
                if (webRequest.result == UnityWebRequest.Result.Success)
                    if (webRequest.responseCode is >= 200 and <= 299)
                    {
                        if (typeof(T) == typeof(void))
                            _responseAction?.Invoke(null);
                        else if (typeof(T) == typeof(string))
                            _responseAction?.Invoke(bodyText as T);
                        else
                            _responseAction?.Invoke(JsonConvert.DeserializeObject<T>(bodyText));
                        _successAction?.Invoke();

                        yield break;
                    }

                _errorAction?.Invoke(JsonConvert.DeserializeObject<ErrorBody>(bodyText));
            }

            public void Build()
            {
                var parameters = _params.Count > 0 ? $"?{string.Join("&", _params)}" : "";
                _networking.StartCoroutine(_Request(BaseUrl + _path + parameters));
            }
        }

        public class Get<T> : Request<T> where T : class
        {
            public Get(string path) : base(path)
            {
            }

            protected override UnityWebRequest WebRequest(string url)
            {
                return UnityWebRequest.Get(url);
            }
        }

        public class Post<T> : Request<T> where T : class
        {
            private readonly string _body;

            public Post(string path, object body) : base(path)
            {
                _body = JsonUtility.ToJson(body);
            }

            protected override UnityWebRequest WebRequest(string url)
            {
                return UnityWebRequest.Post(url, _body, "application/json");
            }
        }

        public class Put<T> : Request<T> where T : class
        {
            private readonly string _body;

            public Put(string path, object body) : base(path)
            {
                _body = JsonUtility.ToJson(body);
            }

            protected override UnityWebRequest WebRequest(string url)
            {
                var unityWebRequest = UnityWebRequest.Put(url, _body);
                unityWebRequest.SetRequestHeader("Content-Type", "application/json");
                return unityWebRequest;
            }
        }

        public class Delete<T> : Request<T> where T : class
        {
            public Delete(string path) : base(path)
            {
            }

            protected override UnityWebRequest WebRequest(string url)
            {
                return new UnityWebRequest(url, "DELETE", new DownloadHandlerBuffer(), null);
            }
        }
    }
}