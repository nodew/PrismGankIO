using PrismGankIO.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PrismGankIO.Shared.Views
{
    public sealed partial class ArticlesPage : Page
    {
        private ArticlesPageViewModel viewModel;
        public ArticlesPage()
        {
            this.InitializeComponent();
            this.viewModel = this.DataContext as ArticlesPageViewModel;
        }

        private void HandlePivotSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = (Pivot)sender;
            TypedPosts selectedItem = (TypedPosts)pivot.SelectedItem;
            viewModel.HandleSelectedTypeChangedCmd.Execute(selectedItem.Type);
        }
    }
}
