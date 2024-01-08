using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Windows.UI;

namespace NetCoreApp
{
    public struct ListItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

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

        public List<ListItem> Items { get; }

        public void SayHello()
        {
            Message = $"Hello, {username}";
        }

        public MainViewModel()
        {
            SayHelloCommand = new RelayCommand(_ => SayHello(), _ => true);
            Items = new()
            {
                new() { Name = "hello world", Value = "It's long overdude" },
                new() { Name = "UWP on .NET 8", Value = "yeah it really works" },
                new() { Name = "Some .NET 5+ stuff?", Value = DateOnly.FromDateTime(DateTime.Now).ToString() },
            };
        }

        private void RaisePropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
