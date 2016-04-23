using System.Windows;

namespace AIMP_v3._0.ViewModel.Pages
{
    public interface IPageViewModel
    {
        string Name { get; }
        Command New { get; }
        Command Refresh { get; }
        Command PrintList { get; }
        Visibility PrintButtonVisible { get; }
    }
}
