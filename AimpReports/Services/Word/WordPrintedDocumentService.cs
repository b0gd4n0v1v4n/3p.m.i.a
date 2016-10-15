using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Models.PrintedDocument;
using Models.PrintedDocument.Templates;
using System.Configuration;
using System.Text;
using System.Threading;

namespace AimpReports.Services.Word
{
    public class WordPrintedDocumentService
        : IPrintedDocumentService
    {
        private WordDocument _document;
        private string _GetConfigProperty(string name)
        {
            string configPath = GetType().Assembly.Location;
            var config = ConfigurationManager.OpenExeConfiguration(configPath);
            return config.AppSettings.Settings[name].Value;
        }
        public void Dispose()
        {
            _document?.Dispose();
        }
#warning ДУБЛИ В БАЗЕ, ТАБЛИЦА КОНТРАГЕНТОВ, И ЕЩЕ НАВЕРНО ГДЕ ТО БЛЯ
        public IPrintedDocument GetDocument(IPrintedDocumentTemplate template)
        {
            string _pathSaveFile = Directory.GetCurrentDirectory();
            string fileName = _pathSaveFile + "\\" +
                       Guid.NewGuid().ToString() +
                       ".doc";
            try
            {

                File.WriteAllBytes(fileName, template.TemplateFile);
                _document = new WordDocument(fileName);
                foreach (var iKeyValue in template.LabelValues)
                {
                    string replace = iKeyValue.Value ?? string.Empty;
                    string find = "[" + iKeyValue.Key + "]";
                    _document.ReplaceAllStrings(find, replace);
                }
                _document.Save(fileName);
                _document.Dispose();
                byte[] file = File.ReadAllBytes(fileName);

                return new WordPrintedDocument()
                {
                    File = file
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
                }
                catch { }
            }
        }
    }
}
