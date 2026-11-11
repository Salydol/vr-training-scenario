using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRTraining.Scenario.Runtime;

namespace VRTraining.UI
{
    // одна строка в списке шагов: кружок статуса, описание и подпись статуса
    public class StepRowView : MonoBehaviour
    {
        [SerializeField] private Image statusIcon;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text statusText;

        private Step step;

        public void Bind(Step stepToShow, int number)
        {
            step = stepToShow;
            descriptionText.text = number + ". " + step.Description;
            Refresh();
        }

        // вызываем когда статус шага мог поменяться
        public void Refresh()
        {
            Color color = StepStatusStyle.GetColor(step.Status);

            statusIcon.color = color;
            statusText.color = color;
            statusText.text = StepStatusStyle.GetLabel(step.Status);
        }
    }
}
