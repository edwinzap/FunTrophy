using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Components
{
    public partial class EditDialog
    {
        protected bool ShowConfirmation { get; set; }

        [Parameter]
        public string ConfirmationTitle { get; set; } = "Editer l'élément";

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
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