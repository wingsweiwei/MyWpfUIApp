using MyWpfUIApp.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace MyWpfUIApp.Views.Pages
{
    /// <summary>
    /// MyTestPage.xaml 的交互逻辑
    /// </summary>
    public partial class MyTestPage : INavigableView<MyTestPageViewModel>
    {
        public MyTestPageViewModel ViewModel { get; }

        public MyTestPage(MyTestPageViewModel myTestPageViewModel)
        {
            DataContext = ViewModel = myTestPageViewModel;
            InitializeComponent();
        }
    }
}
