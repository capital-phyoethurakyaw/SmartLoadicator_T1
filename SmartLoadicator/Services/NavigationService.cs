using System;
using System.Linq;
using System.Windows.Forms;

using SmartLoadicator.Contracts.Services;

namespace SmartLoadicator.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IPageService _pageService;
        private Panel _frame;
        private object _lastParameterUsed;

        public event EventHandler<string> Navigated;

        public NavigationService(IPageService pageService)
        {
            _pageService = pageService;
        }
        
        public void Initialize(Panel shellFrame)
        {
            if (_frame == null)
            {
                _frame = shellFrame;
            }
        }



        public bool NavigateTo(string pageKey, object parameter = null, bool clearNavigation = false)
        {
             var pageType = _pageService.GetPageType(pageKey);
            if (!_frame.Controls.OfType<UserControl>().Any())
            {
                var page = _pageService.GetPage(pageKey);
	         int x = (_frame.Width - page.Width) / 2;
                int y = (_frame.Height - page.Height) / 2;
                page.Location = new System.Drawing.Point(x, y);
                _frame.Controls.Add(page);

            }
            else 
            {
                _frame.Controls.Cast<Control>().Where(c => c is UserControl).ToList().ForEach(c => _frame.Controls.Remove(c));
                var page = _pageService.GetPage(pageKey);
		int x = (_frame.Width - page.Width) / 2;
                int y = (_frame.Height - page.Height) / 2;
                page.Location = new System.Drawing.Point(x, y);
                _frame.Controls.Add(page);
            }
            return false;
        }

    }
}
