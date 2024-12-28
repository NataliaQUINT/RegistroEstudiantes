using Firebase.Database;
using Firebase.Database.Query;
using RegistroEstudiantes.Modelos.Modelos;
using System.Collections.ObjectModel;
using System.Collections.ObjectModel;
using System.Security;
using static System.Net.Mime.MediaTypeNames;
namespace MantenedorEstudiantes.Vistas;

public partial class EditarEstudiante : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://gestionalumnoss-default-rtdb.firebaseio.com//");
    public List<Curso> Cursos { get; set; }
    public ObservableCollection<string> ListaCursos { get; set; } = new ObservableCollection<string>();
    private Estudiante estudianteActualizado = new Estudiante();
    private string estudianteId;
    public EditarEstudiante(string idEstudiante)
    {
        InitializeComponent();
        BindingContext = this;
        estudianteId = idEstudiante;
        CargarListaCursos();
        CargarEstudiante(estudianteId);

    }

    private async void CargarListaCursos()
    {
        try
        {
            var cursos = await client.Child("Cursos").OnceAsync<Curso>();
            ListaCursos.Clear();
            foreach (var curso in cursos)
            {
                ListaCursos.Add(curso.Object.Nombre);
            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", "Error:" + ex.Message, "Ok");
        }

    }

    private async void CargarEstudiante(string idEstudiante)
    {
        var estudiante = await client.Child("Estudiantes").Child(idEstudiante).OnceSingleAsync<Estudiante>();

        if (estudiante != null)
        {
            EditPrimerNombreEntry.Text = estudiante.PrimerNombre;
            EditSegundoNombreEntry.Text = estudiante.SegundoNombre;
            EditApellidoPaternoEntry.Text = estudiante.ApellidoPaterno;
            EditApellidoMaternoEntry.Text = estudiante.ApellidoMaterno;
            EditEmailEntry.Text = estudiante.Email;
            EditEdadEntry.Text = estudiante.Edad.ToString();
            EditCursoPicker.SelectedItem = estudiante.curso?.Nombre;
        }
    }

    private async void ActualizarButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(EditPrimerNombreEntry.Text) ||
                string.IsNullOrWhiteSpace(EditSegundoNombreEntry.Text) ||
                string.IsNullOrWhiteSpace(EditApellidoPaternoEntry.Text) ||
                string.IsNullOrWhiteSpace(EditApellidoMaternoEntry.Text) ||
                string.IsNullOrWhiteSpace(EditEmailEntry.Text) ||
                string.IsNullOrWhiteSpace(EditEdadEntry.Text) ||
                EditCursoPicker.SelectedItem == null)
            {

                await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                return;
            }

            if (!EditEmailEntry.Text.Contains("@"))
            {
                await DisplayAlert("Error", "El correo electrónico no es válido", "OK");
                return;
            }

            if (!int.TryParse(EditEdadEntry.Text, out int edad))
            {
                await DisplayAlert("Error", "La edad debe ser un número válido", "OK");
                return;
            }

            if (edad <= 0)
            {
                await DisplayAlert("Error", "La edad debe ser mayor a 0", "OK");
                return;
            }

            estudianteActualizado.Id = estudianteId;
            estudianteActualizado.PrimerNombre = EditPrimerNombreEntry.Text.Trim();
            estudianteActualizado.SegundoNombre = EditSegundoNombreEntry.Text.Trim();
            estudianteActualizado.ApellidoPaterno = EditApellidoPaternoEntry.Text.Trim();
            estudianteActualizado.ApellidoMaterno = EditApellidoMaternoEntry.Text.Trim();
            estudianteActualizado.Email = EditEmailEntry.Text.Trim();
            estudianteActualizado.Edad = edad;
            estudianteActualizado.Estado = estadoSwitch.IsToggled;
            estudianteActualizado.curso = new Curso { Nombre = EditCursoPicker.SelectedItem.ToString() };

            await client.Child("Estudiantes").Child(estudianteActualizado.Id).PutAsync(estudianteActualizado);

            await DisplayAlert("Éxito", "El estudiante se ha actualizado correctamente", "OK");
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", "Error" + ex.Message, "OK");
        }
    }
}