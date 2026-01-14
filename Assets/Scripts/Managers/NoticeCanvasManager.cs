using TMPro;
using UnityEngine;

namespace Managers
{
    public class NoticeCanvasManager : MonoBehaviour
    {
        private bool _isExaminer;
        public TextMeshProUGUI noticeText;
        public TextMeshProUGUI descriptionText;
        
        private void OnEnable()
        {
            _isExaminer = GameStatics.IsExaminer();
            noticeText.text = _isExaminer ? "당신은 <color #FF9B00>출제자</color> 입니다!" : "당신은 <color #FF9B00>플레이어</color> 입니다!";
            descriptionText.text = _isExaminer ? "문제에 가장 잘 맞고 마음에 쏙 드는 카드를 고르세요" : "문제에 맞게 카드를 고르세요";
        }
    }
}
