using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AC.Desktop.Controls.Helpers
{
    public static class ObservableExtensions
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> collection)
        {
            return collection != null ? new ObservableCollection<T>(collection) : new ObservableCollection<T>();
        }

        public static ReorderableCollection<T> ToReorderableCollection<T>(this IEnumerable<T> collection, Action onAddItemHandler = null, Action onRemoveItemHandler = null)
        {
            return new ReorderableCollection<T>(collection) { OnAddItem = onAddItemHandler, OnRemoveItem = onRemoveItemHandler };
        }

        public static UniqueCollection<T> ToUniqueCollection<T>(this IEnumerable<T> collection, Action<T> addItemHandler = null, Action collectionChangedHanler = null) where T : INotifyPropertyChanged
        {
            return collection != null ? new UniqueCollection<T>(collection) { OnAddItem = addItemHandler, OnCollectionChangedAction = collectionChangedHanler } : new UniqueCollection<T>();
        }

        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> next)
        {
            foreach (var item in next)
                collection?.Add(item);
        }

        public static void AddRange<T>(this ObservableCollection<T> collection, params T[] @params)
        {
            foreach (var item in @params)
                collection?.Add(item);
        }

        public static IEnumerable<TResult> Merge<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> operation)
        {
            using (var iter1 = first.GetEnumerator())
            {
                using (var iter2 = second.GetEnumerator())
                {
                    while (iter1.MoveNext())
                    {
                        if (iter2.MoveNext())
                        {
                            yield return operation(iter1.Current, iter2.Current);
                        }
                        else
                        {
                            yield return operation(iter1.Current, default(TSecond));
                        }
                    }
                    while (iter2.MoveNext())
                    {
                        yield return operation(default(TFirst), iter2.Current);
                    }
                }
            }
        }
    }
}
