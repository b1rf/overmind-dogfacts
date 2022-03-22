using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OvermindDogFacts.Utils
{
    public class Util
    {
        internal static string SelecionarDiretorio() 
        {
            FolderBrowserDialog selectFolder = new FolderBrowserDialog();
            selectFolder.ShowDialog();
            return selectFolder.SelectedPath;
        }
    }
}
