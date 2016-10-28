using System;
using System.Collections.ObjectModel;
using AIMP_v3._0.ViewModel.Filtering;
using System.Linq.Dynamic;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AIMP_v3._0.Model;
using AIMP_v3._0.DataAccess;

namespace AIMP_v3._0.ViewModel.Pages
{
    public abstract class BasePageViewModel<TList> : BaseViewModel
        where TList : Identity
    {
        public abstract Visibility AddButtonVisible { get; }
        private string _GetColumnName(string name)
        {
            if (name.IndexOf('|') > 0)
            {
                return name.Split('|')[1];
            }

            throw new Exception("Неправильное наименование столбца");
        }

        private Collection<FilteringColumn> _usingColumns;
        protected void SetFilter(string columnName, string[] valuesFilter)
        {
            string expression = string.Empty;

            List<string> prmtr = new List<string>();

            for (int iCheckedItem = 0; iCheckedItem < valuesFilter.Count(); iCheckedItem++)
            {

                if (iCheckedItem == 0)
                    expression = $"{columnName} = @{iCheckedItem}";
                else
                    expression = expression + $" Or {columnName} = @{iCheckedItem}";

                prmtr.Add($"{valuesFilter[iCheckedItem]}");
            }

            FilteringList = new ObservableCollection<TList>(FilteringList.Where(expression, prmtr.ToArray()));
        }
        //КАСТЫЛЬ ДЛЯ REPORT OF CLIENTS PAGE
        public Brush KASTIL_BRASH_FOR_CLIENT_STATUS { get; set; }
        public Brush KASTIL_BRASH_FOR_MANAGER { get; set; }
        //КАСТЫЛЬ ДЛЯ cards trancport 
        public Brush KASTIL_BRASH_FOR_STATUS_CARDTRANCPORT { get; set; }
        protected virtual void ClearFilteres()
        {
            foreach (var iColumn in _usingColumns)
                iColumn.BorderCheckBox.BorderBrush = Brushes.Transparent;
            if (KASTIL_BRASH_FOR_CLIENT_STATUS != null)
                KASTIL_BRASH_FOR_CLIENT_STATUS = Brushes.Transparent;
            OnPropertyChanged("KASTIL_BRASH_FOR_CLIENT_STATUS");
            if (KASTIL_BRASH_FOR_MANAGER != null)
                KASTIL_BRASH_FOR_MANAGER = Brushes.Transparent;
            OnPropertyChanged("KASTIL_BRASH_FOR_MANAGER");
            if (KASTIL_BRASH_FOR_STATUS_CARDTRANCPORT != null)
                KASTIL_BRASH_FOR_STATUS_CARDTRANCPORT = Brushes.Transparent;
            OnPropertyChanged("KASTIL_BRASH_FOR_STATUS_CARDTRANCPORT");
            _usingColumns = new Collection<FilteringColumn>();
        }
        public BasePageViewModel()
        {
            _usingColumns = new Collection<FilteringColumn>();
        }

        public abstract Command PrintList { get; }
        public abstract Command OpenListItem { get; }

        private IEnumerable<TList> _list;
        public IEnumerable<TList> List
        {
            get { return _list; }
            set
            {
                _list = value;

                FilteringList = new ObservableCollection<TList>(value);

                OnPropertyChanged("List");
            }
        }

        public TList CurrentItem { get; set; }

        private ObservableCollection<TList> _filteringList;
        public ObservableCollection<TList> FilteringList
        {
            get
            {
                return _filteringList;
            }
            private set
            {
                _filteringList = value;
                OnPropertyChanged("FilteringList");
            }
        }
        
        public Command SetCurrentFilterCommand
        {
            get
            {
                return new Command((checkBoxParam) =>
                {
                    var checkBox = (checkBoxParam as CheckBox);

                    var border = checkBox.Tag as Border;

                    var columnName = _GetColumnName(border.Tag.ToString());

                    if (checkBox.IsChecked == true)
                    {
                        if (List != null)
                        {
                            var queryList = FilteringList?.Select($"new ({columnName} as Text ,true as IsChecked)").OrderBy("Text");

                            if (queryList != null)
                            {
                                var lst = new List<ListItemFiltering>();

                                foreach (dynamic queryItem in queryList)
                                {
                                    if (lst.FirstOrDefault(x => x.Text == queryItem.Text) == null)
                                        lst.Add(new ListItemFiltering()
                                        {
                                            Text = queryItem.Text,
                                            IsChecked = queryItem.IsChecked
                                        });
                                }

                                var usingColumn = _usingColumns.FirstOrDefault(x => x.Name == columnName);

                                if (usingColumn == null)
                                {
                                    CurrentColumn = new FilteringColumn()
                                    {
                                        Name = columnName,
                                        List = new ObservableCollection<ListItemFiltering>(lst.Distinct()),
                                        BorderCheckBox = border
                                    };
                                }
                                else
                                {
                                    CurrentColumn = usingColumn;
                                    CurrentColumn.List = new ObservableCollection<ListItemFiltering>(lst.Distinct());
                                }
                                OnPropertyChanged("CurrentColumn");
                            }
                        }
                    }
                });
            }
        }

        public Command CheckAllItemCommand
        {
            get
            {
                return new Command((check) =>
                {
                    if (CurrentColumn != null)
                    {
                        var checkedList = CurrentColumn.List.Select(x => new ListItemFiltering()
                        {
                            Text = x.Text,
                            IsChecked = (bool)check
                        });

                        CurrentColumn.List = new ObservableCollection<ListItemFiltering>(checkedList);

                        //todo: not working
                        //foreach (var item in CurrentColumn.List)
                        //    item.IsChecked = (bool)check;
                        
                        OnPropertyChanged("CurrentColumn");
                    }
                });
            }
        }

        private FilteringColumn _currentColumn;
        public FilteringColumn CurrentColumn
        {
            get
            {
                return _currentColumn;
            }
            set
            {
                _currentColumn = value;
                OnPropertyChanged("CurrentColumn");
            }
        }

        public Command ClosePopupCommand
        {
            get
            {
                return new Command((popup) =>
                {
                    (popup as Popup).IsOpen = false;
                });
            }
        }

        public  Command FilteringCommand
        {
            get
            {
                return new Command((popup) =>
                {
                    var border = ((popup as Popup).Tag as Border);
                    
                    var checkedItems = CurrentColumn?.List.Where(x => x.IsChecked)?.ToList();

                    if (checkedItems != null)
                    {
                        if (checkedItems.Count() > 0 && checkedItems.Count != FilteringList.Count)
                        {
                            var values = checkedItems.Select(x => (string)x.Text).ToArray();
                            SetFilter(CurrentColumn.Name, values);
                            if (_usingColumns.FirstOrDefault(x => x.Name == CurrentColumn.Name) == null)
                            {
                                _usingColumns.Add(CurrentColumn);
                            }
                            CurrentColumn.BorderCheckBox.BorderBrush = Brushes.Orange;
                            var cntrl = (popup as Popup);

                            if (cntrl != null)
                                cntrl.IsOpen = false;
                        }
                    }
                });
            }
        }

        public Command SearchFromCurrentColumnCommand
        {
            get
            {
                return new Command((popup) =>
                {
                    if (!string.IsNullOrWhiteSpace(CurrentColumn.SearchText))
                    {
                        var checkedItems = CurrentColumn.List.Where(x => x.Text.ToString()?.ToLower().Contains(CurrentColumn.SearchText)).ToList();

                        if (checkedItems.Any())
                        {
                            if (checkedItems.Count > 0)
                            {
                                var values = checkedItems.Select(x => (string)x.Text).ToArray();
                                SetFilter(CurrentColumn.Name, values);
                                if (_usingColumns.FirstOrDefault(x => x.Name == CurrentColumn.Name) == null)
                                {
                                    _usingColumns.Add(CurrentColumn);
                                }

                                CurrentColumn.BorderCheckBox.BorderBrush = Brushes.Orange;

                                var cntrl = (popup as Popup);

                                if (cntrl != null)
                                    cntrl.IsOpen = false;
                            }
                            else
                            {
                                var cntrl = (popup as Popup);

                                if (cntrl != null)
                                    cntrl.IsOpen = false;

                                MessageBox.Show("Поиск не дал результатов");
                            }
                        }
                    }
                }); 
            }
        }
        public Command ClearFilterCommand
        {
            get
            {
                return new Command((popup) =>
                {
                    if (List != null)
                    {
                        FilteringList = new ObservableCollection<TList>(List);
                    }

                    ClearFilteres();

                    var cntrl = (popup as Popup);

                    if (cntrl != null)
                        cntrl.IsOpen = false;
                });
            }
        }

        public Type Type
        {
            get { return GetType(); }
        }

        public bool NewIsEnabled
        {
            get { return CurrentUser.IsAdmin ? true : CurrentUser.IsAdd; }
        }

        public bool RefreshIsEnabled
        {
            get { return CurrentUser.IsAdmin ? true : CurrentUser.IsView; }
        }
        
    }
}
