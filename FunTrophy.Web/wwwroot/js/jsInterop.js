// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.outsideClickHandler = {
    addEvent: function (elementId, dotnetHelper) {
        window.addEventListener("click", (e) => {
            if (!document.getElementById(elementId).contains(e.target)) {
                dotnetHelper.invokeMethodAsync("InvokeClickOutside");
            }
        });
    }
};

window.setupFocusTrap = (modalId) => {
    const modal = document.getElementById(modalId);
    const focusableElements = modal.querySelectorAll('input, select, textarea, [tabindex]:not([tabindex="-1"])');
    const firstElement = focusableElements[0];
    const lastElement = focusableElements[focusableElements.length - 1];

    modal.addEventListener('keydown', (e) => {
        if (e.key === 'Tab') {
            if (e.shiftKey) {
                if (document.activeElement === firstElement) {
                    e.preventDefault();
                    lastElement.focus();
                }
            } else {
                if (document.activeElement === lastElement) {
                    e.preventDefault();
                    firstElement.focus();
                }
            }
        }
    });

    firstElement.focus();
};