using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Enums;
using AimpReports.Templates;

namespace TestAccordDb2AndDb3Version.version3
{
    public class VersionThree : IReport
    {
        public Dictionary<string, string> Get(TypeReport type,int idTransaction)
        {
            Dictionary<string,string> result = new Dictionary<string, string>();

            switch (type)
            {
                case TypeReport.Akt:
                    {
                        //result = new AktTransactionPrintedDocumentTemplate(idTransaction, null).LabelValues();
                    }
                    break;
                case TypeReport.Cash:
                    //result = new CashReport(idTransaction).GetKeyValue();
                    break;
                case TypeReport.Commission:
                    //result = new CommissionReport(idTransaction).GetKeyValue();
                    break;
                case TypeReport.Credit:
                    //result = new CreditReport(idTransaction).GetKeyValue();
                    break;
                case TypeReport.DKP:
                    //result = new DkpReport(idTransaction).GetKeyValue();
                    break;
            }
            return result;
        } 
    }
}
