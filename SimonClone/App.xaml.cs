using System.Windows;

namespace SimonClone
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IoCContainer.Register();

            base.OnStartup(e);
        }
    }
}
