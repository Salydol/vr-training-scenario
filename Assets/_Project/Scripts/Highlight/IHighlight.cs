namespace VRTraining.Highlight
{
    // общий интерфейс подсветки
    // для 3д объектов это контур, для кнопок UI рамка, можно дописать любую другую
    public interface IHighlight
    {
        void Show();
        void Hide();
    }
}
