using System.Collections.Generic;
using UnityEngine;

namespace VRTraining.Scenario.Data
{
    // весь сценарий лежит в одном ассете
    // чтобы сделать новый сценарий код трогать не надо, просто создаем еще один ассет
    [CreateAssetMenu(fileName = "Scenario", menuName = "VRTraining/Scenario")]
    public class ScenarioData : ScriptableObject
    {
        public string title;
        public List<StepGroupData> groups = new List<StepGroupData>();
    }
}
