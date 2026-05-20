using System;
using System.Collections.Generic;
using System.Text;

namespace PAR.WinUI.Interfaces
{
    public interface INavigationService
    {
        void Initialize(object frame);
        bool NavigateTo(string pageKey, object parameter = null);
        bool GoBack();
    }
}
