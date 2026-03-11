using Microsoft.AspNetCore.Components;

namespace Order.Frontend.Components.Shared;

public partial class Loading
{
    [Parameter] public string? Label { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (string.IsNullOrWhiteSpace(Label))
        {
            Label = "Loading, por favor espera ...";
        }
    }
}