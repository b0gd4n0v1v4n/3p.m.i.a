using System;

namespace Aimp.PrintedDocument.Helpers
{
    public enum TextCase
    {
        Nominative /*Кто? Что?*/,
        Genitive /*Кого? Чего?*/,
        Dative /*Кому? Чему?*/,
        Accusative /*Кого? Что?*/,
        Instrumental /*Кем? Чем?*/,
        Prepositional /*О ком? О чём?*/
    };

    public static class MoneyToText
    {
        private static string _zero = "ноль";
        private static string _firstMale = "один";
        private static string _firstFemale = "одна";
        private static string _firstFemaleAccusative = "одну";
        private static string _firstMaleGenetive = "одно";
        private static string _secondMale = "два";
        private static string _secondFemale = "две";
        private static string _secondMaleGenetive = "двух";
        private static string _secondFemaleGenetive = "двух";

        private static string[] _from3till19 =
        {
            "", "три", "четыре", "пять", "шесть",
            "семь", "восемь", "девять", "десять", "одиннадцать",
            "двенадцать", "тринадцать", "четырнадцать", "пятнадцать",
            "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать"
        };

        private static string[] _from3till19Genetive =
        {
            "", "трех", "четырех", "пяти", "шести",
            "семи", "восеми", "девяти", "десяти", "одиннадцати",
            "двенадцати", "тринадцати", "четырнадцати", "пятнадцати",
            "шестнадцати", "семнадцати", "восемнадцати", "девятнадцати"
        };

        private static string[] _tens =
        {
            "", "двадцать", "тридцать", "сорок", "пятьдесят",
            "шестьдесят", "семьдесят", "восемьдесят", "девяносто"
        };

        private static string[] _tensGenetive =
        {
            "", "двадцати", "тридцати", "сорока", "пятидесяти",
            "шестидесяти", "семидесяти", "восьмидесяти", "девяноста"
        };

        private static string[] _hundreds =
        {
            "", "сто", "двести", "триста", "четыреста",
            "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот"
        };

        private static string[] _hundredsGenetive =
        {
            "", "ста", "двухсот", "трехсот", "четырехсот",
            "пятисот", "шестисот", "семисот", "восемисот", "девятисот"
        };

        private static string[] _thousands =
        {
            "", "тысяча", "тысячи", "тысяч"
        };

        private static string[] _thousandsAccusative =
        {
            "", "тысячу", "тысячи", "тысяч"
        };

        private static string[] _millions =
        {
            "", "миллион", "миллиона", "миллионов"
        };

        private static string[] _billions =
        {
            "", "миллиард", "миллиарда", "миллиардов"
        };

        private static string[] _trillions =
        {
            "", "трилион", "трилиона", "триллионов"
        };

        private static string[] _rubles =
        {
            "", "рубль", "рубля", "рублей"
        };

