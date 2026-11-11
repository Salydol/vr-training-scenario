namespace VRTraining.Scenario.Data
{
    public enum StepStatus
    {
        Pending, // еще не дошли до шага
        Active,  // шаг выполняется сейчас
        Success, // сделал без ошибок
        Failed,  // сделал с ошибкой
        Skipped  // пропущен, группу закрыли из за нарушения порядка
    }
}
