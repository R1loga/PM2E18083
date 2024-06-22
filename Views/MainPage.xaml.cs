using System;
using System.IO;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Media;
using Microsoft.Maui;


namespace PM2E18083
{
    public partial class MainPage : ContentPage
    {
        FileResult photo;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                photo = await MediaPicker.CapturePhotoAsync();

                if (photo != null)
                {
                    string localPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using (Stream sourceStream = await photo.OpenReadAsync())
                    using (FileStream imageLocal = File.OpenWrite(localPath))
                    {
                        await sourceStream.CopyToAsync(imageLocal);
                    }

                    foto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public String GetImage64()
        {
            String Base64 = String.Empty;

            if (photo != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream stream = photo.OpenReadAsync().Result;
                    stream.CopyTo(ms);
                    byte[] data = ms.ToArray();

                    Base64 = Convert.ToBase64String(data);
                    return Base64;
                }
            }

            return Base64;
        }

        public byte[] GetImageArray()
        {
            byte[] data = new Byte[] { };

            if (photo != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream stream = photo.OpenReadAsync().Result;
                    stream.CopyTo(ms);
                    data = ms.ToArray();

                    return data;
                }
            }

            return data;
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                   
                    await DisplayAlert("Error", "No se otorgó permiso para acceder a la ubicación", "OK");
                    return;
                }

               
                var location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout =TimeSpan.FromSeconds(30)
                });
                if (location != null)
                {
                   
                    longi.Text = location.Longitude.ToString();
                    lati.Text = location.Latitude.ToString();

                   
                    var locacion = new Models.Locaciones_
                    {
                        longitud = longi.Text,
                        latitud = lati.Text,
                        descrip = desc.Text,
                        Foto = GetImage64()
                    };

                    if (await App.DataBase.storeAutor(locacion) > 0)
                    {
                        await DisplayAlert("Aviso", "Registro ingresado con éxito!!", "OK");
                    }
                }
                else
                {
              
                    await DisplayAlert("Error", "No se pudo obtener la ubicación actual", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void btnListSites_Clicked(object sender, EventArgs e)
        {
            var nextPage = new Views.Pagina_Lista();

         
            Navigation.PushAsync(nextPage);

        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }
    }
}