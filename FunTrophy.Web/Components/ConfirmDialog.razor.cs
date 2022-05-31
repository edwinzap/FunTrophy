using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Components
{
    public partial class ConfirmDialog
    {
        protected bool ShowDialog { get; set; }

        [Parameter]
        public string Title { get; set; } = "Confirmer la suppression";

        [Parameter]
        public string Message { get; set; } = "Es-tu sûr de vouloir supprimer l'élément ?";

        public void Show()
        {
            ShowDialog = true;
            StateHasChanged();
        }

        public void Show(string message)
        {
            Message = message;
            Show();
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowDialog = false;
            await ConfirmationChanged.InvokeAsync(value);
        }
    }
}