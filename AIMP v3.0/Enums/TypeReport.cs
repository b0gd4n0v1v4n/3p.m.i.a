using System.ComponentModel;

namespace AIMP_v3._0.Enums
{
    public enum TypeReport
    {
        [Description("Сделка")]
        Cash = 1,
        [Description("Сделка в кредит")]
        Credit = 2,
        [Description("Договор купли продажи")]
        DKP = 3,
        [Description("Акт к купли продаже")]
        Akt = 4,
        [Description("Комиссия")]
        Commission = 5
    }
}
