using Microsoft.AspNetCore.Components.Forms;
using System.Drawing;
using MudBlazor;

namespace Order.Frontend.Components.Pages.ImageUpload;

public partial class UploadImageIndex
{
#nullable enable
    private const string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
    private readonly List<string> _fileNames = new();
    private readonly List<IBrowserFile> _files = new();
    private string? imageData64; // Variable para mostrar la imagen y enviarla a SQL
    private string _dragClass = DefaultDragClass;
    private MudFileUpload<IReadOnlyList<IBrowserFile>>? _fileUpload;

    private async Task ClearAsync()
    {
        await (_fileUpload?.ClearAsync() ?? Task.CompletedTask);
        _fileNames.Clear();
        _files.Clear();
        imageData64 = null;
        ClearDragClass();
    }

    private Task OpenFilePickerAsync()
        => _fileUpload?.OpenFilePickerAsync() ?? Task.CompletedTask;

    private void OnInputFileChanged(InputFileChangeEventArgs e)
    {
        ClearDragClass();
        var files = e.GetMultipleFiles();
        foreach (var file in files)
        {
            _fileNames.Add(file.Name);
            _files.Add(file); // Guardamos la referencia del archivo
        }
    }

    private async Task UploadAsync()
    {
        // Limite de tamaño: 5MB
        long maxAllowedSize = 5 * 1024 * 1024;

        foreach (var file in _files)
        {
            // Ahora la lectura y conversión se hace aquí
            using var stream = file.OpenReadStream(maxAllowedSize);
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            byte[] fileBytes = memoryStream.ToArray();

            // Asignamos la variable que pinta el frontend (mostrará la última imagen subida si son varias)
            imageData64 = $"data:{file.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
            
            // TODO: ¡Aquí usas `fileBytes` o `imageData64` para enviarlo a tu backend y guardarlo en SQL!
        }

        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
        Snackbar.Add("Upload completado.");
    }

    private void SetDragClass()
        => _dragClass = $"{DefaultDragClass} mud-border-primary";

    private void ClearDragClass()
        => _dragClass = DefaultDragClass;
}