using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace Zadatak_1.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {

        }

        public void GetHtml()
        {
            WebRequest req = WebRequest.Create("https://huddle.eurostarsoftwaretesting.com/agile-testing-vs-waterfall-testing/");
            req.Method = "GET";

            string source;

            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                source = reader.ReadToEnd();
            }

            File.WriteAllText(@"..\\..\HTML\HTML.txt", source);
        }

        public void CreateZip()
        {
            string startPath = @"..\\..\HTML\";
            string zipPath = @"..\\..\ZIP\HTML.zip";

            ZipFile.CreateFromDirectory(startPath, zipPath);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
