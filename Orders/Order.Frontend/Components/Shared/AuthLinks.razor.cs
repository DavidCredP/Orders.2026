using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Order.Frontend.Components.Pages.Auth;

namespace Order.Frontend.Components.Shared;

public partial class AuthLinks
{
    private string? photoUser;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private Order.Frontend.Repositories.IRepository Repository { get; set; } = null!;
    [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

    protected override async Task OnParametersSetAsync()
    {
        var authenticationState = await AuthenticationStateTask;
        if (authenticationState.User.Identity?.IsAuthenticated ?? false)
        {
            var responseHttp = await Repository.GetAsync<Orders.Shared.Entities.User>("/api/accounts");
            if (!responseHttp.Error)
            {
                photoUser = responseHttp.Response?.Photo;
            }
        }
        else
        {
            photoUser = null;
        }
    }

    private void EditAction()
    {
        NavigationManager.NavigateTo("/EditUser");
    }

    private void ShowModalLogIn()
    {
        var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
        DialogService.ShowAsync<Login>("Inicio de Sesion", closeOnEscapeKey);
    }

    private void ShowModalLogOut()
    {
        var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
        DialogService.ShowAsync<Logout>("Cerrar Sesion", closeOnEscapeKey);
    }

    private void ShowModalRegister()
    {
        var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
        DialogService.ShowAsync<Register>("Registar Usuario", closeOnEscapeKey);
    }
}