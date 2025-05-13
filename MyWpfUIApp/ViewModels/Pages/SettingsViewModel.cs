using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Localization;
using MyWpfUIApp.Resources;
using System.Globalization;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Appearance;

namespace MyWpfUIApp.ViewModels.Pages
{
    public partial class SettingsViewModel(IStringLocalizer<Translations> localizer) : ObservableRecipient, IRecipient<ValueChangedMessage<CultureInfo>>, INavigationAware
    {
        [ObservableProperty]
        private string _appVersion = string.Empty;
        [ObservableProperty]
        private ApplicationTheme _currentTheme = ApplicationTheme.Unknown;
        [ObservableProperty]
        private IStringLocalizer _localizer = localizer;
        [ObservableProperty]
        private string? _selectedLanguage = CultureInfo.CurrentUICulture.Name;
        private bool _isInitialized = false;

        public Task OnNavigatedToAsync()
        {
            if (!_isInitialized)
                InitializeViewModel();

            IsActive = true;
            return Task.CompletedTask;
        }
        public Task OnNavigatedFromAsync()
        {
            IsActive = false;
            return Task.CompletedTask;
        }
        public void Receive(ValueChangedMessage<CultureInfo> message)
        {
            if (_isInitialized)
                OnPropertyChanged(nameof(Localizer));
        }

        partial void OnSelectedLanguageChanged(string? value)
        {
            if (!_isInitialized || value == null)
                return;
            var cultureInfo = CultureInfo.GetCultureInfo(value);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            Messenger.Send(new ValueChangedMessage<CultureInfo>(cultureInfo));
        }
        private void InitializeViewModel()
        {
            CurrentTheme = ApplicationThemeManager.GetAppTheme();
            AppVersion = $"UiDesktopApp1 - {GetAssemblyVersion()}";

            _isInitialized = true;
        }
        private static string GetAssemblyVersion() => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
        [RelayCommand]
        private void OnChangeTheme(string parameter)
        {
            switch (parameter)
            {
                case "theme_light":
                    if (CurrentTheme == ApplicationTheme.Light)
                        break;

                    ApplicationThemeManager.Apply(ApplicationTheme.Light);
                    CurrentTheme = ApplicationTheme.Light;

                    break;

                default:
                    if (CurrentTheme == ApplicationTheme.Dark)
                        break;

                    ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                    CurrentTheme = ApplicationTheme.Dark;

                    break;
            }
        }
    }
}
