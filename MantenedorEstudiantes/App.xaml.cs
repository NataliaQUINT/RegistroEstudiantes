using MantenedorEstudiantes.Vistas;

namespace MantenedorEstudiantes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ListaEstudiantes());
        }
    }
}
