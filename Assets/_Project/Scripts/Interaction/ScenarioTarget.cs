using UnityEngine;
using VRTraining.Core.Events;
using VRTraining.Scenario.Data;
using VRTraining.Scenario.Events;

namespace VRTraining.Interaction
{
    // база для всех объектов с которыми игрок что то делает по сценарию
    // сам объект не знает правильное действие или нет, он просто сообщает что оно случилось
    // чтобы добавить новый вид действия, делаем наследника и добавляем значение в ActionType
    public abstract class ScenarioTarget : MonoBehaviour
    {
        [SerializeField] private string targetId;

        public string TargetId => targetId;
        public abstract ActionType ActionType { get; }

        protected void ReportAction()
        {
            EventBus<PlayerActionEvent>.Raise(new PlayerActionEvent
            {
                Type = ActionType,
                TargetId = targetId
            });
        }
    }
}
