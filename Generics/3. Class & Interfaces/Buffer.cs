using System.Collections;
using System.Collections.Generic;

namespace Class_Interfaces
{
    public class Buffer<T> : IBuffer<T>
    {
        /*
            NOTA: 

            1.  Se dejan virtuales sus propiedades y metodos para que cualquier objeto que derive de esta 
                pueda modificar el compartamiento de estos.
        */

        protected Queue<T> _queue = new Queue<T>();

        public virtual bool IsEmpty
        {
            get { return _queue.Count == 0; }
        }

        public virtual T Read()
        {
            return _queue.Dequeue();
        }

        public virtual void Write(T value)
        {
            _queue.Enqueue(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _queue)
            {
                // ... 
                // Podemos realizar cambios sobre el objeto antes de retornalo de forma peresoza con yield
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}