using Microsoft.AspNetCore.Components;

namespace Order.Frontend.Components.Shared;

public partial class Pagination
{
    private List<PageModel> links = new();
    private List<OptionModel> options = new();

    [Parameter] public int CurrentPage { get; set; } = 1;
    [Parameter] public int TotalPages { get; set; }
    [Parameter] public int Radio { get; set; } = 3;
    [Parameter] public EventCallback<int> SelectedPage { get; set; }
    [Parameter] public EventCallback<int> RecordsNumber { get; set; }
    [Parameter] public bool IsHome { get; set; } = false;

    protected override void OnParametersSet()
    {
        BuildOptions();
        BuildLinks();
    }

    private void BuildOptions()
    {
        if (IsHome)
        {
            options =
            [
                new OptionModel { Value = 8, Name = "8" },
                new OptionModel { Value = 16, Name = "16" },
                new OptionModel { Value = 32, Name = "32" },
                new OptionModel { Value = int.MaxValue, Name = "Todos" },
            ];
        }
        else
        {
            options =
            [
                new OptionModel { Value = 10, Name = "10" },
                new OptionModel { Value = 25, Name = "25" },
                new OptionModel { Value = 50, Name = "50" },
                new OptionModel { Value = int.MaxValue, Name = "Todos" },
            ];
        }
    }

    private void BuildLinks()
    {
        links = new List<PageModel>();
        var previousPageEnabled = CurrentPage != 1;
        var previousPage = CurrentPage - 1;
        links.Add(new PageModel { Text = "Anterior", PageValue = previousPage, Enable = previousPageEnabled });

        for (int i = 1; i <= TotalPages; i++)
        {
            if (i >= CurrentPage - Radio && i <= CurrentPage + Radio)
            {
                links.Add(new PageModel { Text = $"{i}", PageValue = i, Enable = true, Active = CurrentPage == i });
            }
        }

        var nextPageEnabled = CurrentPage != TotalPages;
        var nextPage = CurrentPage + 1;
        links.Add(new PageModel { Text = "Siguiente", PageValue = nextPage, Enable = nextPageEnabled });
    }

    private async Task InternalSelectedPage(PageModel pageModel)
    {
        if (pageModel.PageValue == CurrentPage || !pageModel.Enable)
        {
            return;
        }

        await SelectedPage.InvokeAsync(pageModel.PageValue);
    }

    private async Task ChangeRecordsNumber(ChangeEventArgs e)
    {
        var value = Convert.ToInt32(e.Value);
        await RecordsNumber.InvokeAsync(value);
    }

    class PageModel
    {
        public string Text { get; set; } = null!;
        public int PageValue { get; set; }
        public bool Enable { get; set; } = true;
        public bool Active { get; set; } = false;
    }

    class OptionModel
    {
        public int Value { get; set; }
        public string Name { get; set; } = null!;
    }
}