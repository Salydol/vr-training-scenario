using UnityEngine;
using VRTraining.Scenario.Data;

namespace VRTraining.UI
{
    // как статус шага выглядит в UI: подпись и цвет
    // лежит в одном месте чтобы панель шагов и итоги показывали одинаково
    public static class StepStatusStyle
    {
        public static string GetLabel(StepStatus status)
        {
            switch (status)
            {
                case StepStatus.Active: return "выполняется";
                case StepStatus.Success: return "выполнен";
                case StepStatus.Failed: return "с ошибкой";
                case StepStatus.Skipped: return "пропущен";
                default: return "ожидает";
            }
        }

        public static Color GetColor(StepStatus status)
        {
            switch (status)
            {
                case StepStatus.Active: return new Color(1f, 0.8f, 0.2f);
                case StepStatus.Success: return new Color(0.3f, 0.85f, 0.4f);
                case StepStatus.Failed: return new Color(0.95f, 0.3f, 0.3f);
                case StepStatus.Skipped: return new Color(0.6f, 0.6f, 0.65f);
                default: return new Color(0.8f, 0.8f, 0.85f);
            }
        }
    }
}
