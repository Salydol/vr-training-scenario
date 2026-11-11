using VRTraining.Core.Events;

namespace VRTraining.SceneManagement
{
    // кто то просит загрузить сцену, например кнопка в меню
    public struct LoadSceneRequestEvent : IGameEvent
    {
        public string SceneName;
    }
}
