using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAR.WinUI.Interfaces
{
    public interface IDialogService
    {
        Task ShowErrorDialog(string message);
    }
}
