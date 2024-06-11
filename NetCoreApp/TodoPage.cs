using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NetCoreApp
{
    public partial class TodoListItem : INotifyPropertyChanged
    {
        private string _title = "";
        public string Title 
        { 
            get => _title; 
            set 
            { 
                if (_title != value)
                { 
                    _title = value;
                    RaisePropertyChanged();
                }
            } 
        }

        private string _description = "";
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public partial class TodoPageViewModel : INotifyPropertyChanged
    {
        public string _title = "";
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged();
                }
            }
        }
        public ObservableCollection<TodoListItem> TodoListItems { get; set; } = new()
        {
            new TodoListItem{ Title = "Test Item" }
        };

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public partial class TodoPage : Page
    {
        public TodoPageViewModel viewModel = new();

        public TodoPage() {
            DataContext = viewModel;
            Content = new Grid
            {
                Padding = new Thickness(48, 48, 48, 48),
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition()
                },
                Children = {
                    new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Children =
                        {
                            new TextBox().With(textbox => textbox.SetBinding(TextBox.TextProperty, new Binding {Path  = new PropertyPath("Title"), Mode = BindingMode.TwoWay})),
                            new TextBlock().With(textBlock => textBlock.SetBinding(TextBlock.TextProperty, new Binding {Path  = new PropertyPath("Title"), Mode = BindingMode.OneWay})),
                        }
                    }
                    .With(stackPanel =>
                    {
                        stackPanel.SetValue(Grid.RowProperty, 0);
                    })
                }
            };
        }
    }
}
