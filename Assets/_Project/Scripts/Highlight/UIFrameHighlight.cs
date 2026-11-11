using UnityEngine;

namespace VRTraining.Highlight
{
    // подсветка для кнопки UI, просто включает и выключает рамку вокруг нее
    public class UIFrameHighlight : MonoBehaviour, IHighlight
    {
        [SerializeField] private GameObject frame;

        private void Awake()
        {
            frame.SetActive(false);
        }

        public void Show()
        {
            frame.SetActive(true);
        }

        public void Hide()
        {
            frame.SetActive(false);
        }
    }
}
