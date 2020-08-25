using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public static int PageNumber = 1;

        public MainWindowViewModel()
        {
            //HTML files are removed on each application satrtup, to avoid naming collision.
            DirectoryInfo di = new DirectoryInfo(@"..\\..\HTML\");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Method creates HTML files in designated folder.
        /// </summary>
        /// <param name="link"></param>
        public void GetHtml(string link)
        {
            try
            {
                WebRequest req = WebRequest.Create(link);
                req.Method = "GET";

                string source;

                using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    source = reader.ReadToEnd();
                }

                string file = string.Format(@"..\\..\HTML\Page {0}.htm", PageNumber++);

                File.WriteAllText(file, source);

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("HTML file successfully generated.", "Notification");
            }
            catch (Exception)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("HTML file not generated, please try again.", "Notification");
            }
        }

        /// <summary>
        /// Mehod creates ZIP files in designated folder
        /// </summary>
        public void CreateZip()
        {
            try
            {
                //Validation if HTML files exist in designated HTML folder.
                int FileCount = 0;

                DirectoryInfo di = new DirectoryInfo(@"..\\..\HTML\");

                foreach (FileInfo file in di.GetFiles())
                {
                    FileCount++;
                }

                if(FileCount == 0)
                {
                    MessageBoxResult messageBoxResult1 = System.Windows.MessageBox.Show("There are no HTML files generated to ZIP, please create some first.", "Notification");
                    return;
                }

                di = new DirectoryInfo(@"..\\..\ZIP\");

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                string startPath = @"..\\..\HTML\";
                string zipPath = @"..\\..\ZIP\HTML.zip";

                ZipFile.CreateFromDirectory(startPath, zipPath);

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("ZIP file successfully generated.", "Notification");
            }
            catch (Exception)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("ZIP file not generated, please try again.", "Notification");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
