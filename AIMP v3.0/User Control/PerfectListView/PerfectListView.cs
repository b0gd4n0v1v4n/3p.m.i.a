using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace AIMP_v3._0.PerfectListView
{
    public class PerfectListView : System.Windows.Controls.ListView
    {
        private Collection<PerfectGridViewColumnHeaderViewModel> _headers;
        public PerfectListView()
        {
            _headers = new Collection<PerfectGridViewColumnHeaderViewModel>();
            //SizeChanged += (sender, args) => ColumnsSetWidth();
            
            Loaded += (sender, args) => { ColumnsSetWidth(); SubscribeForPerfectHeader(); };
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
                        if (perfColumn.PercentageWidth > 0)
                            perfColumn.Width = (ActualWidth * perfColumn.PercentageWidth) / 100;
                }
            }
        }

        private void SubscribeForPerfectHeader()
        {
            var source = ItemsSource as IEnumerable<IFilterRow>;
            if (source == null)
                throw new System.Exception("ItemsSource is not IEnumerable<IFilterRow>");

            var gridView = View as GridView;

            if (gridView != null)
            {
                foreach (System.Windows.Controls.GridViewColumn iColumn in gridView.Columns)
                {
                    var columnHeader = iColumn.Header as PerfectGridViewColumnHeader;

                    if (columnHeader != null)
                    {
                        var perfectHeader = columnHeader.DataContext as PerfectGridViewColumnHeaderViewModel;

                        if (perfectHeader != null)
                        {
                            _headers.Add(perfectHeader);

                            perfectHeader.ItemSourceChanged += (s) => ItemsSource = s;
                            perfectHeader.FilterClearCahnged += () => 
                            {
                                foreach (var iHeader in _headers)
                                    iHeader.IsFiltering = false;
                            };
                            perfectHeader.SetItemSource(source);
                        }
                    }
                }
            }
        }
    }
}