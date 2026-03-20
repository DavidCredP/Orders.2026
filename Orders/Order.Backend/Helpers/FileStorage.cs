namespace Order.Backend.Helpers;

public class FileStorage : IFileStorage
{
    public async Task RemoveFileAsync(string path, string containerName)
    {
        // No es necesario eliminar nada físicamente ya que la imagen vive en el registro de la BD.
        await Task.CompletedTask;
    }

    public async Task<string> SaveFileAsync(byte[] content, string extention, string containerName)
    {
        // Convertimos el arreglo de bytes a string base64
        var base64 = Convert.ToBase64String(content);
        
        // Removemos el punto de la extensión si lo trae (ej. ".jpg" -> "jpg")
        var ext = extention.Replace(".", "");
        if (string.IsNullOrEmpty(ext)) ext = "jpeg";

        // Devolvemos el string formateado para ser usado directamente en el src de una imagen HTML
        return $"data:image/{ext};base64,{base64}";
    }
}