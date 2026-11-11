using VRTraining.Scenario.Data;

namespace VRTraining.Scenario.Runtime
{
    // шаг во время прохождения, хранит статус и на каком действии сейчас игрок
    public class Step
    {
        private readonly StepData data;
        private int actionIndex;

        public string Description => data.description;
        public StepStatus Status { get; private set; } = StepStatus.Pending;
        public bool IsFinished => Status == StepStatus.Success || Status == StepStatus.Failed || Status == StepStatus.Skipped;
        public bool AllActionsDone => actionIndex >= data.actions.Count;
        public ActionData ExpectedAction => AllActionsDone ? null : data.actions[actionIndex];

        public Step(StepData data)
        {
            this.data = data;
        }

        public void Activate()
        {
            Status = StepStatus.Active;
        }

        public void Finish(StepStatus status)
        {
            Status = status;
        }

        // это то действие которое мы сейчас ждем
        public bool IsExpected(ActionType type, string targetId)
        {
            return !AllActionsDone && ExpectedAction.Matches(type, targetId);
        }

        // есть ли такое действие вообще в этом шаге, нужно чтобы поймать нарушение порядка
        public bool Contains(ActionType type, string targetId)
        {
            foreach (ActionData action in data.actions)
            {
                if (action.Matches(type, targetId))
                    return true;
            }

            return false;
        }

        public void CompleteAction()
        {
            actionIndex++;
        }
    }
}
