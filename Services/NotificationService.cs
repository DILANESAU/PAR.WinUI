using PAR.WinUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PAR.WinUI.Services
{
    public class NotificationService : INotificationService
    {
        public void ShowInfo(string message, string title = "Information")
        {
            Debug.WriteLine($"[{title}] {message}");
        }
        public void ShowWarning(string message, string title = "Warning")
        {
            Debug.WriteLine($"[{title}] {message}");
        }
        public void ShowError(string message, string title = "Error")
        {
            Debug.WriteLine($"[{title}] {message}");
        }
    }
}
