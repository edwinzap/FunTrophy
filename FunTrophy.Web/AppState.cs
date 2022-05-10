using FunTrophy.Shared.Model;

namespace FunTrophy.Web
{
    public class AppState
    {
        public bool IsRaceSelected => Race != null;

        public event Action OnChange;

        private RaceDto? _race;

        public RaceDto? Race
        {
            get { return _race; }
            set
            {
                _race = value;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}