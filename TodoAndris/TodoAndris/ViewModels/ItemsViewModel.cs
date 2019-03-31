﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TodoAndris.Models;
using TodoAndris.Views;

namespace TodoAndris.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Rituals";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                try
                {
                    var _item = item as Item;
                    // Crosscheck if the item is not somehow completed
                    // and add it to the list with other items
                    if (!_item.IsCompleted)
                    {
                        Items.Add(_item);
                    }

                    await DataStore.AddItemAsync(_item);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException);
                }
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "DeleteItem", async (obj, item) =>
            {
                // We have this method in both Active and Completed viewModels.
                // This is why it is run in a try-catch block, because it might be executed
                // twice. We better catch an error when executing it twice rather than 
                // risk the item not being deleted
                try
                {
                    var _item = item as Item;

                    // Remove item from the list
                    Items.Remove(_item);

                    // Delete item from the server
                    await DataStore.DeleteItemAsync(_item.Id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException);
                }
            });
        }



        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    // Only add the items that are still active
                    if (!item.IsCompleted)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}