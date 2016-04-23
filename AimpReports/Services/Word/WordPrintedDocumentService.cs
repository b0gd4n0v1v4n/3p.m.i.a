using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Models.PrintedDocument;
using Models.PrintedDocument.Templates;
using System.Configuration;

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

        public IPrintedDocument GetDocument(IPrintedDocumentTemplate template)
        {
            string _pathSaveFile = @"D:\AimpFiles\temp\PrintedDocuments";
            string fileName = _pathSaveFile + "\\tmpPDoc" +
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
