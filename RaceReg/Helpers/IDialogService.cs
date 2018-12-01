using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceReg.Helpers
{
    public interface IDialogService //needs to renamed to a different name. Name conflict with excisting C# class.
    {
        void ShowMessage(string message);
    }
}
