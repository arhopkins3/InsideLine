using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;

using InsideLine.Services;

namespace InsideLine.Components.Pages
{
    public partial class RegisteredDriver
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }




    }



    
}