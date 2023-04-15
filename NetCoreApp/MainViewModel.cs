using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Windows.UI;

namespace NetCoreApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string username;
        public string UserName 
        {
            get => username; 
            set
            {
                if (value != username)
                {
                    username = value;
                    System.Diagnostics.Debug.WriteLine(username);
                    RaisePropertyChanged();
                }
            } 
        }
        private string message;
        public string Message
        {
            get => message;
            set
            {
                if (value != message)
                {
                    message = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand SayHelloCommand { get; }

        public void SayHello()
        {
            Message = $"Hello, {username}";
        }

        public MainViewModel()
        {
            SayHelloCommand = new RelayCommand(_ => SayHello(), _ => true);
        }

        private void RaisePropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
