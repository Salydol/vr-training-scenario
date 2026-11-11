using System.Collections.Generic;
using VRTraining.Core.Events;
using VRTraining.Scenario.Data;
using VRTraining.Scenario.Runtime;

namespace VRTraining.Scenario.Events
{
    // игрок что то сделал: дошел, взял, кликнул или нажал кнопку
    public struct PlayerActionEvent : IGameEvent
    {
        public ActionType Type;
        public string TargetId;
    }

    public struct GroupStartedEvent : IGameEvent
    {
        public StepGroup Group;
        public int Index;
        public int Total;
    }

    // группа закончилась, SequenceBroken значит закрыли из за нарушения порядка
    public struct GroupFinishedEvent : IGameEvent
    {
        public StepGroup Group;
        public bool SequenceBroken;
    }

    public struct StepStartedEvent : IGameEvent
    {
        public Step Step;
    }

    public struct StepFinishedEvent : IGameEvent
    {
        public Step Step;
    }

    // какое действие сейчас ждем, по нему включается подсветка
    // если Action пустой значит ничего не ждем
    public struct ExpectedActionChangedEvent : IGameEvent
    {
        public ActionData Action;
    }

    public struct ScenarioFinishedEvent : IGameEvent
    {
        public IReadOnlyList<StepGroup> Groups;
    }
}
