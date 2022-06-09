namespace FunTrophy.Web.Models
{
    public class DropDownMenuItem
    {
        public DropDownMenuItem(string title, string link, bool isVisible = true)
        {
            Title = title;
            Link = link;
            IsVisible = isVisible;
        }

        public string Title { get; set; }
        public string Link { get; set; }
        public bool IsVisible { get; set; }
    }
}