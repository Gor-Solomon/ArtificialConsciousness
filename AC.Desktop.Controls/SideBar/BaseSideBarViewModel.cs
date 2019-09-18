using AC.BLL.Models;
using AC.Desktop.Controls.Commands;
using AC.Desktop.Controls.Helpers;
using AC.Desktop.Controls.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AC.Desktop.Controls.SideBar
{
    public class BaseSideBarViewModel<T, U> : ViewModel<U> where U : FrameworkElement where T : class, IBllModel
    {
        #region Fields

        private T _selectedItem;
        private ObservableCollection<T> _items;
        private string _filterText;
        private bool _isFilterTextFocused;

        #endregion

        #region Commands

        public BaseCommand CreateNewCommand { get; }

        public BaseCommand DeleteCommand { get; }

        public BaseCommand ImportCommand { get; }

        public BaseCommand ExportCommand { get; }

        public BaseCommand CreateFromCommand { get; }

        public BaseCommand FilterTextChangedCommand { get; }

        public BaseCommand SelectCommand { get; }

        public BaseCommand ItemMouseDoubleClickCommand { get; }

        public BaseCommand ClearCommand { get; }

        public BaseCommand EnterKeyPressedCommand { get; }

        #endregion

        #region Properties

        public bool IsFilterTextFocused
        {
            get { return _isFilterTextFocused; }
            set
            {
                if (_isFilterTextFocused != value)
                {
                    _isFilterTextFocused = value;
                    Notify();
                }
            }
        }

        protected internal SideBarConfiguration<T> Configuration { get; set; }

        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    //var result = Configuration.PreSelectAction?.Invoke(value);
                    //if (result.HasValue && !result.Value)
                    //{
                    //    return;
                    //}

                    _selectedItem = value;
                    Configuration.SelectedItemAction?.Invoke(_selectedItem);
                    DeleteCommand.NotifyCanExecuteChanged();
                    Notify();
                    Notify(nameof(SelectedItemIsNull));
                    CreateNewCommand?.NotifyCanExecuteChanged();
                }
            }
        }

        public ObservableCollection<T> Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    Notify();
                    CreateNewCommand?.NotifyCanExecuteChanged();
                }
            }
        }

        public bool CreateNewVisible
        {
            get { return Configuration?.NewAction != null; }
        }

        public bool DeleteVisible
        {
            get { return Configuration?.DeleteAction != null; }
        }

        public bool ImportVisible
        {
            get { return Configuration?.ImportAction != null; }
        }

        public bool ClearVisible
        {
            get { return Configuration?.ClearAction != null; }
        }

        #region Item Actions

        public bool ExportVisible
        {
            get
            {
                return SelectedItemIsNull ? Configuration?.ExportAction != null : SelectedItem.Id != 0 && Configuration?.ExportAction != null;
            }
        }

        public bool CreateFromVisible
        {
            get
            {
                return SelectedItemIsNull ? Configuration?.CreateFromAction != null : SelectedItem.Id != 0 && Configuration?.CreateFromAction != null;
            }
        }

        public bool SelectVisible
        {
            get
            {
                return SelectedItemIsNull ? Configuration?.SelectAction != null : SelectedItem.Id != 0 && Configuration?.SelectAction != null;
            }
        }

        #endregion

        public bool TopPanelVisible
        {
            get
            {
                return ImportVisible || ExportVisible || DeleteVisible;
            }
        }

        public bool FilterVisible
        {
            get { return Configuration?.FilterTextChangedAction != null || Configuration?.FilterTextChangedActionAsync != null; }
        }

        public bool SelectedItemIsNull
        {
            get { return SelectedItem == null; }
        }


        public bool IsSelectEnabled
        {
            get { return Configuration?.IsSelectEnabled ?? false; }
        }

        public bool IsActionsVisible
        {
            get { return CreateFromVisible || ExportVisible || SelectVisible; }
        }

        public virtual string ActionsColumnWidth
        {
            get
            {
                double value = 0;

                if (Configuration?.CreateFromAction != null)
                    value += 2;
                if (Configuration.ExportAction != null)
                    value += 1.5;
                if (Configuration.SelectAction != null)
                    value += 1;

                return $"{value}*";
            }
        }

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    Notify();
                    if (Configuration.FilterTextChangedActionAsync == null)
                        Items = Configuration?.FilterTextChangedAction(_filterText).ToObservable();
                    else
                        ExecuteFilterTextAsync(value);
                }
            }
        }

        #endregion

        #region Execute

        async void ExecuteFilterTextAsync(string param)
        {
            var items = await Configuration?.FilterTextChangedActionAsync?.Invoke(param);
            Items = items.ToObservable();
        }

        #endregion

        #region Constructors

        public BaseSideBarViewModel(IEnumerable<T> collection, SideBarConfiguration<T> configuration = default(SideBarConfiguration<T>))
        {
            Configuration = configuration;
            Items = collection.ToObservable();

            if (Configuration != null)
            {
                CreateNewCommand = new BaseCommand(o => { Configuration.NewAction(o); Refresh(); }, o => !Items?.Any(item => item.Id == 0) ?? true);
                DeleteCommand = new BaseCommand(Configuration.DeleteAction, o => !SelectedItemIsNull);
                ImportCommand = new BaseCommand(Configuration.ImportAction);
                ExportCommand = new BaseCommand(Configuration.ExportAction);
                CreateFromCommand = new BaseCommand(Configuration.CreateFromAction);
                SelectCommand = new BaseCommand(Configuration.SelectAction);
                if (Configuration.ItemDoubleClickAction != null)
                    ItemMouseDoubleClickCommand = new BaseCommand(Configuration.ItemDoubleClickAction);
                ClearCommand = new BaseCommand(Configuration.ClearAction);
                EnterKeyPressedCommand = new BaseCommand(Configuration.EnterKeyPressedAction);
                IsFilterTextFocused = Configuration.IsFilterTextFocusedOnStart;
            }
        }

        #endregion

        #region Helpers

        public virtual void MoveToTopSelectedItem()
        {
            if (Items == null || !Items.Any())
                return;

            foreach (var i in Items)
                i.IsSelectedItemForView = false;

            if (!SelectedItemIsNull)
            {
                SelectedItem.IsSelectedItemForView = true;
            }       

            var item = SelectedItem;

            var notSelected = Items.Where(x => !x.IsSelectedItemForView).ToList();

            Items.Clear();
            Items.Add(item);
            Items.AddRange(notSelected);

            SelectedItem = item;

            Notify(nameof(Items));
            CreateNewCommand?.NotifyCanExecuteChanged();
        }

        public virtual T this[int index]
        {
            get
            {
                if (Items == null || Items.Count == 0)
                    throw new InvalidOperationException();

                return FirstOrDefaultItem(x => x.Id == index);
            }
            set
            {
                var itemToReplace = FirstOrDefaultItem(x => x.Id == index);
                if (Items != null)
                {
                    Items.Remove(itemToReplace);
                    Items.Add(value);
                }
            }
        }

        public void Refresh()
        {
            Notify(nameof(Items));
            CreateNewCommand?.NotifyCanExecuteChanged();
        }

        public virtual void Refresh(IEnumerable<T> refreshCollection)
        {
            Items = refreshCollection?.ToObservable();
        }

        public virtual void AddItem(T item)
        {
            if (Items != null)
            {
                Items.Add(item);
                SelectedItem = item;
                Notify(nameof(Items));
                CreateNewCommand?.NotifyCanExecuteChanged();
            }
        }

        public virtual void ClearItems()
        {
            if (Items != null && AnyItem())
            {
                Items.Clear();
                Notify(nameof(Items));
                CreateNewCommand?.NotifyCanExecuteChanged();
            }
        }

        public virtual void Remove(T item)
        {
            if (Items != null && AnyItem())
            {
                Items.Remove(item);
                SelectedItem = Items.FirstOrDefault();
                Notify(nameof(Items));
                CreateNewCommand?.NotifyCanExecuteChanged();
            }
        }

        public virtual bool AnyItem()
        {
            return Items != null && Items.Any();
        }

        public virtual bool AnyItem(Func<T, bool> predicate)
        {
            return Items != null && Items.Any(predicate);
        }

        public virtual T FirstOrDefaultItem(Func<T, bool> predicate)
        {
            if (AnyItem())
                return Items.FirstOrDefault(predicate);
            return null;
        }

        public virtual T LastOrDefaultItem(Func<T, bool> predicate)
        {
            return AnyItem() ? Items.LastOrDefault(predicate) : null;
        }

        public virtual T LastOrDefaultItem()
        {
            return AnyItem() ? Items.LastOrDefault() : null;
        }

        public virtual T FirstOrDefaultItem()
        {
            return AnyItem() ? Items.FirstOrDefault() : null;
        }

        public virtual int ItemsCount
        {
            get { return Items?.Count ?? 0; }
        }

        public virtual void RemoveEmptyItems()
        {
            if (Items != null)
            {
                var data = Items.Where(x => x.Id == 0).ToList();
                foreach (var d in data)
                    Items.Remove(d);
                CreateNewCommand?.NotifyCanExecuteChanged();
            }
        }

        public virtual int NameColumnWidth
        {
            get
            {
                int value = 290;

                if (SelectVisible)
                {
                    value -= 50;
                }

                if (ExportVisible)
                {
                    value -= 50;
                }

                if (CreateFromVisible)
                {
                    value -= 75;
                }

                return value;
            }
        }

        #endregion
    }
}
