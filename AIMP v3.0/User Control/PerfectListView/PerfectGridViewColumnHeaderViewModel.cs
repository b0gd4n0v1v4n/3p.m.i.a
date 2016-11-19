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

namespace AIMP_v3._0.PerfectListView
{
    public class FilterRow : BaseViewModel
    {
        public IEnumerable<int> Ids { get; set; }
               
        private bool _isSelected;
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; OnPropertyChanged(); } }

        public dynamic Text { get; set; }
    }
    public class PerfectGridViewColumnHeaderViewModel : BaseViewModel
    {
        public string ColumnName { get; private set; }

        //private IEnumerable<IFilterRow> _originalSource;
        private IEnumerable _originalSource;
        public ObservableCollection<FilterRow> Rows { get; private set; }
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

        public bool IsFiltering { get; set; }

        public event Action<IEnumerable> ItemSourceChanged;

        public PerfectGridViewColumnHeaderViewModel(string columnName)
        {
            ColumnName = columnName;
            OrderingAscCommand = new Command((x) => Ordering(true));
            OrderingDescCommand = new Command((x) => Ordering(false));
            FilterApplyCommand = new Command((x)=> FilterApply());
            ClearFilterCommand = new Command((x)=> ClearFilter());
            SelectedAllItemCommand = new Command((x)=>SelectedAllItem());
        }

        public void SetItemSource(IEnumerable<IFilterRow> itemSource)
        {
            _originalSource = itemSource;
            RefreshItemSource();
        }

        public void RefreshItemSource()
        {
            try
            {
                Rows = new ObservableCollection<FilterRow>();
                foreach (dynamic iGroupColumn in _originalSource.GroupBy(ColumnName, "it").Select("new (it.Key as Text,it as Ids)"))
                {
                    var ids = new Collection<int>();

                    foreach (dynamic iColumn in iGroupColumn.Ids)
                    {
                        ids.Add(iColumn.Id);
                    }
                    var filterRow = new FilterRow()
                    {
                        Ids = ids,
                        Text = iGroupColumn.Text
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
                        foreach (var iId in iRow.Ids)
                            foreach(dynamic iOrifginRow in _originalSource.Where(string.Format("Id = {0}",iId)))
                               iOrifginRow.IsVisible = false;

                IsFiltering = true;
                OnPropertyChanged("IsFiltering");
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
                if (ItemSourceChanged != null)
                    ItemSourceChanged(_originalSource.OrderBy(asc ? "Text ASC" : "Text DESC"));
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
            OnPropertyChanged("IsFiltering");
        }
        private void FilterApply()
        {
            try
            {
                //foreach (var iRow in Rows)
                //    foreach (var iOriginalRow in _originalSource)
                //        if (iRow.Ids.Contains(iOriginalRow.Id))
                //            iOriginalRow.IsVisible = iRow.IsSelected;

                IsFiltering = true;
                OnPropertyChanged("IsFiltering");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
