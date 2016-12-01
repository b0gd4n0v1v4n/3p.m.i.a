using AIMP_v3._0.DataAccess;
using AIMP_v3._0.User_Control.PerfectListView;
using System.Windows;

namespace AIMP_v3._0.PerfectListView
{
    public class PerfectGridViewColumnHeader : System.Windows.Controls.GridViewColumnHeader
    {
        private bool _isOneLoaded;

        private PerfectGridViewColumnHeaderViewModel _perfectColumnHeaderViewModel;
        public PerfectGridViewColumnHeader()
        {
            Loaded+=(s,a)=>
            {
                if (!string.IsNullOrEmpty(StartFilterItem))
                {
#warning не биндится поле, ход конем
                    if (!_isOneLoaded) {
                        _isOneLoaded = true;
                        if (StartFilterItem == "manager")
                            _perfectColumnHeaderViewModel.StartFilterApply(CurrentUser.LastName);
                        else
                            _perfectColumnHeaderViewModel.StartFilterApply(StartFilterItem);
                    }
                }
            };
            Initialized += (sender, args) => 
            {
                _perfectColumnHeaderViewModel = new PerfectGridViewColumnHeaderViewModel(SourceColumn,DataType);
                DataContext = _perfectColumnHeaderViewModel;
            };
        }

        protected override void OnClick()
        {
            _perfectColumnHeaderViewModel.IsOpenMenu = !_perfectColumnHeaderViewModel.IsOpenMenu;

            //if (_perfectColumnHeaderViewModel.IsOpenMenu)
            //    _perfectColumnHeaderViewModel.RefreshItemSource();

            base.OnClick();
        }
        public ColumnDataType DataType { get; set; }
        public string SourceColumn { get; set; }
        public string StartFilterItem
        {
            get
            {
                return (string)GetValue(StartFilterItemProperty);
            }
            set
            {
                SetValue(StartFilterItemProperty, value);
            }
        }
        public static readonly DependencyProperty StartFilterItemProperty =
    DependencyProperty.Register(
        "StartFilterItem", typeof(string), typeof(PerfectGridViewColumnHeader), new PropertyMetadata(string.Empty));
    }
}
