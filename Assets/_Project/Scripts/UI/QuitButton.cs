using UnityEngine;
using UnityEngine.UI;

namespace VRTraining.UI
{
    // кнопка выхода из приложения
    [RequireComponent(typeof(Button))]
    public class QuitButton : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(Quit);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Quit);
        }

        private void Quit()
        {
#if UNITY_EDITOR
            // в редакторе Application.Quit не работает, просто останавливаем игру
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
