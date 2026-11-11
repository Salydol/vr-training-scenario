using System;

namespace VRTraining.Scenario.Data
{
    // одно действие которое ждем от игрока
    [Serializable]
    public class ActionData
    {
        public ActionType type;
        // id объекта на сцене, такой же id стоит на самом объекте
        public string targetId;

        public bool Matches(ActionType otherType, string otherTargetId)
        {
            return type == otherType && targetId == otherTargetId;
        }
    }
}
