using System;
using System.Collections.Generic;
using System.Text;

namespace PAR.WinUI.Interfaces
{
    public interface INotificationService
    {
        void ShowInfo(string message, string title = "Information");
        void ShowWarning(string message, string title = "Warning");
        void ShowError(string message, string title = "Error");
    }
}
