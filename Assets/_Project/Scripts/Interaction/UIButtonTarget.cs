using UnityEngine;
using UnityEngine.UI;
using VRTraining.Scenario.Data;

namespace VRTraining.Interaction
{
    // кнопка UI которая участвует в сценарии
    [RequireComponent(typeof(Button))]
    public class UIButtonTarget : ScenarioTarget
    {
        private Button button;

        public override ActionType ActionType => ActionType.UIButton;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(ReportAction);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(ReportAction);
        }
    }
}
