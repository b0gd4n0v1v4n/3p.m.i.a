using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace AIMP_v3._0.ViewModel.Filtering
{
    public class FilteringColumn
    {
        public string Name { get; set; }

        public string SearchText { get; set; }

        public Border BorderCheckBox { get; set; }

        public ObservableCollection<ListItemFiltering> List { get; set; }
    }
}
