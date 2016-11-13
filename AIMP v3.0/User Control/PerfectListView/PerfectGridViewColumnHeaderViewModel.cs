using System;
using System.Collections;
using System.ComponentModel;
using System.Linq.Dynamic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using AIMP_v3._0.Annotations;
using AIMP_v3._0.ViewModel;

namespace AIMP_v3._0.PerfectListView
{
    public class PerfectGridViewColumnHeaderViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly string _startFilterItem;
        public string ColumnName { get; private set; }

        public IEnumerable ItemSource { get; private set; }
        
        private bool _isOpenMenu;
        public bool IsOpenMenu
        {
            get { return _isOpenMenu; }
            set { _isOpenMenu = value; OnPropertyChanged();}
        }

        public event Action<IEnumerable> ItemSourceChanged;
 
        public ICommand OrderingAscCommand { get; private set; }
        public ICommand OrderingDescCommand { get; private set; }

        public ICommand FilterApplyCommand { get; private set; }
        public ICommand ClearFilterCommand { get; private set; }
        
        public ICommand SelectedAllItemCommand { get; private set; }
        public bool IsSelectedAll { get; set; }

        public PerfectGridViewColumnHeaderViewModel(string columnName,string startFilterItem = null)
        {
            _startFilterItem = startFilterItem;
            ColumnName = columnName;
            OrderingAscCommand = new Command((x) => Ordering(true));
            OrderingDescCommand = new Command((x) => Ordering(false));
            FilterApplyCommand = new Command((x)=>FilterApply());
            ClearFilterCommand = new Command((x)=>ClearFilter());
            SelectedAllItemCommand = new Command((x)=>SelectedAllItem());   
        }

        public void SetItemSource(IEnumerable itemSource)
        {
            ItemSource = itemSource;
            OnPropertyChanged("ItemSource");
        }

        public void ApplyFilter()
        {
            if (!string.IsNullOrEmpty(_startFilterItem))
                FilterApply(_startFilterItem); 
        }

        private void SelectedAllItem()
        {
            foreach (var item in ItemSource)
            {
                var row = (item as IFilterRow);

                if (row == null || !row.IsVisible) continue;

                row.IsSelected = IsSelectedAll;
            }
            ItemSourceChanged(ItemSource.Where("1 = 1"));
        }

        private void Ordering(bool asc)
        {
            try
            {
                ItemSourceChanged(ItemSource.OrderBy(string.Format(asc ? "{0} ASC" : "{0} DESC", ColumnName)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FilterApply()
        {
            try
            {
                foreach (var item in ItemSource)
                {
                    var row = (item as IFilterRow);

                    if (row == null) continue;
                    
                    row.IsVisible = row.IsSelected;
                }
                ItemSourceChanged(ItemSource.Where("1 = 1"));
                IsOpenMenu = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FilterApply(string filterItem)
        {
            try
            {
                foreach (var item in ItemSource.Where(string.Format("{0} != {1}", ColumnName, filterItem)))
                {
                    var row = (item as IFilterRow);

                    if (row == null) continue;

                    row.IsVisible = false;
                }

                if (ItemSourceChanged != null)
                    ItemSourceChanged(ItemSource.Where("1 = 1"));

                IsOpenMenu = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearFilter()
        {
            try
            {
                foreach (var item in ItemSource)
                {
                    var row = (item as IFilterRow);

                    if (row != null)
                    {
                        row.IsVisible = true;
                        row.IsSelected = false;
                    }
                }
                ItemSourceChanged(ItemSource.Where("1 = 1"));
                IsOpenMenu = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
