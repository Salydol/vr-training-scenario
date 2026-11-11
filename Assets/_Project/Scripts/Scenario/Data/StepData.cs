using System;
using System.Collections.Generic;

namespace VRTraining.Scenario.Data
{
    // описание шага, в шаге может быть одно или несколько действий подряд
    [Serializable]
    public class StepData
    {
        public string description;
        public List<ActionData> actions = new List<ActionData>();
    }
}
