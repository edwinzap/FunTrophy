using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Components
{
    public partial class Confirm
    {
        protected bool ShowConfirmation { get; set; }

        [Parameter]
        public string ConfirmationTitle { get; set; } = "Confirmer la suppression";

        [Parameter]
        public string ConfirmationMessage { get; set; } = "Es-tu sûr de vouloir supprimer l'élément ?";

        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        public void Show(string message)
        {
            ConfirmationMessage = message;
            Show();
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }
    }
}