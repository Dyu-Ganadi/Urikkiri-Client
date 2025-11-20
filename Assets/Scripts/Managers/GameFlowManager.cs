using UnityEngine;
using Utils;

namespace Managers
{
    public class GameFlowManager : MonoBehaviour, ICallHandler
    {
        public GameObject noticeCanvas;
        public GameObject gameCanvas;
        public GameObject resultCanvas;

        private void LoadGame()
        {
            noticeCanvas.SetActive(false);
            gameCanvas.SetActive(true);
        }

        public void HandleEvent(string eventName)
        {
            switch (eventName)
            {
                case "game_load": LoadGame();
                    break;
            }
        }
    }
}
