using System;
using Microsoft.Office.Interop.Excel;
using ExcelApp = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Aimp.PrintedDocument.DocumentBuilders.Excel
{
    public class DocExcel
    {
        private ExcelApp._Application excelapp;
        public ExcelApp.Worksheet _currentSheet;//текущий лист
        public ExcelApp.Workbook excelBook;
        private ExcelApp.Range range; //текущая ячейка

        public bool visible
        {
            get { return excelapp.Visible; }
            set { excelapp.Visible = value; }
        }
        public int countSheet
        {
            get
            {
                return excelBook.Sheets.Count;
            }
            set
            {

            }
        }
        public void Save(string path)
        {
            //            var misValue = Type.Missing;
            //            excelBook.SaveAs(path, XlFileFormat.xlCSV,
            //misValue, misValue, false, false, XlSaveAsAccessMode.xlNoChange,
            //misValue, misValue, misValue, misValue, misValue);
            excelBook.SaveCopyAs(path);
        }
        public void DeleteColumn(string column)
        {
            ExcelApp.Range range = (ExcelApp.Range)excelapp.get_Range(column, Missing.Value);
            range.EntireColumn.Delete(Missing.Value);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
        }
        public void SetValue(int x, int y, object value)
        {
            range = _currentSheet.Cells[x, y];
            range.Font.Size = 8;
            range.NumberFormat = "@";
            range.Value = value;
            range.WrapText = true;
            range.Borders.LineStyle = ExcelApp.XlLineStyle.xlContinuous;
        }
        public void SetRangeValue(string range, object[,] value)
        {
            this.range = (ExcelApp.Range)_currentSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty,
                null, _currentSheet, new object[] { range });
            range.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, range, new object[] { value });
        }
        //преобразует количество строк и столбцов входной таблицы в диапазон Excel(A1:N94)
        public string ReturnRange(int rows, int columns)
        {
            int dividend = columns;
            string columnName = String.Empty;
            int modulo;
            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            string range = "A1:" + columnName + rows.ToString();

            return range;
        }
        public void SetColumn(int x, int y, object value)
        {
            range = _currentSheet.Cells[x, y];
            range.Font.Size = 8;
            range.NumberFormat = "@";
            range.Value = value;
            range.WrapText = true;
            range.EntireColumn.ColumnWidth = 8;
            range.Borders.LineStyle = ExcelApp.XlLineStyle.xlContinuous;
            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
        }
        public void SetColumn(int x, int y, object value, ExcelApp.XlOrientation orientation)
        {
            range = _currentSheet.Cells[x, y];
            range.Font.Size = 8;
            range.NumberFormat = "@";
            range.Value = value;
            range.Orientation = orientation;
            range.EntireColumn.ColumnWidth = 2;
            range.Borders.LineStyle = ExcelApp.XlLineStyle.xlContinuous;
            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
        }

        public DocExcel(string nameExcel, string nameSheet, XlPageOrientation orientation) //конструктор
        {
            excelapp = new ExcelApp.Application();
            excelapp.Caption = nameExcel;
            excelBook = excelapp.Workbooks.Add(Type.Missing);
            _currentSheet = (ExcelApp.Worksheet)excelBook.Sheets.get_Item(1);
            _currentSheet.PageSetup.Orientation = orientation;
        }
        public void SearchAndReplace(string search, string replace)
        {
            this.excelapp.DisplayAlerts = false;
            foreach (ExcelApp._Worksheet sheet in this.excelapp.Worksheets)
            {
                sheet.Cells.Replace(search, replace, ExcelApp.XlLookAt.xlWhole, ExcelApp.XlSearchOrder.xlByRows, true, Type.Missing, Type.Missing, Type.Missing);
            }
        }
        public DocExcel(string fileName)
        {
            excelapp = new ExcelApp.Application();
            excelBook = excelapp.Workbooks.Open(fileName);
        }
        public void Quit()
        {

            //if (excelapp != null)
            //{
            //    excelapp.DisplayAlerts = false;
            //    excelapp.Quit();
            //}
            //excelBook?.Close(false,null,null);
            Process[] ps2 = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (Process p2 in ps2)
            {
                p2.Kill();
            }
        }
    }
}
