using System.Collections.Generic;

namespace Methods_Delegates
{
    public interface IBuffer<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }
        void Write(T value);
        T Read();
    }
}