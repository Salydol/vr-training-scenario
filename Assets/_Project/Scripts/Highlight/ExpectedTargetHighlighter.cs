using UnityEngine;
using VRTraining.Core.Events;
using VRTraining.Interaction;
using VRTraining.Scenario.Events;

namespace VRTraining.Highlight
{
    // подсказка игроку: подсвечивает объект если сейчас по сценарию ждем действие именно с ним
    // какая именно подсветка ему не важно, берет любую через IHighlight
    [RequireComponent(typeof(ScenarioTarget))]
    public class ExpectedTargetHighlighter : MonoBehaviour
    {
        private ScenarioTarget target;
        private IHighlight highlight;

        private void Awake()
        {
            target = GetComponent<ScenarioTarget>();
            highlight = GetComponent<IHighlight>();
        }

        private void OnEnable()
        {
            EventBus<ExpectedActionChangedEvent>.Subscribe(OnExpectedActionChanged);
        }

        private void OnDisable()
        {
            EventBus<ExpectedActionChangedEvent>.Unsubscribe(OnExpectedActionChanged);
        }

        private void OnExpectedActionChanged(ExpectedActionChangedEvent expected)
        {
            if (highlight == null)
                return;

            bool isMine = expected.Action != null
                          && expected.Action.Matches(target.ActionType, target.TargetId);

            if (isMine)
                highlight.Show();
            else
                highlight.Hide();
        }
    }
}
