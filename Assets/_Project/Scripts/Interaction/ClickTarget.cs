using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using VRTraining.Scenario.Data;

namespace VRTraining.Interaction
{
    // объект по которому надо кликнуть лучом
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class ClickTarget : ScenarioTarget
    {
        private XRSimpleInteractable interactable;

        public override ActionType ActionType => ActionType.PointerClick;

        private void Awake()
        {
            interactable = GetComponent<XRSimpleInteractable>();
        }

        private void OnEnable()
        {
            interactable.selectEntered.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            interactable.selectEntered.RemoveListener(OnClicked);
        }

        private void OnClicked(SelectEnterEventArgs args)
        {
            ReportAction();
        }
    }
}
