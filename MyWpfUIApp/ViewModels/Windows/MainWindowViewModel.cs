using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Localization;
using MyWpfUIApp.Resources;
using MyWpfUIApp.Views.Windows;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Navigation;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Tray.Controls;

namespace MyWpfUIApp.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<CultureInfo>>
    {
        [ObservableProperty]
        private IStringLocalizer<Translations> _localizer;
        public MainWindow? Window { get; set; }
        private readonly INavigationService _navigationService;
        private bool _hasInit;

        public MainWindowViewModel(INavigationService navigationService, IStringLocalizer<Translations> localizer)
        {
            _navigationService = navigationService;
            _localizer = localizer;
            Init();
            IsActive = true;
        }

        public void Receive(ValueChangedMessage<CultureInfo> message)
        {
            OnPropertyChanged(nameof(Localizer));
            _navigationService.GoBack();
            _navigationService.Navigate(typeof(Views.Pages.SettingsPage));
        }

        //[ObservableProperty]
        //private string _applicationTitle = localizer["ApplicationTitle"];

        //[ObservableProperty]
        //private ObservableCollection<object> _menuItems =
        //[
        //    new NavigationViewItem()
        //    {
        //        Content = "Home",
        //        Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
        //        TargetPageType = typeof(Views.Pages.DashboardPage)
        //    },
        //    new NavigationViewItem()
        //    {
        //        Content = "Data",
        //        Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
        //        TargetPageType = typeof(Views.Pages.DataPage)
        //    }
        //];

        //[ObservableProperty]
        //private ObservableCollection<NavigationViewItem> _footerMenuItems = [];

        //[ObservableProperty]
        //private ObservableCollection<MenuItem> _trayMenuItems = new()
        //{
        //    new MenuItem { Header = "Home", Tag = "tray_home" }
        //};

        private void Init()
        {
            _hasInit = false;
            //FooterMenuItems =
            //[
            //    new NavigationViewItem()
            //    {
            //        Content = Localizer["Settings"].ToString(),
            //        Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            //        TargetPageType = typeof(Views.Pages.SettingsPage)
            //    }
            //];
            _hasInit = true;
        }

        [RelayCommand]
        private void Test()
        {
            var cultureInfo = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            OnPropertyChanged(nameof(Localizer));
        }
        [RelayCommand]
        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
