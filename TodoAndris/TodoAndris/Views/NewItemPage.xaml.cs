using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TodoAndris.Models;

namespace TodoAndris.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            // Create new item and bind it to the page
            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description.",
                Progress = 0,
                Days = 30
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            // This message will be caught by an observer in ItemsViewModel.cd
            MessagingCenter.Send(this, "AddItem", Item);
            // Trigger back navigation
            await Navigation.PopModalAsync();
        }
    }
}