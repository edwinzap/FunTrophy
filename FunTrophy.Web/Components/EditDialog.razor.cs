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

        [Parameter]
        public ElementReference? FocusOn { get; set; }

        public async Task ShowAsync()
        {
            ShowDialog = true;
            StateHasChanged();
            if (FocusOn.HasValue)
                await FocusOn.Value.FocusAsync();
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            await ConfirmationChanged.InvokeAsync(value);
            ShowDialog = false;
        }

        private async Task OnKeyUp(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case "Enter":
                    await OnConfirmationChange(true);
                    break;

                case "Escape":
                    await OnConfirmationChange(false);
                    break;
            }
        }
    }
}