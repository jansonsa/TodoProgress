using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TodoAndris.Models;
using TodoAndris.ViewModels;

namespace TodoAndris.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item",
                Description = "This is an item description.",
                Progress = 0,
                Days = 1
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {

            // This message will be caught by an observer
            // We can't just delete the item here, because
            // The previous page will try to find it and raise
            // an array out of bounds exception
            MessagingCenter.Send(this, "DeleteItem", viewModel.Item);
            await Navigation.PopAsync();
        }
    }
}