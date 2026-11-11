using UnityEngine;
using VRTraining.Core.Events;
using VRTraining.Scenario.Data;
using VRTraining.Scenario.Events;
using VRTraining.Scenario.Runtime;

namespace VRTraining.Scenario
{
    // висит на сцене тренировки, запускает сценарий и передает в него действия игрока
    public class ScenarioController : MonoBehaviour
    {
        [SerializeField] private ScenarioData scenario;

        private ScenarioRunner runner;

        private void OnEnable()
        {
            EventBus<PlayerActionEvent>.Subscribe(OnPlayerAction);
        }

        private void OnDisable()
        {
            EventBus<PlayerActionEvent>.Unsubscribe(OnPlayerAction);
        }

        // в Start а не в Awake, чтобы UI и звук успели подписаться
        private void Start()
        {
            runner = new ScenarioRunner(scenario);
            runner.Start();
        }

        private void OnPlayerAction(PlayerActionEvent playerAction)
        {
            runner?.HandleAction(playerAction.Type, playerAction.TargetId);
        }
    }
}
