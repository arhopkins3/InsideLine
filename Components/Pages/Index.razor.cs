using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;

using InsideLine.Services;

namespace InsideLine.Components.Pages
{
    public partial class Index
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


 bool smooth = true;
    bool showDataLabels = false;
    class DataItem
    {
        public string Date { get; set; }
        public double Revenue { get; set; }
    }

    string FormatAsUSD(object value)
    {
        return ((double)value).ToString("C0", CultureInfo.CreateSpecificCulture("en-GB"));
    }

    string FormatAsMonth(object value)
    {
        if (value != null)
        {
            return Convert.ToDateTime(value).ToString("MMM");
        }

        return string.Empty;
    }

    DataItem[] revenue2019 = new DataItem[] {
        new DataItem
        {
            Date = ("2019-01-01"),
            Revenue = 234000
        },
        new DataItem
        {
            Date = ("2019-02-01"),
            Revenue = 269000
        },
        new DataItem
        {
            Date = ("2019-03-01"),
            Revenue = 233000
        },
        new DataItem
        {
            Date = ("2019-04-01"),
            Revenue = 244000
        },
        new DataItem
        {
            Date = ("2019-05-01"),
            Revenue = 214000
        },
        new DataItem
        {
            Date = ("2019-06-01"),
            Revenue = 253000
        },
        new DataItem
        {
            Date = ("2019-07-01"),
            Revenue = 274000
        },
        new DataItem
        {
            Date = ("2019-08-01"),
            Revenue = 284000
        },
        new DataItem
        {
            Date = ("2019-09-01"),
            Revenue = 273000
        },
        new DataItem
        {
            Date = ("2019-10-01"),
            Revenue = 282000
        },
        new DataItem
        {
            Date = ("2019-11-01"),
            Revenue = 289000
        },
        new DataItem
        {
            Date = ("2019-12-01"),
            Revenue = 294000
        }
    };

    DataItem[] revenue2020 = new DataItem[] {
        new DataItem
        {
            Date = ("2019-01-01"),
            Revenue = 334000
        },
        new DataItem
        {
            Date = ("2019-02-01"),
            Revenue = 369000
        },
        new DataItem
        {
            Date = ("2019-03-01"),
            Revenue = 333000
        },
        new DataItem
        {
            Date = ("2019-04-01"),
            Revenue = 344000
        },
        new DataItem
        {
            Date = ("2019-05-01"),
            Revenue = 314000
        },
        new DataItem
        {
            Date = ("2019-06-01"),
            Revenue = 353000
        },
        new DataItem
        {
            Date = ("2019-07-01"),
            Revenue = 374000
        },
        new DataItem
        {
            Date = ("2019-08-01"),
            Revenue = 384000
        },
        new DataItem
        {
            Date = ("2019-09-01"),
            Revenue = 373000
        },
        new DataItem
        {
            Date = ("2019-10-01"),
            Revenue = 382000
        },
        new DataItem
        {
            Date = ("2019-11-01"),
            Revenue = 389000
        },
        new DataItem
        {
            Date = ("2019-12-01"),
            Revenue = 394000
        }
    };



    }



    
}