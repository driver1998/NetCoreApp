﻿using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using WinRT;

namespace NetCoreApp
{
    [GeneratedBindableCustomProperty]
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
    public partial class TodoPage : Page
    {
        public ObservableCollection<TodoListItem> TodoListItems { get; set; } = new()
        {
            new() { Title = "Hello world", Description = "This is a quick and dirty demo" },
            new() { Title = "Content won't be saved", Description = "Why would it" },
        };

        public TodoPage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                Console.WriteLine("hello world");
            };
        }

        public async void Add_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new TodoListItem();
            EditDialog.DataContext = newItem;
            var result = await EditDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                TodoListItems.Add(newItem);
            }
            EditDialog.DataContext = null;
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement)?.DataContext as TodoListItem;
            if (item == null) return;

            var scratchItem = new TodoListItem()
            {
                Title = item.Title,
                Description = item.Description
            };
            EditDialog.DataContext = scratchItem;
            var result = await EditDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                item.Title = scratchItem.Title;
                item.Description = scratchItem.Description;
            }
            EditDialog.DataContext = null;
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement)?.DataContext as TodoListItem;
            if (item == null) return;
            TodoListItems.Remove(item);
        }
    }
}
