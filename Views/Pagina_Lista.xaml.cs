using PM2E18083.Views;
using PM2E18083.Controllers;
using Microsoft.Maui.Controls;
using static SQLite.SQLite3;
using System.Diagnostics;
using System.Collections.ObjectModel;
using PM2E18083.Models;

namespace PM2E18083.Views
{
    public partial class Pagina_Lista : ContentPage
    {

        private Controllers.Locaciones Locaciones;

        Models.Locaciones_ selecteddireccion;
        private Locaciones controller;
        public ObservableCollection<Locaciones_> Ubicacion { get; set; }
        public Command<Locaciones_> UpdateCommand { get; }
        public Command<Locaciones_> DeleteCommand { get; }

        public Pagina_Lista()
        {
            InitializeComponent();
            Locaciones = new Controllers.Locaciones();
            controller = new Locaciones();
            Ubicacion = new ObservableCollection<Locaciones_>();
            BindingContext = this;

        }

        private async void listperson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listperson.ItemsSource = await App.DataBase.getListAutor();
        }

        private void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selecteddireccion = e.CurrentSelection.FirstOrDefault() as Models.Locaciones_;
        }

        private async void btneditar_Clicked(object sender, EventArgs e)
        {
           /* if (selecteddireccion != null)
            {
                await Navigation.PushAsync(new Actualizar(selecteddireccion.Id));
            }
            else
            {
                await DisplayAlert("Error", "Seleccione una ubicacion primero", "OK");
            }*/
        }

        private async void btnborrar_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Confirmar", "¿Está seguro que desea eliminar esta ubicacion?", "Sí", "No");

            if (Locaciones != null)
            {
                if (result)
                {
                    await controller.deleteAutor(selecteddireccion.Id);
                    Ubicacion.Remove(selecteddireccion);

                    var currentPage = this;
                    await Navigation.PushAsync(new Pagina_Lista());
                    Navigation.RemovePage(currentPage);
                }
                else
                {
                    return;
                }
            }
            else
            {
                await DisplayAlert("Error", "Seleccione una ubicacion primero", "OK");
            }
        }

        private async void VerMapa_Clicked(object sender, EventArgs e)
        {
            if (selecteddireccion != null)
            {

                await Navigation.PushAsync(new Mapa(selecteddireccion));
            }
            else
            {
                await DisplayAlert("Error", "Seleccione una ubicacion primero", "OK");
            }
        }
    }
}




