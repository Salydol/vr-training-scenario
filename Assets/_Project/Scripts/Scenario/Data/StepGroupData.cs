using System;
using System.Collections.Generic;

namespace VRTraining.Scenario.Data
{
    // группа шагов, связанных по смыслу
    [Serializable]
    public class StepGroupData
    {
        public string title;
        public List<StepData> steps = new List<StepData>();
    }
}
