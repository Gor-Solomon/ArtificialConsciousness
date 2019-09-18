using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace AC.Desktop.Controls.Helpers
{
    [Serializable]
    public class UniqueCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        [NonSerialized]
        internal Action<T> OnAddItem;
        [NonSerialized]
        internal Action OnCollectionChangedAction;

        public UniqueCollection() : base() { }

        public UniqueCollection(IEnumerable<T> collection) : base(collection)
        {

            foreach (var item in Items)
                item.PropertyChanged += ItemPropertyChanged;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            OnCollectionChangedAction?.Invoke();
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCollectionChangedAction?.Invoke();
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            OnAddItem?.Invoke(item);
        }

        public override int GetHashCode()
        {
            return Items.Select(x => x.GetHashCode() / 1000).Sum();
        }
    }
}
