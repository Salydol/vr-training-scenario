using UnityEngine;
using UnityEngine.SceneManagement;
using VRTraining.Core.Events;

namespace VRTraining.SceneManagement
{
    // единственное место где грузятся сцены
    // кнопки сами сцены не грузят, они только кидают запрос
    public class SceneLoader : MonoBehaviour
    {
        private bool isLoading;

        private void OnEnable()
        {
            EventBus<LoadSceneRequestEvent>.Subscribe(OnLoadSceneRequest);
        }

        private void OnDisable()
        {
            EventBus<LoadSceneRequestEvent>.Unsubscribe(OnLoadSceneRequest);
        }

        private void OnLoadSceneRequest(LoadSceneRequestEvent request)
        {
            // защита от двойного клика по кнопке
            if (isLoading)
                return;

            isLoading = true;
            SceneManager.LoadSceneAsync(request.SceneName);
        }
    }
}
