using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FunTrophy.Web.Components
{
    public partial class EditDialog
    {
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

        private string ModalId { get; } = $"modal-{Guid.NewGuid()}";

        private bool _focusTrapActivated;

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

        public async Task ShowAsync()
        {
            ShowDialog = true;
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (ShowDialog && !_focusTrapActivated)
            {
                _focusTrapActivated = true;
                await JSRuntime.InvokeVoidAsync("setupFocusTrap", ModalId);
            }
        }

        protected async Task OnConfirmationChange(bool value)
        {
            await ConfirmationChanged.InvokeAsync(value);
            ShowDialog = false;
            _focusTrapActivated = false;
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