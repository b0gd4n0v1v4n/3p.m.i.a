using System.Windows;

namespace AIMP_v3._0.PerfectListView
{
    public class PerfectGridViewColumnHeader : System.Windows.Controls.GridViewColumnHeader
    {
        private PerfectGridViewColumnHeaderViewModel _perfectColumnHeaderViewModel;
        public PerfectGridViewColumnHeader()
        {
            Loaded+=(s,a)=>
            {
                if (!string.IsNullOrEmpty(StartFilterRow))
                {
                    _perfectColumnHeaderViewModel.StartFilterApply(StartFilterRow);
                }
            };
            Initialized += (sender, args) => 
            {
                _perfectColumnHeaderViewModel = new PerfectGridViewColumnHeaderViewModel(SourceColumn);
                DataContext = _perfectColumnHeaderViewModel;
            };
        }

        protected override void OnClick()
        {
            _perfectColumnHeaderViewModel.IsOpenMenu = !_perfectColumnHeaderViewModel.IsOpenMenu;

            if (_perfectColumnHeaderViewModel.IsOpenMenu)
                _perfectColumnHeaderViewModel.RefreshItemSource();

            base.OnClick();
        }

        public string SourceColumn { get; set; }
        public string StartFilterRow
        {
            get
            {
                return (string)GetValue(StartFilterRowProperty);
            }
            set
            {
                SetValue(StartFilterRowProperty, value);
            }
        }
        public static readonly DependencyProperty StartFilterRowProperty =
    DependencyProperty.Register(
        "StartFilterRow", typeof(string), typeof(PerfectGridViewColumnHeader),null);
    }
}
