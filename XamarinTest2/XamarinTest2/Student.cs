using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace XamarinTest2
{
    class Student : INotifyPropertyChanged
    {
        private string name;
        private string faculty;
        private float score;
        private Guid guid;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Faculty
        {
            get { return faculty; }
            set
            {
                if (faculty != value)
                {
                    faculty = value;
                    OnPropertyChanged("Faculty");
                }
            }
        }
        public float Score
        {
            get { return score; }
            set
            {
                if (score != value)
                {
                    score = value;
                    OnPropertyChanged("Score");
                }
            }
        }
        public Guid Guid
        {
            get { return guid; }
            set
            {
                if (guid != value)
                {
                    guid = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        /*public async Task CloudSync()
        {

        }*/
        public static Student Parse(string st)
        {
            var args = st.Split('\n');
            if (args.Length == 4)
            {
                return new Student
                {
                    Guid = Guid.Parse(args[0]),
                    Name = args[1],
                    Faculty = args[2],
                    Score = float.Parse(args[3]),
                };
            }
            return null;
        }

        public override string ToString()
        {
            return $"{name}\n{faculty}\n{score}";
        }
    }
}
