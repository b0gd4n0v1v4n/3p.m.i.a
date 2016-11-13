using System.Windows;

namespace AIMP_v3._0.PerfectListView
{
    public class PerfectGridViewColumnHeader : System.Windows.Controls.GridViewColumnHeader
    {
        private PerfectGridViewColumnHeaderViewModel _perfectColumnHeaderViewModel;
        public PerfectGridViewColumnHeader()
        {
            Initialized += (sender, args) =>
            {
                _perfectColumnHeaderViewModel = new PerfectGridViewColumnHeaderViewModel(SourceColumn, StartFilterItem);
                DataContext = _perfectColumnHeaderViewModel;
            };
        }
        protected override void OnClick()
        {
            _perfectColumnHeaderViewModel.IsOpenMenu = !_perfectColumnHeaderViewModel.IsOpenMenu;     
            base.OnClick();
        }
        public string SourceColumn { get; set; }
        public string StartFilterItem { get; set; }
        public static readonly DependencyProperty StartFilterItemProperty =
    DependencyProperty.Register(
        "StartFilterItem", typeof(string), typeof(PerfectGridViewColumnHeader),null);
    }
}
