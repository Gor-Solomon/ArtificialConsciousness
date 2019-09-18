using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AC.Desktop.Controls.Helpers
{
    public class ReorderableCollection<T> : ObservableCollection<T>
    {
        public Action OnAddItem;
        public Action OnRemoveItem;

        public ReorderableCollection(IEnumerable<T> collection) : base(collection)
        {

        }

        public ReorderableCollection() : base()
        {

        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            OnAddItem?.Invoke();
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            OnRemoveItem?.Invoke();
        }
    }
}
