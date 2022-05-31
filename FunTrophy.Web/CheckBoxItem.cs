namespace FunTrophy.Web
{
    public class CheckBoxItem<T>
    {
        public bool IsChecked { get; set; }

        public T Value { get; set; }

        public CheckBoxItem(bool isChecked, T value)
        {
            IsChecked = isChecked;
            Value = value;
        }
    }
}
