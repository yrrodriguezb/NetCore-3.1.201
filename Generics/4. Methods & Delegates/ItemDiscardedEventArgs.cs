using System;

namespace Methods_Delegates
{
    public class ItemDiscardedEventArgs<T> : EventArgs
    {
        public ItemDiscardedEventArgs(T discarded, T newitem)
        {
            ItemDiscarded = discarded;
            NewItem = newitem;
        }
        
        public T ItemDiscarded { get; set; }
        public T NewItem { get; set; }
    }
}