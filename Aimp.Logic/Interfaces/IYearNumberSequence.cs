using System;

namespace Aimp.Logic.Interfaces
{
    public interface IYearNumberSequence<T>
    {
        T CurrentValue(DateTime date);
        void NextValue(DateTime date);
    }
}
