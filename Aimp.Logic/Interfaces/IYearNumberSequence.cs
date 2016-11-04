using System;

namespace Aimp.Logic.Interfaces
{
    public interface IYearNumberSequence<out T>
    {
        T CurrentValue(DateTime date);
        void NextValue(DateTime date);
    }
}
