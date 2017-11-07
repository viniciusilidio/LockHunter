using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace Teste
{
    class Main
    {
        int numOfFolders = 0;
        int totalFolder = 0;

        public void Execute (string path)
        {
            Thread thread = new Thread(() =>
            {
                Form1 form = (Form1)Application.OpenForms[0];
                form.Invoke(new Action (()=> form.ToogleUI()));

                totalFolder = CountSubFolders(path);

                SeachLockRecursive(path);
                form.Invoke(new Action(() => form.ToogleUI()));
                numOfFolders = 0;
                totalFolder = 0;
            });

            thread.Start();
        }

        private void SeachLockRecursive (string path)
        {
            string[] folders = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            foreach (string fileString in files)
            {
                FileStream stream = null;
                FileInfo file = new FileInfo(fileString);

                try
                {
                    stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                }
                catch (IOException)
                {
                    AppendToFile(fileString);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }

            foreach (string folder in folders)
            {
                Form1 form = (Form1)Application.OpenForms[0];
                form.Invoke(new Action(() => form.UpdateProgressBar(++numOfFolders, totalFolder)));
                SeachLockRecursive(folder);
            }
        }

        private int CountSubFolders (string root)
        {
            string[] folders = Directory.GetDirectories(root);

            int subFolders = 0;
            foreach (string folder in folders)
            {
                subFolders += CountSubFolders(folder);
            }

            return subFolders + 1;
        }

        private void AppendToFile (string text)
        {
            string desktop = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\lockedFields.txt";
            File.AppendAllText(desktop, $"{text}{Environment.NewLine}");
        }
    }
}
