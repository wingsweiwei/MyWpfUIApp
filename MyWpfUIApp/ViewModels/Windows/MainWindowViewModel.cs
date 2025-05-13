using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Localization;
using MyWpfUIApp.Resources;
using System.Globalization;
using Wpf.Ui;

namespace MyWpfUIApp.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<CultureInfo>>
    {
        [ObservableProperty]
        private IStringLocalizer<Translations> _localizer;
        private readonly INavigationService _navigationService;

        public MainWindowViewModel(INavigationService navigationService, IStringLocalizer<Translations> localizer)
        {
            _navigationService = navigationService;
            _localizer = localizer;
            IsActive = true;
        }

        public void Receive(ValueChangedMessage<CultureInfo> message)
        {
            OnPropertyChanged(nameof(Localizer));
            _navigationService.GoBack();
            _navigationService.Navigate(typeof(Views.Pages.SettingsPage));
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
        private static void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
