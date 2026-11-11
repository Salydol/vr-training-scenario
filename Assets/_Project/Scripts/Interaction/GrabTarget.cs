using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using VRTraining.Scenario.Data;

namespace VRTraining.Interaction
{
    // объект который надо взять в руку
    [RequireComponent(typeof(XRGrabInteractable))]
    public class GrabTarget : ScenarioTarget
    {
        private XRGrabInteractable grabInteractable;

        public override ActionType ActionType => ActionType.Grab;

        private void Awake()
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }

        private void OnEnable()
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }

        private void OnDisable()
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }

        private void OnGrabbed(SelectEnterEventArgs args)
        {
            ReportAction();
        }
    }
}
