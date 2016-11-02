﻿using System;
using MSWord = Microsoft.Office.Interop.Word;

namespace Aimp.Reports.Services.Word
{
    public enum TextAligment { Left, Center, Right, Justify }

    public enum BorderType { None, Single, Double }


    /// Версия 1.3
    // класс - параграф MS Word, обертка над обьектом Range который соответствует параграфу (вставленному в документ), дает доступ к стилю текста, выравниваю, размеру шрифта (возможно дальнейшее расширение, по идее создается внутри класса документа при вставке абзаца как публичное свойство-обьект, позволяющее заполнять свои поля по необходимости
    public class WordSelection
    {
        private MSWord.Range _range;
        private bool _insertParagrAfterText = true;

        // надо проверить возможно не нужно после последнего исправления (вставки параграфа после текста)
        private MSWord.WdParagraphAlignment _savedAligment;

        // конструктор принимает обьект Range
        public WordSelection(MSWord.Range inputRange)
            : this(inputRange, true, true)
        {
        }

        public WordSelection(MSWord.Range inputRange, bool insertParagrAfterText)
            : this(inputRange, insertParagrAfterText, true)
        {

        }

        public WordSelection(MSWord.Range inputRange, bool insertParagrAfterText, bool setDefaultStyle)
        {
            _range = inputRange;

            _insertParagrAfterText = insertParagrAfterText;

            if (setDefaultStyle)
            {
                _savedAligment = _range.ParagraphFormat.Alignment;
                _range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                _savedAligment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                _range.Italic = 0;
                _range.Bold = 0;
            }
            else
            {
                _savedAligment = _range.ParagraphFormat.Alignment;
            }
        }
        public bool Bold
        {
            get
            {
                // нет точных данных о возможных значениях, 1 жирный, 0 нет... но по идее может быть и еще
                if (_range.Bold == 1) { return true; }
                else { return false; }
            }

            set
            {
                if (value == true) { _range.Bold = 1; }
                else { _range.Bold = 0; }
            }
            // завершение public bool Bold
        }

        public bool Italic
        {
            get
            {
                // открытый вопрос с возможными занчениями в Word по умолчанию, документация плохая
                if (_range.Italic == 1) { return true; }
                else { return false; }
            }
            set
            {
                if (value == true) { _range.Italic = 1; }
                else { _range.Italic = 0; }
            }
            // завершение  public bool Italic
        }

        //свойство-перечисление выравнивания
        public TextAligment Aligment
        {
            get
            {
                if (_range.ParagraphFormat.Alignment == MSWord.WdParagraphAlignment.wdAlignParagraphLeft)
                { return TextAligment.Left; }
                else if (_range.ParagraphFormat.Alignment == MSWord.WdParagraphAlignment.wdAlignParagraphCenter)
                { return TextAligment.Center; }
                else if (_range.ParagraphFormat.Alignment == MSWord.WdParagraphAlignment.wdAlignParagraphRight)
                { return TextAligment.Right; }
                else if (_range.ParagraphFormat.Alignment == MSWord.WdParagraphAlignment.wdAlignParagraphJustify)
                { return TextAligment.Justify; }
                else { throw new Exception("Ошибка при определении типа вырвнивания текста"); }
            }
            set
            {
                if (value == TextAligment.Left)
                {
                    _range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                    _savedAligment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                }
                else if (value == TextAligment.Center)
                {
                    _range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                    _savedAligment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                }
                else if (value == TextAligment.Right)
                {
                    _range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphRight;
                    _savedAligment = MSWord.WdParagraphAlignment.wdAlignParagraphRight;
                }
                else if (value == TextAligment.Justify)
                {
                    _range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphJustify;
                    _savedAligment = MSWord.WdParagraphAlignment.wdAlignParagraphJustify;
                }
                else { throw new Exception("Неизвестный тип выравнивания текста"); }
            }
            // завершение public TextAligment Aligment
        }

        //собственно текст параграфа
        public string Text
        {
            get { return _range.Text; }
            set
            {
                _range.Text = value;
                // обход глюка Word, при заполнении свойства "текст" параграф затирается и текст присоединяется к предыдущему параграфу, Range начинаеьт указывать на предыдущий параграф
                if (_insertParagrAfterText)
                {
                    _range.InsertParagraphAfter();
                }
                // обход глюка Word: при вставке текста выравнивание ставится по центру
                _range.ParagraphFormat.Alignment = this._savedAligment;

            }
            // завершение public string Text
        }

        //свойство int размер шрифта
        public int FontSize
        {
            get { return Convert.ToInt32(this._range.Font.Size); }
            set
            {
                if (value < 1) { throw new Exception("Ошибка при установке размера шрифта  Word. Размер шрифта не может быть меньше 1."); }
                float fontSize = (float)Convert.ToDouble(value);
                this._range.Font.Size = fontSize;
            }
            // завершение public int FontSize
        }

        public BorderType Border
        {
            set
            {
                switch (value)
                {
                    case BorderType.None:
                        _range.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleNone;
                        break;
                    case BorderType.Single:
                        _range.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;
                        break;
                    case BorderType.Double:
                        _range.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleDouble;
                        break;
                    default:
                        _range.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleNone;
                        break;
                }
            }

            get
            {
                switch (_range.Borders.OutsideLineStyle)
                {
                    case MSWord.WdLineStyle.wdLineStyleNone:
                        return BorderType.None;
                    case MSWord.WdLineStyle.wdLineStyleSingle:
                        return BorderType.Single;
                    case MSWord.WdLineStyle.wdLineStyleDouble:
                        return BorderType.Double;
                    default:
                        return BorderType.None;
                }
            }
        }



        // завершение class WordParagraph
    }
}
