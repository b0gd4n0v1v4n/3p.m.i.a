﻿using System;
using MSWord = Microsoft.Office.Interop.Word;
using System.Reflection;


namespace Aimp.Reports.Services.Word
{
    /// Версия 1.8
    // Word запускается ОТДЕЛЬНЫМ приложением, которое должно быть установелено на компьютере, класс просто управляет им через интерфейс Word Interoperability, в проекте должна быть ссылка на Microsoft.Office.Interop.Word, соотвествующая библиотека .dll должна быть в папке с программой, ----- класс позволяет создать новый документ по шаблону, произвести поиск и замену строк (одно вхождение или все), изменить видимость документа, закрыть документ
    public class WordDocument : IDisposable
    {
        // фиксированные параметры для передачи приложению Word
        private Object _missingObj = System.Reflection.Missing.Value;
        private Object _trueObj = true;
        private Object _falseObj = false;

        //рабочие параметры если использовать Word.Application и Word.Document получим предупреждение от компиллятора
        private MSWord._Application _application;
        private MSWord._Document _document;
        public MSWord._Document File { get { return _document; } }
        private Object _templatePathObj;

        private MSWord.Range _currentRange = null;

        private MSWord.Table _table = null;

        // обьект вставленного параграфа, представляет собой параграф с текстом, обертка над Range
        private WordSelection _selection;

        // вставленный параграф доступен только для чтения
        public WordSelection Selection
        {
            get { return _selection; }
            set { throw new Exception("Ошибка! Свойство InsertedParagraph только для чтения"); }
        }

        // СИМВОЛ МЯГКОГО ПЕРЕНОСА СТРОКИ В WORD (в ручную ставится через Shift + Enter)
        public static char NewLineChar
        {
            get { return (char)11; }
        }

