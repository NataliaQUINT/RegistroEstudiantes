using RegistroEstudiantes.Modelos.Modelos;
using System.Collections.ObjectModel;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using LiteDB;

namespace MantenedorEstudiantes.Vistas;

public partial class ListaEstudiantes : ContentPage
{

    FirebaseClient client = new FirebaseClient("https://gestionalumnoss-default-rtdb.firebaseio.com"); // Voy a la base de datos de google
    public ObservableCollection<Estudiante> ListadoEstudiantes { get; set; } = new ObservableCollection<Estudiante>(); // creo una coleccion de estudiantes
    public ListaEstudiantes() // este constructor
    {
        InitializeComponent(); // inicia la vista
        BindingContext = this;
        CargarLista(); // carga la lista al iniciar la vista por que esta dentro del constructor ListaEstudiante 
    }
    private async void CargarLista()
    {
        ListadoEstudiantes.Clear();// limpia la lista
        var estudiantes = await client
            .Child("Estudiantes")
            .OnceAsync<Estudiante>(); // la coleccion busca los estudiantes
        var estudiantesActivos = estudiantes.Where(e => e.Object.Estado == true).ToList();


        foreach (var estudiante in estudiantesActivos)
        {
            ListadoEstudiantes.Add(new Estudiante
            {
                Id = estudiante.Key,
                PrimerNombre = estudiante.Object.PrimerNombre,
                SegundoNombre = estudiante.Object.SegundoNombre,
                ApellidoPaterno = estudiante.Object.ApellidoPaterno,
                ApellidoMaterno = estudiante.Object.ApellidoMaterno,
                Email = estudiante.Object.Email,
                Edad = estudiante.Object.Edad,
                Estado = estudiante.Object.Estado,
                curso = estudiante.Object.curso
            });
        }
    }
    private void FiltroSearchBar_TextChanged(object sender, EventArgs e) // cuando cambia el texto de la barra de busqueda
    {
        string filtro = FiltroSearchBar.Text.ToLower(); // ese Tolower Transforma todo a minusculas 

        if (filtro.Length > 0)
        {
            ColeccionDeEstudiantes.ItemsSource = ListadoEstudiantes.Where(x => x.NombreCompleto.ToLower().Contains(filtro));
            // esto es como hacer un select * from listadoEstudiantes where Nombrecompleto = filtro a la coleccion ...  
        }
        else
        {
            ColeccionDeEstudiantes.ItemsSource = ListadoEstudiantes; // si el buscador esta vacio .. trae todos los elementos del listado
        }
    }
    private async void NuevoEstudianteBtn_Clicked(object sender, EventArgs e) // cuando pinchan el boton Nuevo Estudiante
    {
        await Navigation.PushAsync(new CrearEstudiante()); // voy a la vista CrearEstudiante
    }

    private async void editarButton_Clicked(object sender, EventArgs e)
    {
        var boton = sender as ImageButton;
        var estudiante = boton?.CommandParameter as Estudiante;

        if (estudiante != null && !string.IsNullOrEmpty(estudiante.Id))
        {
            var paginaEdicion = new EditarEstudiante(estudiante.Id);
            await Navigation.PushAsync( paginaEdicion);

            paginaEdicion.Disappearing += (s, args) => CargarLista();//espera que pag cierre y luego recarga lista

        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener la información del estudiante", "OK");
        }
    }

    private async void deshabilitarButton_Clicked(object sender, EventArgs e)
    {
        var boton = sender as ImageButton;
        var estudiante = boton?.CommandParameter as Estudiante;

        if (estudiante == null)
        {
            await DisplayAlert("Error", "No se pudo obtener la información del estudiante", "OK");
            return;
        }

        bool confirmacion = await DisplayAlert
            ("Confirmación", $"Está seguro que desea deshabilitar al estudiante {estudiante.NombreCompleto}", "Sí", "No");

        if (confirmacion)
        {
            try
            {
                estudiante.Estado = false;
                await client.Child("Estudiantes").Child(estudiante.Id).PutAsync(estudiante);
                await DisplayAlert("Exito", $"Se ha deshabilitado correctamente al estudiante {estudiante.NombreCompleto}", "OK");
                CargarLista();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
