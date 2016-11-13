using System;
using System.Collections.ObjectModel;
using AIMP_v3._0.PerfectListView;
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
        where TList : Identity,IFilterRow
    {
        public abstract Visibility AddButtonVisible { get; }
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
                OnPropertyChanged("List");
            }
        }

        public TList CurrentItem { get; set; }
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
