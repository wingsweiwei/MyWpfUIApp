using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Localization;
using MyWpfUIApp.Resources;
using System.Globalization;
using Wpf.Ui;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Extensions;

namespace MyWpfUIApp.ViewModels.Pages;

public partial class MyTestPageViewModel(ISnackbarService snackbarService,
    INavigationService navigationService,
    IContentDialogService contentDialogService,
    IStringLocalizer<Translations> localizer) : ObservableRecipient, IRecipient<ValueChangedMessage<CultureInfo>>, INavigationAware
{
    [ObservableProperty]
    private IStringLocalizer _localizer = localizer;

    public Task OnNavigatedToAsync()
    {
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
        OnPropertyChanged(nameof(Localizer));
    }
    [RelayCommand]
    private void Snack()
    {
        snackbarService.Show("Snack Title", "Snack Message");
    }
    [RelayCommand]
    private void Navigate()
    {
        navigationService.NavigateWithHierarchy(typeof(Views.Pages.DataPage));
    }
    [RelayCommand]
    private void Alert()
    {
        contentDialogService.ShowAlertAsync("Alert Title", "Alert Message", "Close");
    }
}
