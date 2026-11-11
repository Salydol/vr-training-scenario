using UnityEngine;
using UnityEngine.UI;
using VRTraining.Core.Events;
using VRTraining.SceneManagement;

namespace VRTraining.UI
{
    // кнопка перехода на сцену
    // одна и та же для "начать тренировку", "попытаться еще" и "в лобби", отличается только имя сцены
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private string sceneName = SceneNames.Lobby;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            EventBus<LoadSceneRequestEvent>.Raise(new LoadSceneRequestEvent { SceneName = sceneName });
        }
    }
}
