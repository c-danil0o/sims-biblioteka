using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.Utilities;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            //Disable shutdown when the dialog closes
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            var idg = new IDGenerator();
            var roomIdg = new MembershipCardIDGenerator();
            var repositoryLocator = new RepositoryLocator();
            var serviceLocator = new ServiceLocator(repositoryLocator);
            

            var dialog = new LoginDialog(serviceLocator.UserAccountService);

            if (dialog.ShowDialog() == true)
            {
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                dialog.Close();
                if (Current.MainWindow != null) Current.MainWindow.Show();
            }
            else
            {
                MessageBox.Show("Invalid login.", "Error", MessageBoxButton.OK);
                Current.Shutdown(-1);
            }
        }
    }
}