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

       
        Curso cursop = cursoPicker.SelectedItem as Curso;// selecciona un curso de la lista seleccionado por el usuario

        var estudiante = new Estudiante // instancia clase estudiante
        {
            PrimerNombre = PrimerNombreEntry.Text,// captura campo de text PrimerNombre y guarda en la propiedad PrimerNombre clase estudiante.
            SegundoNombre = SegundoNombreEntry.Text,
            ApellidoPaterno = ApellidoPaternoEntry.Text,
            ApellidoMaterno = ApellidoMaternoEntry.Text,
            Email = EmailEntry.Text,
            Edad = int.Parse(EdadEntry.Text),
            Estado = estadoSwitch.IsToggled, // en duro
            curso = cursop // clase hija de estudiante
        };

        try // INTENTA
        {
            await client.Child("Estudiantes").PostAsync(estudiante);//guardar en la BD objeto estudiante
            await DisplayAlert("Éxito", $"El estudiante {estudiante.PrimerNombre} {estudiante.ApellidoPaterno} fue agregado correctamente.", "OK");//envia msj
            await Navigation.PopAsync();//vuelve a ventana anterior
        }
        catch (Exception ex) //CAPTURA 
        {
            await DisplayAlert("Error", ex.Message, "OK");//envia msj error.
        }
    }

 



  
}




 //var estudiante1 = new Estudiante
 //{
 //    PrimerNombre = "Carlos",
 //    SegundoNombre = "Eduardo",
 //    ApellidoPaterno = "López",
 //    ApellidoMaterno = "Martínez",
 //    Email = "carlos.lopez@example.com",
 //    Edad = 15,
 //    curso = new Curso { Nombre = "1ero Medio" }
 //};
 //   await client.Child("Estudiantes").PostAsync(estudiante1);

 //   var estudiante2 = new Estudiante
 //   {
 //       PrimerNombre = "Maria",
 //       SegundoNombre = "Fernanda",
 //       ApellidoPaterno = "Gonzalez",
 //       ApellidoMaterno = "Perez",
 //       Email = " maria.fer@example.com",
 //       Edad = 16,
 //       curso = new Curso { Nombre = "2do Medio" }
 //   };
 //   await client.Child("Estudiantes").PostAsync(estudiante2);

 //   var estudiante3 = new Estudiante
 //   {
 //       PrimerNombre = "Juan",
 //       SegundoNombre = "Pablo",
 //       ApellidoPaterno = "Rodriguez",
 //       ApellidoMaterno = "Gonzalez",
 //       Email = "Juan.R@example.cl",
 //       Edad = 17,
 //       curso = new Curso { Nombre = "3ero Medio" }
 //   };
 //   await client.Child("Estudiantes").PostAsync(estudiante3);

 //   var estudiante4 = new Estudiante
 //   {
 //       PrimerNombre = "Pedro",
 //       SegundoNombre = "Pablo",
 //       ApellidoPaterno = "Rojas",
 //       ApellidoMaterno = "Gonzalez",
 //       Email = "Pedro.Rojas@example.cl",
 //       Edad = 18,
 //       curso = new Curso { Nombre = "4to Medio" }
 //   };
 //   await client.Child("Estudiantes").PostAsync(estudiante4);

 //   var estudiante5 = new Estudiante
 //   {
 //       PrimerNombre = "Jose",
 //       SegundoNombre = "Pablo",
 //       ApellidoPaterno = "Torres",
 //       ApellidoMaterno = "Retamales",
 //       Email = " JP.Torres@example.cl",
 //       Edad = 15,
 //       curso = new Curso { Nombre = "2do Medio" }
 //   };
 //   await client.Child("Estudiantes").PostAsync(estudiante5);