        private static string[] _copecks =
        {
            "", "копейка", "копейки", "копеек"
        };
        static int _lastDigit(long _amount)
        {
            long amount = _amount;

            if (amount >= 100)
                amount = amount % 100;

            if (amount >= 20)
                amount = amount % 10;

            return (int)amount;
        }
        static bool _IsPluralGenitive(int _digits)
        {
            if (_digits >= 5 || _digits == 0)
                return true;

            return false;
        }
        static bool _IsSingularGenitive(int _digits)
        {
            if (_digits >= 2 && _digits <= 4)
                return true;

            return false;
        }
        /// <summary>
        /// Десять тысяч рублей 67 копеек
        /// </summary>
        /// <param name="_amount"></param>
        /// <param name="_firstCapital"></param>
        /// <returns></returns>
        public static string Convert(decimal _amount)
        {
            //Десять тысяч рублей 67 копеек
            long rublesAmount = (long)Math.Floor(_amount);
            long copecksAmount = ((long)Math.Round(_amount * 100)) % 100;
            int lastRublesDigit = _lastDigit(rublesAmount);
            int lastCopecksDigit = _lastDigit(copecksAmount);

            string s = _NumeralsToTxt(rublesAmount, TextCase.Nominative, true, true) + " ";

            if (_IsPluralGenitive(lastRublesDigit))
            {
                s += _rubles[3] + " ";
            }
            else if (_IsSingularGenitive(lastRublesDigit))
            {
                s += _rubles[2] + " ";
            }
            else
            {
                s += _rubles[1] + " ";
            }

            s += String.Format("{0:00} ", copecksAmount);

            if (_IsPluralGenitive(lastCopecksDigit))
            {
                s += _copecks[3] + " ";
            }
            else if (_IsSingularGenitive(lastCopecksDigit))
            {
                s += _copecks[2] + " ";
            }
            else
            {
                s += _copecks[1] + " ";
            }

            return s.Trim();
        }
        static string _MakeText(int _digits, string[] _hundreds, string[] _tens, string[] _from3till19, string _second, string _first, string[] _power)
        {
            string s = "";
            int digits = _digits;

            if (digits >= 100)
            {
                s += _hundreds[digits / 100] + " ";
                digits = digits % 100;
            }
            if (digits >= 20)
            {
                s += _tens[digits / 10 - 1] + " ";
                digits = digits % 10;
            }

            if (digits >= 3)
            {
                s += _from3till19[digits - 2] + " ";
            }
            else if (digits == 2)
            {
                s += _second + " ";
            }
            else if (digits == 1)
            {
                s += _first + " ";
            }

            if (_digits != 0 && _power.Length > 0)
            {
                digits = _lastDigit(_digits);

                if (_IsPluralGenitive(digits))
                {
                    s += _power[3] + " ";
                }
                else if (_IsSingularGenitive(digits))
                {
                    s += _power[2] + " ";
                }
                else
                {
                    s += _power[1] + " ";
                }
            }

            return s;
        }
        /// <summary>
        /// реализовано для падежей: именительный (nominative), родительный (Genitive),  винительный (accusative)
        /// </summary>
        /// <param name="_sourceNumber"></param>
        /// <param name="_case"></param>
        /// <param name="_isMale"></param>
        /// <param name="_firstCapital"></param>
        /// <returns></returns>
        private static string _NumeralsToTxt(long _sourceNumber, TextCase _case, bool _isMale, bool _firstCapital)
        {
            string s = "";
            long number = _sourceNumber;
            int remainder;
            int power = 0;

            if ((number >= (long)Math.Pow(10, 15)) || number < 0)
            {
                return "";
            }

            while (number > 0)
            {
                remainder = (int)(number % 1000);
                number = number / 1000;

                switch (power)
                {
                    case 12:
                        s = _MakeText(remainder, _hundreds, _tens, _from3till19, _secondMale, _firstMale, _trillions) + s;
                        break;
                    case 9:
                        s = _MakeText(remainder, _hundreds, _tens, _from3till19, _secondMale, _firstMale, _billions) + s;
                        break;
                    case 6:
                        s = _MakeText(remainder, _hundreds, _tens, _from3till19, _secondMale, _firstMale, _millions) + s;
                        break;
                    case 3:
                        switch (_case)
                        {
                            case TextCase.Accusative:
                                s = _MakeText(remainder, _hundreds, _tens, _from3till19, _secondFemale, _firstFemaleAccusative, _thousandsAccusative) + s;
                                break;
                            default:
                                s = _MakeText(remainder, _hundreds, _tens, _from3till19, _secondFemale, _firstFemale, _thousands) + s;
                                break;
                        }
                        break;
                    default:
                        string[] powerArray = { };
                        switch (_case)
                        {
                            case TextCase.Genitive:
                                s = _MakeText(remainder, _hundredsGenetive, _tensGenetive, _from3till19Genetive, _isMale ? _secondMaleGenetive : _secondFemaleGenetive, _isMale ? _firstMaleGenetive : _firstFemale, powerArray) + s;
                                break;
                            case TextCase.Accusative:
                                s = _MakeText(remainder, _hundreds, _tens, _from3till19, _isMale ? _secondMale : _secondFemale, _isMale ? _firstMale : _firstFemaleAccusative, powerArray) + s;
                                break;
                            default:
                                s = _MakeText(remainder, _hundreds, _tens, _from3till19, _isMale ? _secondMale : _secondFemale, _isMale ? _firstMale : _firstFemale, powerArray) + s;
                                break;
                        }
                        break;
                }

                power += 3;
            }

            if (_sourceNumber == 0)
            {
                s = _zero + " ";
            }

            if (s != "" && _firstCapital)
                s = s.Substring(0, 1).ToUpper() + s.Substring(1);

            return s.Trim();
        }
    }
}
