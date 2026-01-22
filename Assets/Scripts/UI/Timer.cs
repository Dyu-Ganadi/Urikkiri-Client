using System;
using System.Collections;
using Network;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class Timer : MonoBehaviour, ICallHandler
    {
        public float currentTime;
        public float maxTime;
        public Image timeBar;
        public TextMeshProUGUI timeText;
        public Animator animator;
        public UnityEvent onTimeOver;

        private Coroutine _timer;

        private void OnEnable()
        {
            animator.enabled = true;
        }

        private void Update()
        {
            timeText.text = $"{currentTime:F1}s";
            timeBar.fillAmount = currentTime / maxTime;
        }

        private void ActiveTimer(float time)
        {
            animator.enabled = false;
            currentTime = maxTime = time;
            if (_timer != null) StopCoroutine(_timer);
            _timer = StartCoroutine(TimerFlow());
        }

        private IEnumerator TimerFlow()
        {
            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                yield return null;
            }
            onTimeOver.Invoke();
            API.SubmitCard(null);
            _timer = null;
            gameObject.SetActive(false);
        }

        public void HandleEvent(string eventName)
        {
            var args = eventName.Split("_");
            switch (args[0])
            {
                case "at":
                    if (float.TryParse(args[1], out var time)) ActiveTimer(time);
                    break;
            }
        }
    }
}
