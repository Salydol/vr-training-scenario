using System.Collections.Generic;
using VRTraining.Core.Events;
using VRTraining.Scenario.Data;
using VRTraining.Scenario.Events;

namespace VRTraining.Scenario.Runtime
{
    // вся логика прохождения сценария
    // обычный класс без MonoBehaviour, про сцену и UI ничего не знает
    // получает действие игрока, решает правильно или нет и кидает события
    public class ScenarioRunner
    {
        private readonly List<StepGroup> groups = new List<StepGroup>();
        private int groupIndex = -1;

        public bool IsFinished => groupIndex >= groups.Count;
        private StepGroup CurrentGroup => groups[groupIndex];

        public ScenarioRunner(ScenarioData data)
        {
            foreach (StepGroupData groupData in data.groups)
                groups.Add(new StepGroup(groupData));
        }

        public void Start()
        {
            StartNextGroup();
        }

        public void HandleAction(ActionType type, string targetId)
        {
            if (groupIndex < 0 || IsFinished)
                return;

            StepGroup group = CurrentGroup;
            Step step = group.CurrentStep;

            // сделал то что ждали
            if (step.IsExpected(type, targetId))
            {
                step.CompleteAction();

                if (step.AllActionsDone)
                    FinishStep(step, StepStatus.Success);
                else
                    RaiseExpectedAction(step.ExpectedAction);

                return;
            }

            // полез в шаг который должен быть позже, это нарушение порядка
            if (group.IsActionFromLaterStep(type, targetId))
            {
                group.SkipUnfinishedSteps();
                FinishGroup(true);
                return;
            }

            // просто не то действие, шаг закрываем с ошибкой
            FinishStep(step, StepStatus.Failed);
        }

        private void StartNextGroup()
        {
            groupIndex++;

            if (IsFinished)
            {
                RaiseExpectedAction(null);
                EventBus<ScenarioFinishedEvent>.Raise(new ScenarioFinishedEvent { Groups = groups });
                return;
            }

            EventBus<GroupStartedEvent>.Raise(new GroupStartedEvent
            {
                Group = CurrentGroup,
                Index = groupIndex,
                Total = groups.Count
            });

            StartCurrentStep();
        }

        private void StartCurrentStep()
        {
            Step step = CurrentGroup.CurrentStep;
            step.Activate();

            EventBus<StepStartedEvent>.Raise(new StepStartedEvent { Step = step });
            RaiseExpectedAction(step.ExpectedAction);
        }

        private void FinishStep(Step step, StepStatus status)
        {
            step.Finish(status);
            EventBus<StepFinishedEvent>.Raise(new StepFinishedEvent { Step = step });

            CurrentGroup.MoveToNextStep();

            if (CurrentGroup.IsFinished)
                FinishGroup(false);
            else
                StartCurrentStep();
        }

        private void FinishGroup(bool sequenceBroken)
        {
            EventBus<GroupFinishedEvent>.Raise(new GroupFinishedEvent
            {
                Group = CurrentGroup,
                SequenceBroken = sequenceBroken
            });

            StartNextGroup();
        }

        private void RaiseExpectedAction(ActionData action)
        {
            EventBus<ExpectedActionChangedEvent>.Raise(new ExpectedActionChangedEvent { Action = action });
        }
    }
}
