using RegistroEstudiantes.Modelos.Modelos;
using System.Collections.ObjectModel;
using Firebase.Database;



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
    private void CargarLista()
    {
        client.Child("Estudiantes").AsObservable<Estudiante>().Subscribe(estudiante => // la coleccion busca los estudiantes
        {
            if (estudiante != null)
            {
                ListadoEstudiantes.Add(estudiante.Object); // agrego el objeto estudiante a la coleccion 
            }
        });
    }
    private void FiltroSearchBar_TextChanged(object sender, EventArgs e) // cuando cambia el texto de la barra de busqueda
    {
        string filtro = FiltroSearchBar.Text.ToLower(); // ese Tolower Transforma todo a minusculas 

        if (filtro.Length > 0) 
        {
            ColeccionDeEstudiantes.ItemsSource = ListadoEstudiantes.Where(FuncionAnonima => FuncionAnonima.NombreCompleto.ToLower().Contains(filtro));
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
}