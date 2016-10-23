using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.PrintedDocument;
using Models.PrintedDocument.Templates;
using Models.Entities;
using Models.ReportOfClient;
using System.IO;

namespace AimpReports.Services.Excel
{
    public class ExcelPrintedDocumentService : IPrintedDocumentService
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
        public IPrintedDocument GetReportOfClientList(ClientReports reports)
        {
            string _pathSaveFile = Directory.GetCurrentDirectory();
            string fileName = _pathSaveFile + "\\tmpPDoc" +
                       Guid.NewGuid().ToString() +
                       ".xlsx";
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
                int bankColumnsCount = reports.Banks.Count();
                var banks = reports.Banks.ToList();
                for (int iColumn = 8;iColumn < bankColumnsCount; iColumn++)
                {
                    _excel.SetColumn(1, iColumn, banks[iColumn].Name, Microsoft.Office.Interop.Excel.XlOrientation.xlVertical);
                }
                _excel.SetColumn(1, bankColumnsCount, "Статус клиента");
                _excel.SetColumn(1, bankColumnsCount + 1, "Источник");
                int iRow = 1;
                foreach (var iReport in reports.Items)
                {
                    iRow++;
                    _excel.SetValue(iRow,1, iReport.DateReportClient);
                    _excel.SetValue(iRow, 2, iReport.FullNameReportClient);
                    _excel.SetValue(iRow, 3, iReport.TelefonReportClient);
                    _excel.SetValue(iRow, 4, iReport.TrancportNameReportClient);
                    _excel.SetValue(iRow, 5, iReport.PriceTrancportReportClient);
                    _excel.SetValue(iRow, 6, iReport.TotalContributionReportClient);
                    _excel.SetValue(iRow, 7, iReport.ProgrammCreditReportClient);
                    for (int iColumn = 8; iColumn < bankColumnsCount; iColumn++)
                    {
                        _excel.SetValue(iRow, iColumn, iReport.BankStatusesReportClient[iColumn]);
                    }
                    _excel.SetValue(iRow, bankColumnsCount, iReport.ClientStatusReportClient);
                    _excel.SetValue(iRow, bankColumnsCount+1, iReport.SourceInfoReportClient);
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
                    File.Delete(fileName);
                    File.Delete(saveFile);
                    _excel.Quit();
                }
                catch 
                    { }
            }
        }
    }
}
