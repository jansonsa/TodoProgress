using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TodoAndris.Models;
using TodoAndris.Services;
using Xamarin.Forms;

namespace TodoAndris
{
    class CompleteClicked : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            // When a Command is binded to an element, IsEnabled will not disable the button,
            // instead, to disable the button we have to use this method and return false
            var item = (Item)parameter;
           
            return item == null ? false : item.IsEnabled;
        }

        public void Execute(object parameter)
        {
            // The user clicked "I Did This Today"
            Item item = (Item)parameter;
            if (item == null)
                return;
            // Get our data store
            IDataStore<Item> DataStore = DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
            // Increase the progress, add the last completed date as now and
            // trigger property changed for isEnabled
            item.Progress += 1;
            item.LastCompleted = DateTime.Now;
            item.IsEnabled = false;
            // Send the new update to the server
            DataStore.UpdateItemAsync(item);
        }
    }
}
