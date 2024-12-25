using RegistroEstudiantes.Modelos.Modelos;
using Firebase.Database;
using Firebase.Database.Query;


namespace MantenedorEstudiantes.Vistas;

public partial class CrearEstudiante : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://gestionalumnoss-default-rtdb.firebaseio.com");
    public List<Curso> ListaCurso { get; set; }
    public CrearEstudiante()
	{
		InitializeComponent();
        ListaCursos();
		BindingContext=this;
	}
    private void ListaCursos()
    {
        var Cursosp = client.Child("Cursos").OnceAsync<Curso>();
        ListaCurso = Cursosp.Result.Select(x => x.Object).ToList();
    }
    private async void ButonGuadar_Clicked(object sender, EventArgs e)
    {
        Curso cursop = cursoPicker.SelectedItem as Curso;

        var estudiante = new Estudiante
        {
            PrimerNombre = PrimerNombreEntry.Text,
            SegundoNombre = SegundoNombreEntry.Text,
            ApellidoPaterno = ApellidoPaternoEntry.Text,
            ApellidoMaterno = ApellidoMaternoEntry.Text,
            Email = EmailEntry.Text,
            Edad = int.Parse(EdadEntry.Text),
            curso = cursop
        };

        try
        {
            await client.Child("Estudiantes").PostAsync(estudiante);
            await DisplayAlert("Éxito", $"El estudiante {estudiante.PrimerNombre} {estudiante.ApellidoPaterno} fue agregado correctamente.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

 



  
}