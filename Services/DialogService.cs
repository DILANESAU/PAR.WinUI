using PAR.WinUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace PAR.WinUI.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowErrorDialog(string message)
        {
            Debug.WriteLine($"[DIALOG ERROR] {message}");
            return Task.CompletedTask;
        }
    }
}
