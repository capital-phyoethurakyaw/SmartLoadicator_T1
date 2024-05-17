using System.Windows.Forms;

namespace SmartLoadicator.Contracts.Views
{
    public interface IShellWindow
    {

        Panel GetNavigationFrame();

        void ShowWindow();

        void CloseWindow();
    }
}
