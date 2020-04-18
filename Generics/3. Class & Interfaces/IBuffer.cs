using System.Collections.Generic;

namespace Class_Interfaces
{
    public interface IBuffer<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }
        void Write(T value);
        T Read();
    }
}