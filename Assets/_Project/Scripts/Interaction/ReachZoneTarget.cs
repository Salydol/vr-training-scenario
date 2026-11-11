using UnityEngine;
using VRTraining.Scenario.Data;

namespace VRTraining.Interaction
{
    // точка интереса, срабатывает когда игрок зашел в круг
    // считаю по расстоянию до головы, так работает и при телепорте и при ходьбе
    public class ReachZoneTarget : ScenarioTarget
    {
        [SerializeField] private float radius = 0.6f;

        private Transform playerHead;
        private bool playerInside;

        public override ActionType ActionType => ActionType.ReachPoint;

        private void Start()
        {
            if (Camera.main != null)
                playerHead = Camera.main.transform;
        }

        private void Update()
        {
            if (playerHead == null)
                return;

            bool insideNow = IsPlayerInside();

            // сообщаем только в момент входа, а не каждый кадр
            if (insideNow && !playerInside)
                ReportAction();

            playerInside = insideNow;
        }

        private bool IsPlayerInside()
        {
            // высоту не учитываем, только положение на полу
            Vector3 offset = playerHead.position - transform.position;
            offset.y = 0f;

            return offset.sqrMagnitude <= radius * radius;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
