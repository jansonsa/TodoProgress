using System;
using TodoAndris.Models;
using TodoAndris.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoAndris.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{

        CompletedViewModel viewModel;

        public AboutPage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new CompletedViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;
            // Navigate to item details page
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}