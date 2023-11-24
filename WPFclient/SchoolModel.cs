using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFclient
{
    class SchoolModel : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                else
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value < 0)
                {
                    age = 0;
                    OnPropertyChanged("Age");
                }
                else
                {
                    age = value;
                    OnPropertyChanged("Age");
                }


            }
        }
        public bool IsBad
        {
            get
            {
                return isbad;
            }
            set
            {
                isbad = value;
                OnPropertyChanged("IsBad");
            }
        }

        private string? name;
        private string? surname;
        private int age;
        private bool isbad;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
