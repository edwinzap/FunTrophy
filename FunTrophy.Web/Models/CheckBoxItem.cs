namespace FunTrophy.Web.Models
{
    public class CheckBoxItem<T>
    {
        public bool IsChecked { get; set; }

        public string Label { get; set; }

        public T Value { get; set; }

        public CheckBoxItem(bool isChecked, T value, string label)
        {
            IsChecked = isChecked;
            Value = value;
            Label = label;
        }
    }
}
