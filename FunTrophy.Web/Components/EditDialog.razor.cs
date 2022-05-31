using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Components
{
    public partial class EditDialog
    {
        protected bool ShowDialog { get; set; }

        [Parameter]
        public bool ShowCancelButton { get; set; } = true;

        [Parameter]
        public string OkButtonText { get; set; } = "OK";

        [Parameter]
        public string CancelButtonText { get; set; } = "Annuler";

        [Parameter]
        public string Title { get; set; } = "Editer l'élément";

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public void Show()
        {
            ShowDialog = true;
            StateHasChanged();
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