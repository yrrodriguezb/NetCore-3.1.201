using System;

namespace Methods_Delegates
{   
    public class CircularBuffer<T> : Buffer<T>
    {
        int _capacity;

        public CircularBuffer(int capacity = 10)
        {
            _capacity = capacity;
        }

        public override void Write(T value)
        {
            base.Write(value);

            if (_queue.Count > _capacity)
            {
                // Elimina el objeto mas antiguo
                var discard = _queue.Dequeue();
                OnItemDiscarded(discard, value);
            }
        }

        private void OnItemDiscarded(T discard, T value)
        {
            if (ItemDiscarded != null)
            {
                var args = new ItemDiscardedEventArgs<T>(discard, value);
                ItemDiscarded(this, args);
            }
        }

        public event EventHandler<ItemDiscardedEventArgs<T>> ItemDiscarded;
        public bool IsFull
        {
            get { return _queue.Count == _capacity; }
        }
    }
}