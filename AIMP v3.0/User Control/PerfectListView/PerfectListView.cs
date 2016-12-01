using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;

namespace AIMP_v3._0.PerfectListView
{
    public class PerfectListView : ListView
    {
        private bool _isOrderingChangedItemsSoruce;

        private Collection<PerfectGridViewColumnHeaderViewModel> _headers;
        public PerfectListView()
        {
            _headers = new Collection<PerfectGridViewColumnHeaderViewModel>();

            Initialized += (sender, args) => SubscribeForPerfectHeader();

            Loaded += (sender, args) => ColumnsSetWidth();
        }
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_isOrderingChangedItemsSoruce)
            {
                foreach (var iHeader in _headers)
                    iHeader.SetItemSource(GetSource());
            }
            base.OnItemsChanged(e);
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

        private IEnumerable<IFilterRow> GetSource()
        {
            var source = ItemsSource as IEnumerable<IFilterRow>;
            if (source == null)
                throw new System.Exception("ItemsSource is not IEnumerable<IFilterRow>");

            return source;
        }

        private void SubscribeForPerfectHeader()
        {
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

                            perfectHeader.OrderingEvent += (s) => 
                            {
                                _isOrderingChangedItemsSoruce = true;
                                ItemsSource = s;
                                _isOrderingChangedItemsSoruce = false;
                            };
                            perfectHeader.FilterClearCahnged += () => 
                            {
                                foreach (var iHeader in _headers)
                                    iHeader.IsFiltering = false;
                            };
                        }
                    }
                }
            }
        }
    }
}