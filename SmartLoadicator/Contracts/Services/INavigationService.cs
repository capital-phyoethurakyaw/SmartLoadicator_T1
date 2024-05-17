using System;
using System.Windows.Forms;

namespace SmartLoadicator.Contracts.Services
{
    public interface INavigationService
    {

        void Initialize(Panel shellFrame);


        bool NavigateTo(string pageKey, object parameter = null, bool clearNavigation = false);
    }
}
