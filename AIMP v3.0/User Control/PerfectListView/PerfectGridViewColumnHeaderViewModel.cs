using System;
using System.Collections;
using System.ComponentModel;
using System.Linq.Dynamic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using AIMP_v3._0.Annotations;
using AIMP_v3._0.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using AIMP_v3._0.User_Control.PerfectListView;

namespace AIMP_v3._0.PerfectListView
{
    public class GroupingFilterRow : BaseViewModel
    {
        public IEnumerable<IFilterRow> OriginalRows { get; set; }
               
        private bool _isSelected;
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; OnPropertyChanged(); } }

        public dynamic Value { get; set; }
        public string Text { get; set; }
    }
    public class PerfectGridViewColumnHeaderViewModel : BaseViewModel
    {
        public ColumnDataType DataType { get; private set; }
        public string ColumnName { get; private set; }
        
        private IEnumerable<IFilterRow> _originalSource;
        public ObservableCollection<GroupingFilterRow> Rows { get; private set; }
        private bool _isOpenMenu;
        public bool IsOpenMenu
        {
            get { return _isOpenMenu; }
            set { _isOpenMenu = value; OnPropertyChanged(); }
        }
 
        public ICommand OrderingAscCommand { get; private set; }
        public ICommand OrderingDescCommand { get; private set; }

        public ICommand FilterApplyCommand { get; private set; }
        public ICommand ClearFilterCommand { get; private set; }
        
        public ICommand SelectedAllItemCommand { get; private set; }
        public bool IsSelectedAll { get; set; }

        public string SearchText { get; set; }
        public ICommand SearchCommand { get; private set; }

        private bool _isFiltering;
        public bool IsFiltering { get {
                return _isFiltering;
            } set { _isFiltering = value; OnPropertyChanged(); } }

        public event Action<IEnumerable<IFilterRow>> OrderingEvent;
        public event Action FilterClearCahnged;

        public PerfectGridViewColumnHeaderViewModel(string columnName,ColumnDataType type)
        {
            DataType = type;
            ColumnName = columnName;
            OrderingAscCommand = new Command((x) => Ordering(true));
            OrderingDescCommand = new Command((x) => Ordering(false));
            FilterApplyCommand = new Command((x)=> FilterApply());
            ClearFilterCommand = new Command((x)=> ClearFilter());
            SelectedAllItemCommand = new Command((x)=>SelectedAllItem());
            SearchCommand = new Command((x) => Search());
        }

        public void SetItemSource(IEnumerable<IFilterRow> itemSource)
        {
            _originalSource = itemSource;
            RefreshItemSource();
            IsFiltering = false;
        }

        private void RefreshItemSource()
        {
            try
            {
                Rows = new ObservableCollection<GroupingFilterRow>();
                foreach (dynamic iGroupColumn in _originalSource.GroupBy(ColumnName, "it").Select("new (it.Key as Text,it as GroupingRows)"))
                {
                    var filterRow = new GroupingFilterRow()
                    {
                        OriginalRows = iGroupColumn.GroupingRows,
                        Value = iGroupColumn.Text,
                        Text = iGroupColumn.Text?.ToString()
                    };

                    Rows.Add(filterRow);
                }

                OnPropertyChanged("Rows");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void StartFilterApply(string filterText)
        {
            try
            {
                foreach (var iRow in Rows)
                    if (iRow.Text == filterText)
                        foreach (var iOriginal in _originalSource.Where(x => !iRow.OriginalRows.Select(o=>o.Id).Contains(x.Id)))
                            iOriginal.IsVisible = false;

                IsFiltering = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SelectedAllItem()
        {
            foreach (var iRow in Rows)
                iRow.IsSelected = IsSelectedAll;
        }

        private void Ordering(bool asc)
        {
            try
            {
                Collection<IFilterRow> orderingRows = new Collection<IFilterRow>();

                if (asc)
                {
                    foreach(var iOrd in Rows.OrderBy(x => x.Value))
                        foreach(var iG in iOrd.OriginalRows)
                            orderingRows.Add(iG);
                }
                else
                {
                    foreach (var iOrd in Rows.OrderByDescending(x => x.Value))
                        foreach (var iG in iOrd.OriginalRows)
                            orderingRows.Add(iG);
                }

                if (OrderingEvent != null)
                    OrderingEvent(orderingRows);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearFilter()
        {
            foreach (IFilterRow iRow in _originalSource)
                iRow.IsVisible = true;

            IsFiltering = false;

            if (FilterClearCahnged != null)
                FilterClearCahnged();
        }
        private void FilterApply()
        {
            try
            {
                foreach (var iRow in Rows)
                    foreach (var iOriginalRow in _originalSource)
                        if (iRow.OriginalRows.Select(o=>o.Id).Contains(iOriginalRow.Id))
                            iOriginalRow.IsVisible = iRow.IsSelected;

                IsFiltering = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                try
                {
                    foreach (var iRow in Rows.Where(x=>!x.Text.Contains(SearchText)))
                        foreach (var iOriginalRow in _originalSource)
                            if (iRow.OriginalRows.Select(o => o.Id).Contains(iOriginalRow.Id))
                                iOriginalRow.IsVisible = false;

                    IsFiltering = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
