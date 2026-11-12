using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VRTraining.Core.Events;
using VRTraining.Scenario.Events;
using VRTraining.Scenario.Runtime;

namespace VRTraining.UI
{
    // табло текущей группы: название, подсказка про порядок и список шагов со статусами
    public class ScenarioPanel : MonoBehaviour
    {
        private const string OrderHint = "Выполняйте шаги по порядку сверху вниз";
        private const string SequenceBrokenMessage = "Нарушен порядок! Группа «{0}» закрыта";
        private const string FinishedMessage = "Сценарий завершён, панель с результатами рядом с вами";

        [SerializeField] private TMP_Text groupTitleText;
        [SerializeField] private TMP_Text progressText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Transform rowsContainer;
        [SerializeField] private StepRowView rowPrefab;

        private readonly List<StepRowView> rows = new List<StepRowView>();

        private void OnEnable()
        {
            EventBus<GroupStartedEvent>.Subscribe(OnGroupStarted);
            EventBus<GroupFinishedEvent>.Subscribe(OnGroupFinished);
            EventBus<StepStartedEvent>.Subscribe(OnStepStarted);
            EventBus<StepFinishedEvent>.Subscribe(OnStepFinished);
            EventBus<ScenarioFinishedEvent>.Subscribe(OnScenarioFinished);
        }

        private void OnDisable()
        {
            EventBus<GroupStartedEvent>.Unsubscribe(OnGroupStarted);
            EventBus<GroupFinishedEvent>.Unsubscribe(OnGroupFinished);
            EventBus<StepStartedEvent>.Unsubscribe(OnStepStarted);
            EventBus<StepFinishedEvent>.Unsubscribe(OnStepFinished);
            EventBus<ScenarioFinishedEvent>.Unsubscribe(OnScenarioFinished);
        }

        private void OnGroupStarted(GroupStartedEvent groupStarted)
        {
            groupTitleText.text = groupStarted.Group.Title;
            progressText.text = "Группа " + (groupStarted.Index + 1) + " из " + groupStarted.Total;

            // если прошлую группу закрыли за нарушение порядка, сообщение об этом оставляем
            if (!messageText.text.StartsWith("Нарушен"))
                messageText.text = OrderHint;

            BuildRows(groupStarted.Group);
        }

        private void OnGroupFinished(GroupFinishedEvent groupFinished)
        {
            if (groupFinished.SequenceBroken)
                messageText.text = string.Format(SequenceBrokenMessage, groupFinished.Group.Title);
        }

        private void OnStepStarted(StepStartedEvent stepStarted)
        {
            RefreshRows();
        }

        private void OnStepFinished(StepFinishedEvent stepFinished)
        {
            messageText.text = OrderHint;
            RefreshRows();
        }

        private void OnScenarioFinished(ScenarioFinishedEvent scenarioFinished)
        {
            messageText.text = FinishedMessage;
        }

        // строки создаются под количество шагов, так что в группе их может быть не только три
        private void BuildRows(StepGroup group)
        {
            foreach (StepRowView row in rows)
                Destroy(row.gameObject);

            rows.Clear();

            for (int i = 0; i < group.Steps.Count; i++)
            {
                StepRowView row = Instantiate(rowPrefab, rowsContainer);
                row.Bind(group.Steps[i], i + 1);
                rows.Add(row);
            }
        }

        private void RefreshRows()
        {
            foreach (StepRowView row in rows)
                row.Refresh();
        }
    }
}
