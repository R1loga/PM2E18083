
using Microsoft.Maui.Controls.Maps;
using PM2E18083.Controllers;
using PM2E18083.Models;
namespace PM2E18083.Views;

public partial class Mapa : ContentPage
{
    string photo;
    public Mapa(Locaciones_ Tabla)
	{
		InitializeComponent();
	}

    private async void btnCompartir_Clicked(object sender, EventArgs e)
    {
        byte[] imageBytes = Convert.FromBase64String(photo);
        string tempFilePath = Path.Combine(FileSystem.CacheDirectory, "tempImage.png");
        File.WriteAllBytes(tempFilePath, imageBytes);

        await ShareFileAsync(tempFilePath);
    }

    private async Task ShareFileAsync(string filePath)
    {
        await Share.RequestAsync(new ShareFileRequest
        {
            Title = "Compartir Imagen",
            File = new ShareFile(filePath)
        });
    }
}
