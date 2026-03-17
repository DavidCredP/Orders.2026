using Microsoft.AspNetCore.Components;
using MudBlazor;
using Order.Frontend.Repositories;
using Order.Frontend.Services;
using Orders.Shared.DTOs;

namespace Order.Frontend.Components.Pages.Auth;

public partial class Login
{
    private LoginDTO loginDTO = new();
    private bool wasClose;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ILoginService LoginService { get; set; } = null!;

    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    private void ShowModalResendConfirmationEmail()
    {
        var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true, CloseButton = true, MaxWidth = MaxWidth.ExtraLarge };
        DialogService.ShowAsync<ResendConfirmationEmailToken>("Reenvio de correo", closeOnEscapeKey);
    }

    private void ShowModalRecoverPassword()
    {
        var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true, CloseButton = true, MaxWidth = MaxWidth.ExtraLarge };
        DialogService.ShowAsync<RecoverPassword>("Rec. contraseña", closeOnEscapeKey);
    }

    private void CloseModal()
    {
        wasClose = true;
        MudDialog.Cancel();
    }

    private async Task LoginAsync()
    {
        if (wasClose)
        {
            NavigationManager.NavigateTo("/");
            return;
        }

        var responseHttp = await Repository.PostAsync<LoginDTO, TokenDTO>("/api/accounts/Login", loginDTO);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message!, Severity.Error);
            return;
        }

        await LoginService.LoginAsync(responseHttp.Response!.Token);
        NavigationManager.NavigateTo("/");
    }
}