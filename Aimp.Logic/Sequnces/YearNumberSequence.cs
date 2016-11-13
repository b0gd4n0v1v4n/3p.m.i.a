using System;
using Aimp.Logic.Interfaces;
using System.Collections.Generic;

namespace Aimp.Logic.Sequnces
{
    public class YearNumberSequence : IYearNumberSequence<int>
    {
        private readonly IDictionary<int, int> _dictionary;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary">year,last number</param>
        public YearNumberSequence(IDictionary<int,int> dictionary)
        {
            _dictionary = dictionary;
        }
        public int CurrentValue(DateTime date)
        {
            if (!_dictionary.ContainsKey(date.Year))
                _dictionary.Add(date.Year, 1);

            return _dictionary[date.Year];
        }

        public void NextValue(DateTime date)
        {
            if (!_dictionary.ContainsKey(date.Year))
                _dictionary.Add(date.Year, 1);

            _dictionary[date.Year]++;
        }
    }
}
