using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestOnWork.Model
{
    class Lot : INotifyPropertyChanged
    {

        private string title;
        private string imagePath;
        public int Id { get; set; }
        public string Title {
            get { return title; }
            set { 
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string ImagePath
        {
            get { return imagePath; }
            set { 
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
