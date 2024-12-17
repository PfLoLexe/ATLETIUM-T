using System;
using ATLETIUM_T.viewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ATLETIUM_T;

public class MessageTemplateSelector : DataTemplateSelector, IMarkupExtension {
    public DataTemplate SenderTemplate { get; set; }
    public DataTemplate RecipientTemplate { get; set; }
    public object ProvideValue(IServiceProvider serviceProvider) => this;

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
        return item is Message message ? message.Sender.Name == "Me" ? SenderTemplate : RecipientTemplate : null;
    }
}