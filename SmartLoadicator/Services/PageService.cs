using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using SmartLoadicator.Contracts.Services;
using SmartLoadicator.Views;

namespace SmartLoadicator.Services
{
    public class PageService : IPageService
    {
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();
        private readonly IServiceProvider _serviceProvider;

        public PageService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Configure<MainPage>();
        }

        public Type GetPageType(string key)
        {
            Type pageType;
            lock (_pages)
            {
                string tempKey = string.Empty;
                if (key.Contains("."))
                {
                    tempKey = key.Split('.')[2].Contains("Page") ? key.Split('.')[2] : key.Split('.')[2] + "Page";
                }
                else
                {
                    tempKey = key.Contains("Page") ? key : key + "Page";
                }
                var selectedPages = _pages.Select(x => x)
                .Where(x => x.Key.Split('.')[2].Equals(tempKey))
                          .ToList();
                if (!_pages.TryGetValue(selectedPages[0].Key, out pageType))
                {
                    throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
                }
            }

            return pageType;
        }

        public UserControl GetPage(string key)
        {
            var pageType = GetPageType(key);
            return _serviceProvider.GetService(pageType) as UserControl;
        }

        private void Configure<V>()
            where V : UserControl
        {
            lock (_pages)
            {
                var key = typeof(V).FullName;
                if (_pages.ContainsKey(key))
                {
                    throw new ArgumentException($"The key {key} is already configured in PageService");
                }

                var type = typeof(V);
                if (_pages.Any(p => p.Value == type))
                {
                    throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
                }

                _pages.Add(key, type);
            }
        }
    }
}
