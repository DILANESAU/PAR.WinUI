using Microsoft.UI.Xaml.Controls;
using PAR.WinUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAR.WinUI.Services
{
    public class NavigationService : INavigationService
    {
        private Frame? _frame;
        public void Initialize(object frame)
        {
            _frame = frame as Frame;
        }
        public bool NavigateTo(string pageKey, object parameter = null)
        {
            if (_frame == null) return false;

            Type? pageType = pageKey switch
            {
                "Dashboard" => typeof(Views.DashboardView),
                _ => null
            };
            if (pageType != null && _frame.Content?.GetType() != pageType)
            {
                return _frame.Navigate(pageType, parameter);
            }
            return false;
        }
        public bool GoBack()
        {
            if (_frame == null || !_frame.CanGoBack)
            {
                _frame.GoBack();
                return true;
            }
            return false;
        }
    }
}
