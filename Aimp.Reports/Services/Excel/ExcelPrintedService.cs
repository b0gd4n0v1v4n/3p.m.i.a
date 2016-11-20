using System;
using System.Linq;
using System.IO;
using Aimp.Model.PrintedDocument;
using Aimp.Model.PrintedDocument.Templates;
using Aimp.Model.ReportOfClient;
using Aimp.Reports.Interfaces;
using System.Collections.Generic;
using Entities;
using Aimp.Model;

namespace Aimp.Reports.Services.Excel
{
    public class ExcelPrintedService : IExcelPrintedService
    {
        private Excel.DocExcel _excel;
        
        public void Dispose()
        {
            _excel?.Quit();
        }

        public IPrintedDocument GetDocument(IPrintedDocumentTemplate template)
        {
            throw new NotImplementedException();
        }
        public IPrintedDocument GetReportOfClientList(IEnumerable<Bank> banks,IEnumerable<ClientReportListItem> reports)
        {
            string _pathSaveFile = Directory.GetCurrentDirectory();

            string saveFile = _pathSaveFile + "\\tmpPDoc" +
                       Guid.NewGuid().ToString() +
                       ".xlsx";
            try
            {
                _excel = new DocExcel("Отчёт", "Отчёт", Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape);
                _excel.SetColumn(1, 1, "Дата подачи заявки");
                _excel.SetColumn(1, 2, "Ф.И.О. Заемщика");
                _excel.SetColumn(1, 3, "Телефон");
                _excel.SetColumn(1, 4, "Данные ТС");
                _excel.SetColumn(1, 5, "Стоимость ТС");
                _excel.SetColumn(1, 6, "Общий взнос");
                _excel.SetColumn(1, 7, "Программа кредитования");
                int bankColumnsCount = banks.Count();
                var banksList = banks.ToList();

                for (int iColumn = 0;iColumn < bankColumnsCount; iColumn++)
                {
                    _excel.SetColumn(1, iColumn + 8, banksList[iColumn].Name, Microsoft.Office.Interop.Excel.XlOrientation.xlVertical);
                }
                _excel.SetColumn(1, bankColumnsCount + 8, "Статус клиента");
                _excel.SetColumn(1, bankColumnsCount + 9, "Источник");
                int iRow = 1;
                foreach (var iReport in reports)
                {
                    iRow++;
                    _excel.SetValue(iRow,1, iReport.DateReportClient.ToString(DataFormats.DateFormat));
                    _excel.SetValue(iRow, 2, iReport.FullNameReportClient);
                    _excel.SetValue(iRow, 3, iReport.TelefonReportClient);
                    _excel.SetValue(iRow, 4, iReport.TrancportNameReportClient);
                    _excel.SetValue(iRow, 5, iReport.PriceTrancportReportClient);
                    _excel.SetValue(iRow, 6, iReport.TotalContributionReportClient);
                    _excel.SetValue(iRow, 7, iReport.ProgrammCreditReportClient);
                    for (int iColumn = 0; iColumn < bankColumnsCount; iColumn++)
                    {
                        _excel.SetValue(iRow, iColumn + 8, iReport.BankStatusesReportClient[iColumn]);
                    }
                    _excel.SetValue(iRow, bankColumnsCount + 8, iReport.ClientStatusReportClient);
                    _excel.SetValue(iRow, bankColumnsCount+ 9, iReport.SourceInfoReportClient);
                }
                _excel.Save(saveFile);
                
                byte[] file = File.ReadAllBytes(saveFile);
                return new ExcelPrintedDocument()
                {
                    File = file,
                    FileName = $"Отчет клиентов.xslx"
                };
            }
            catch (Exception ex)
            {
                Dispose();

                throw ex;
            }
            finally
            {
                try
                {
                    File.Delete(saveFile);
                    _excel.Quit();
                }
                catch 
                    { }
            }
        }
    }
}
