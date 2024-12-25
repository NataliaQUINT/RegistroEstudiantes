using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Logging;
using RegistroEstudiantes.Modelos.Modelos;

namespace MantenedorEstudiantes
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            Registrar();
            
            return builder.Build();
        }

        public static void Registrar()
        {
            FirebaseClient client = new FirebaseClient("https://gestionalumnoss-default-rtdb.firebaseio.com");

            var curso = client.Child("Cursos").OnceAsync<Curso>();

            if (curso.Result.Count == 0)
            {
                client.Child("Cursos").PostAsync(new Curso { Nombre = "1° Básico" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "2° Básico" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "3° Básico" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "4° Básico" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "5° Básico" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "6° Básico" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "7° Básico" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "8° Básico" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "1ero Medio" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "2do Medio" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "3ero Medio" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "4to Medio" });
               
            }

        }
    }
}
