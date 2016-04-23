namespace AIMP_v3._0.ViewModel.Pages
{
    public class CreditTransactionPageViewModel : BasePageViewModel
    {
        public override string Name { get { return "СДЕЛКА В КРЕДИТ"; } }
        public override Command New { get; }
        public override Command Delete { get; }
        public override Command Refresh { get; }
        public override Command Filter { get; set; }
    }
}