        public bool Closed
        {
            get
            {
                if (_application == null || _document == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // видимость на экране приложения Word, по умолчанию false, документ создается невидимым и его надо явно сделать видимым после выполения необходимых операций
        public bool Visible
        {
            get
            {
                if (Closed)
                {
                    throw new Exception(
                        "Ошибка при попытке изменить видимость Microsoft Word. Программа или документ уже закрыты.");
                }
                return _application.Visible;

            }
            set
            {
                if (Closed)
                {
                    throw new Exception(
                        "Ошибка при попытке изменить видимость Microsoft Word. Программа или документ уже закрыты.");
                }
                _application.Visible = value;
            }
            // завершение public bool Visible  
        }

        // количество страниц
        public int PagesCount
        {
            get
            {
                int pagesCount = 0;
                MSWord.WdStatistic pagesStatType = MSWord.WdStatistic.wdStatisticPages;
                pagesCount = _document.ComputeStatistics(pagesStatType, ref _missingObj);
                return pagesCount;
            }
        }


        // КОНСТРУКТОР ПУСТОЙ ДОКУМЕНТ
        public WordDocument(bool startVisible)
        {
            //создаем обьект приложения word
            _application = new MSWord.Application();

            // если вылетим не этом этапе, приложение останется открытым
            try
            {
                _document = _application.Documents.Add(ref _missingObj, ref _missingObj, ref _missingObj,
                    ref _missingObj);
            }
            catch (Exception error)
            {
                this.Close();
                throw error;
            }
            Visible = startVisible;

            // устанавливаем текущую позицию в начало документа
            SetSelectionToBegin();
        }

        public WordDocument()
            : this(false)
        {
        }

        // КОНСТРУКТОР ШАБЛОН
        public WordDocument(string templatePath, bool startVisible)
        {
            //создаем обьект приложения word
            _application = new MSWord.Application();
            _application.Caption = templatePath.ToString();
            // создаем путь к файлу используя имя файла
            _templatePathObj = templatePath;

            // если вылетим не этом этапе, приложение останется открытым
            try
            {
                _document = _application.Documents.Add(ref _templatePathObj, ref _missingObj, ref _missingObj,
                    ref _missingObj);
            }
            catch (Exception error)
            {
                this.Close();
                throw error;
            }
            Visible = startVisible;

            // устанавливаем текущую позицию в начало документа
            SetSelectionToBegin();
        }

        public WordDocument(string templatePath)
            : this(templatePath, false)
        {
        }


        public static void FillShowTemplate(string pathToTemplate, Action<WordDocument> fillWordDoc)
        {

            // ошибку при открытии обработает вышестоящий код формы
            WordDocument wordDoc = null;
            try
            {
                wordDoc = new WordDocument(pathToTemplate);
                fillWordDoc(wordDoc);
            }
            catch (Exception error)
            {
                if (wordDoc != null)
                {
                    wordDoc.Close();
                }
                throw error;
            }

            wordDoc.Visible = true;
        }

        // выбор текста в документе для свойства selectedText ИЩЕТ ПЕРВОЕ ВХОЖДЕНИЕ
        public void SetSelectionToText(string stringToFind)
        {
            MSWord.Range foundRange = findRangeByString(stringToFind);
            if (foundRange == null)
            {
                //throw new Exception("Ошибка при поиске текста в MS Word. Не удалось найти и выбрать заданный текст: " + stringToFind);
                return;
            }
            _currentRange = foundRange;
            _selection = new WordSelection(foundRange, false);
        }

        // поиск и выбор текста в документе Word внутри строки-контейнера, сначала ищется контейнер, потом текст внутри него
        public void SetSelectionToText(string containerStr, string stringToFind)
        {

            MSWord.Range containerRange = null;
            MSWord.Range foundRange = null;

            containerRange = findRangeByString(containerStr);
            if (containerRange != null)
            {
                foundRange = findRangeByString(containerRange, stringToFind);
            }

            if (foundRange != null)
            {
                _currentRange = foundRange;
                _selection = new WordSelection(foundRange, false);
            }
            else
            {
                throw new Exception(
                    "Ошибка при поиске текста в MS Word. Не удалось найти заданную область для поиска текста: " +
                    containerStr);
            }
            // завершение public void searchSelectText(string containerStr, string stringToFind)
        }

        // встаем на закладку, то есть получаем обьект Range по имени закладки и заноми его в переменужж экземпляра класса, доступную для других методов
        public void SetSelectionToBookmark(string bookmarkName, bool isParagraph)
        {
            if (Closed)
            {
                throw new Exception("Ошибка при обращении к документу Word. Документ уже закрыт.");
            }

            Object bookmarkNameObj = bookmarkName;
            MSWord.Range bookmarkRange = null;
            try
            {
                bookmarkRange = _document.Bookmarks.get_Item(ref bookmarkNameObj).Range;
            }
            catch (Exception error)
            {
                throw new Exception("Ошибка при поиске закладки " + bookmarkName +
                                    " в документе Word. Сообщение от Word: " + error.Message);
            }
            _currentRange = bookmarkRange;
            _selection = new WordSelection(_currentRange, isParagraph);
        }

        public void SetSelectionToBookmark(string bookmarkName)
        {
            SetSelectionToBookmark(bookmarkName, false);
        }

        public void SetSelectionToBegin()
        {
            object start = 0;
            object end = 0;
            this._currentRange = this._document.Range(ref start, ref end);
            _selection = new WordSelection(_currentRange);
        }

        public void SetSelectionToCell(int rowIndex, int columnIndex)
        {
            if (_table == null)
            {
                throw new Exception("Ошибка при выборе ячейки в таблице Word, не выбрана текущая таблица.");
            }

            _currentRange = _table.Cell(rowIndex, columnIndex).Range;
            _selection = new WordSelection(_currentRange, false);
        }

        // вставляем пустой абзац, доступ к его тексту и свойствам через 
        public void InsertParagraphAfter()
        {
            if (Closed)
            {
                throw new Exception("Ошибка при обращении к документу Word. Документ уже закрыт.");
            }
            // сворачиваем текущую позицию и переходим в ее конец
            Object collapseDirection = MSWord.WdCollapseDirection.wdCollapseEnd;
            try
            {
                _currentRange.Collapse(ref collapseDirection);
                //вставляем абзац
                _currentRange.InsertParagraphAfter();
                _selection = new WordSelection(_currentRange);
            }
            catch (Exception wordError)
            {
                throw wordError;
            }
        }

        // упрощенная функция для вставки текста с умолчальными параметрами
        public void InsertParagraphAfter(string textToInsert)
        {
            this.InsertParagraphAfter();
            this._selection.Text = textToInsert;
        }

        public void InsertTable(int numRows, int numColumns)
        {
            InsertTable(numRows, numColumns, BorderType.Single);
        }

        public void InsertTable(int numRows, int numColumns, BorderType border)
        {

            _table = _document.Tables.Add(_currentRange, numRows, numColumns, ref _missingObj, ref _missingObj);

            switch (border)
            {
                case BorderType.None:
                    _table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleNone;
                    _table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleNone;
                    break;
                case BorderType.Single:
                    _table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;
                    _table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;
                    break;
                case BorderType.Double:
                    _table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleDouble;
                    _table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleDouble;
                    break;
                default:
                    _table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleNone;
                    _table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleNone;
                    break;
            }


            _currentRange = _table.Range;
            _selection = new WordSelection(_currentRange, false);
        }

        public void SetColumnWidth(int columnIndex, int widthPixels)
        {
            if (_table == null)
            {
                throw new Exception(
                    "Ошибка при установке ширины колонки в таблице Word - текущая таблица не выбрана (SetColumnWidth(int columnIndex, int widthPixels))");
            }
            _table.Columns[columnIndex].SetWidth(widthPixels, MSWord.WdRulerStyle.wdAdjustNone);
        }

        // ВСТАВЛЯЕМ ПУСТУЮ СТРАНИЦУ С ОДНИМ ПАРАГРАФОМ В КОНЦЕ, тупо добавляем пустые абзацы до появления следующей страницы
        public void InsertPageAtEnd()
        {
            object missing = Missing.Value;
            object what = MSWord.WdGoToItem.wdGoToLine;
            object which = MSWord.WdGoToDirection.wdGoToLast;
            MSWord.Range endRange = _document.GoTo(ref what, ref which, ref missing, ref missing);
            _currentRange = endRange;
            _selection = new WordSelection(endRange);

            // пока не изменится количество страниц вставляем пустые абзацы в конце
            int oldPagesCount = PagesCount;
            while (oldPagesCount == PagesCount)
            {
                InsertParagraphAfter();
            }
            InsertParagraphAfter();
        }

        //ВСТАВЛЯЕМ ДОКУМЕНТ WORD ИЗ ФАЙЛА
        public void InsertFile(string pathToFile)
        {
            if (_currentRange == null)
            {
                throw new Exception("Ничего не выбрано");
            }
            _currentRange.InsertFile(pathToFile);
        }

        // СОХРАНЯЕМ НА ДИСК С ПЕРЕЗАПИСЬЮ СУЩЕСТВУЮЩЕГО ФАЙЛА
        public void Save(string pathToSave)
        {
            Object pathToSaveObj = pathToSave;
            _document.SaveAs(ref pathToSaveObj, MSWord.WdSaveFormat.wdFormatDocumentDefault, ref _missingObj, ref _missingObj,
                ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj,
                ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj);
        }

        // закрытие открытого документа и приложения
        public void Close()
        {
            if (_document != null)
            {
                _document.Close(ref _falseObj, ref _missingObj, ref _missingObj);
            }
            _application?.Quit(ref _missingObj, ref _missingObj, ref _missingObj);
            _document = null;
            _application = null;
        }

        // поиск строки и ее замена на заданную строку
        public void ReplaceAllStrings(string strToFind, string replaceStr)
        {

            if (Closed)
            {
                throw new Exception("Ошибка при обращении к документу Word. Документ уже закрыт.");
            }
            try
            {
                var selection = _application.Selection;
                var find = selection.Find;
                var replacement = find.Replacement;

                find.ClearFormatting();
                find.Text = strToFind;

                // Word limitation : can't replace with more than 255 characters, 
                // use another way to do it if that's the case
                if (replaceStr.Length < 255)
                {
                    replacement.ClearFormatting();
                    replacement.Text = replaceStr;
                    find.Execute(Replace: MSWord.WdReplace.wdReplaceAll);
                }
                else
                {
                    while (find.Execute())
                    {
                        selection.TypeText(replaceStr.Replace("^p", char.ConvertFromUtf32(11)));
                    }
                    selection.GoTo(1, 1);
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        // ВЫБИРАЕМ ТАБЛИЦУ ПО ПОРЯДКОВОМ НОМЕРУ НАЧИНАЯ С ЕДИНИЦЫ
        public void SelectTable(int tableNumber)
        {
            try
            {
                _table = _document.Tables[tableNumber];
            }
            catch (Exception error)
            {
                throw new Exception("Таблица с номером " + tableNumber + " не найдена в документе Word. Подробности: " +
                                    error.Message);
            }
            _currentRange = _table.Range;
            _selection = new WordSelection(_table.Range, true, false);
        }


        public void AddRowToTable()
        {
            _table.Rows.Add(ref _missingObj);
        }

        // ИЩЕТ ПЕРВОЕ ВХОЖДЕНИЕ функция поиска Range  в документе Word строке, возвращает соответствующий строке Range, на входе строка для поиска
        private MSWord.Range findRangeByString(string stringToFind)
        {
            // проверяем, не закрыт ли документ или приложение ворд
            if (Closed)
            {
                throw new Exception("Ошибка при обращении к документу Word. Документ уже закрыт.");
            }
            // оформляем обьектные параметры
            object stringToFindObj = stringToFind;
            MSWord.Range wordRange;
            bool rangeFound;

            //в цикле обходим все разделы документа, получаем Range, запускаем поиск
            // если поиск вернул true, он долже ужать Range до найденное строки, выходим и возвращаем Range
            // обходим все разделы документа
            for (int i = 1; i <= _document.Sections.Count; i++)
            {
                // берем всю секцию диапазоном
                wordRange = _document.Sections[i].Range;

                /*
               // Обходим редкий глюк в Find, ПРИЗНАННЫЙ MICROSOFT, метод Execute на некоторых машинах вылетает с ошибкой "Заглушке переданы неправильные данные / Stub received bad data"  Подробности: http://support.microsoft.com/default.aspx?scid=kb;en-us;313104
               // выполняем метод поиска и  замены обьекта диапазона ворд
               rangeFound = wordRange.Find.Execute(ref stringToFindObj, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing);
                 */

                MSWord.Find wordFindObj = wordRange.Find;

                object[] wordFindParameters = new object[15]
                {
                    stringToFindObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj,
                    _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj,
                    _missingObj
                };

                rangeFound =
                    (bool)
                        wordFindObj.GetType()
                            .InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj, wordFindParameters);

                if (rangeFound)
                {
                    return wordRange;
                }

            }

            // если ничего не нашли, возвращаем null
            return null;
        }

        // ищет строку ВНУТРИ Range, при успехе возвращает Range  для этой строки
        private MSWord.Range findRangeByString(MSWord.Range containerRange, string stringToFind)
        {
            // проверяем, не закрыт ли документ или приложение ворд
            if (Closed)
            {
                throw new Exception("Ошибка при обращении к документу Word. Документ уже закрыт.");
            }
            // оформляем обьектные параметры
            object stringToFindObj = stringToFind;
            bool rangeFound;

            /*
            Обходим редкий глюк в Find, ПРИЗНАННЫЙ MICROSOFT, метод Execute на некоторых машинах вылетает с ошибкой "Заглушке переданы неправильные данные / Stub received bad data" 
             http://support.microsoft.com/default.aspx?scid=kb;en-us;313104
            rangeFound = containerRange.Find.Execute(ref stringToFindObj, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing);
             */

            MSWord.Find wordFindObj = containerRange.Find;

            object[] wordFindParameters = new object[15]
            {
                stringToFindObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj,
                _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj
            };

            rangeFound =
                (bool)
                    wordFindObj.GetType()
                        .InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj, wordFindParameters);
            if (rangeFound)
            {
                return containerRange;
            }
            else
            {
                return null;
            }
        }

        public static string GetDocumentTempFolder(byte[] data, string filename)
        {
            var tempFolder = System.IO.Path.GetTempPath();
            filename = System.IO.Path.Combine(tempFolder, filename);
            System.IO.File.WriteAllBytes(filename, data);
            return filename;
        }
        public void Dispose()
        {
            Close();
        }
    }

    // завершение class WordDocument
}