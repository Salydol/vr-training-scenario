using TMPro;
using UnityEngine;
using VRTraining.Core.Events;
using VRTraining.Scenario.Events;
using VRTraining.Scenario.Runtime;

namespace VRTraining.UI
{
    // итоги в конце сценария: все группы, все шаги и их статусы
    public class ResultsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private Transform rowsContainer;
        [SerializeField] private TMP_Text groupTitlePrefab;
        [SerializeField] private StepRowView rowPrefab;
        [SerializeField] private float distanceFromPlayer = 2f;

        private void Awake()
        {
            content.SetActive(false);
        }

        private void OnEnable()
        {
            EventBus<ScenarioFinishedEvent>.Subscribe(OnScenarioFinished);
        }

        private void OnDisable()
        {
            EventBus<ScenarioFinishedEvent>.Unsubscribe(OnScenarioFinished);
        }

        private void OnScenarioFinished(ScenarioFinishedEvent scenarioFinished)
        {
            foreach (StepGroup group in scenarioFinished.Groups)
                AddGroup(group);

            PlaceInFrontOfPlayer();
            content.SetActive(true);
        }

        private void AddGroup(StepGroup group)
        {
            TMP_Text title = Instantiate(groupTitlePrefab, rowsContainer);
            title.text = group.Title;

            for (int i = 0; i < group.Steps.Count; i++)
            {
                StepRowView row = Instantiate(rowPrefab, rowsContainer);
                row.Bind(group.Steps[i], i + 1);
            }
        }

        // ставим панель перед игроком, чтобы ему не пришлось ее искать
        private void PlaceInFrontOfPlayer()
        {
            if (Camera.main == null)
                return;

            Transform head = Camera.main.transform;

            // наклон головы не учитываем, иначе панель может уехать в пол или потолок
            Vector3 forward = head.forward;
            forward.y = 0f;
            forward.Normalize();

            transform.position = head.position + forward * distanceFromPlayer;
            transform.rotation = Quaternion.LookRotation(forward);
        }
    }
}
