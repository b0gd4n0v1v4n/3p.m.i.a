using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace AIMP_v3._0.PerfectListView
{
    public class PerfectListView : System.Windows.Controls.ListView
    {
        private Collection<PerfectGridViewColumnHeaderViewModel> _headers = new Collection<PerfectGridViewColumnHeaderViewModel>();
 
        public PerfectListView()
        {
            Initialized += (sender, args) => SubscribeForPerfectHeader();
            SizeChanged += (sender, args) => ColumnsSetWidth();
        }

        private void ColumnsSetWidth()
        {
            var gridView = View as GridView;

            if (gridView != null)
            {
                foreach (System.Windows.Controls.GridViewColumn iColumn in gridView.Columns)
                {
                    var perfColumn = iColumn as PerfectGridViewColumn;
                    if (perfColumn != null)
                        perfColumn.Width = (ActualWidth * perfColumn.PercentageWidth) / 100;
                }
            }
        }

        private void SubscribeForPerfectHeader()
        {
            var gridView = View as GridView;

            if (gridView != null)
            {
                foreach (System.Windows.Controls.GridViewColumn iColumn in gridView.Columns)
                {
                    var columnHeader = iColumn.Header as GridViewColumnHeader;

                    if (columnHeader != null)
                    {
                        var perfectHeader = columnHeader.DataContext as PerfectGridViewColumnHeaderViewModel;

                        if (perfectHeader != null)
                        {
                            _headers.Add(perfectHeader);

                            perfectHeader.SetItemSource(ItemsSource);
                            perfectHeader.ApplyFilter();

                            perfectHeader.ItemSourceChanged += (source) =>
                            {
                                ItemsSource = source;

                                foreach (var iHeader in _headers)
                                    //if (perfectHeader != iHeader)
                                        iHeader.SetItemSource(ItemsSource);
                                
                                Items.Refresh();
                            };
                        }
                    }
                }
            }
        }
    }
}