namespace VRTraining.Scenario.Data
{
    // что игрок вообще может сделать в сценарии
    public enum ActionType
    {
        ReachPoint,   // дойти до точки
        Grab,         // взять объект в руку
        PointerClick, // кликнуть лучом по объекту
        UIButton      // нажать кнопку на UI
    }
}
