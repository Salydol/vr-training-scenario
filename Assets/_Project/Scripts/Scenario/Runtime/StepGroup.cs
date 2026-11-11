using System.Collections.Generic;
using VRTraining.Scenario.Data;

namespace VRTraining.Scenario.Runtime
{
    // группа шагов во время прохождения
    public class StepGroup
    {
        private readonly List<Step> steps = new List<Step>();
        private int stepIndex;

        public string Title { get; }
        public IReadOnlyList<Step> Steps => steps;
        public bool IsFinished => stepIndex >= steps.Count;
        public Step CurrentStep => IsFinished ? null : steps[stepIndex];

        public StepGroup(StepGroupData data)
        {
            Title = data.title;

            foreach (StepData stepData in data.steps)
                steps.Add(new Step(stepData));
        }

        public void MoveToNextStep()
        {
            stepIndex++;
        }

        // игрок сделал действие из шага который идет позже текущего
        public bool IsActionFromLaterStep(ActionType type, string targetId)
        {
            for (int i = stepIndex + 1; i < steps.Count; i++)
            {
                if (steps[i].Contains(type, targetId))
                    return true;
            }

            return false;
        }

        // все что не успели закончить помечаем пропущенным и закрываем группу
        public void SkipUnfinishedSteps()
        {
            foreach (Step step in steps)
            {
                if (!step.IsFinished)
                    step.Finish(StepStatus.Skipped);
            }

            stepIndex = steps.Count;
        }
    }
}
