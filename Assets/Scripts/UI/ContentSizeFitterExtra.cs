using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent( typeof( LayoutElement ), typeof( ContentSizeFitter ))]
    [ExecuteAlways]
    public class ContentSizeFitterExtra : MonoBehaviour
    {
        public LayoutElement layoutElement;
        public TextMeshProUGUI text;
        private void Update()
        {
            layoutElement.enabled = layoutElement.preferredWidth <= text.preferredWidth;
        }
    }
}
