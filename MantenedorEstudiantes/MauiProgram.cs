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
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
                builder.Logging.AddDebug();
#endif
            ActualizarCursos();
            ActualizarEstudiantes();

            return builder.Build();
        }

        public static async Task ActualizarCursos()
        {
            FirebaseClient client = new FirebaseClient("https://gestionalumnoss-default-rtdb.firebaseio.com");

            var cursos = await client
                .Child("Cursos")
                .OnceAsync<Curso>();

            if (cursos.Count == 0)
            {
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "1° Básico" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "2° Básico" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "3° Básico" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "4° Básico" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "5° Básico" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "6° Básico" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "7° Básico" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "8° Básico" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "1ero Medio" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "2do Medio" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "3ero Medio" });
                await client.Child("Cursos").PostAsync(new Curso { Nombre = "4to Medio" });

            }
            else
            {
                foreach (var cursoItem in cursos)
                {
                    if (cursoItem.Object.Estado == null)
                    {
                        var cursoActualizado = cursoItem.Object;
                        cursoActualizado.Estado = true;

                        await client.Child("Cursos").Child(cursoItem.Key).PutAsync(cursoActualizado);
                    }
                }
            }
        }

        public static async Task ActualizarEstudiantes()
        {
            FirebaseClient client = new FirebaseClient("https://gestionalumnoss-default-rtdb.firebaseio.com/");

            var estudiantes = await client.Child("Estudiantes").OnceAsync<Estudiante>();

            foreach (var estudiante in estudiantes)
            {
                if (estudiante.Object.Estado == null)
                {
                    var estudianteActualizado = estudiante.Object;
                    estudianteActualizado.Estado = true;

                    await client.Child("Estudiantes").Child(estudiante.Key).PutAsync(estudianteActualizado);
                }
            }

        }
    }
}

