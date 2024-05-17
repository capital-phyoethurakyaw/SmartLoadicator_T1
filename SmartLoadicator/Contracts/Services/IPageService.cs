using System;
using System.Windows.Forms;

namespace SmartLoadicator.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);

        UserControl GetPage(string key);
    }
}
