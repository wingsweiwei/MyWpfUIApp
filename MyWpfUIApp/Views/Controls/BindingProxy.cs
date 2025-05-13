﻿namespace MyWpfUIApp.Views.Controls;

public class BindingProxy : Freezable
{
    public object Data
    {
        get { return (object)GetValue(DataProperty); }
        set { SetValue(DataProperty, value); }
    }
    public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy));
    protected override Freezable CreateInstanceCore()
    {
        return new BindingProxy();
    }
}
