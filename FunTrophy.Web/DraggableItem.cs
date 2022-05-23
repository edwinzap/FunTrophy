namespace FunTrophy.Web
{
    public class DraggableItem<T>
    {
        public bool IsDragOver { get; set; }
        public T Item { get; set; }
        public int Order { get; set; }
    }
}