using AC.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Desktop.Controls.SideBar
{
    public class SideBarConfiguration<T> where T : class, IBllModel
    {
        public Func<T, bool> PreSelectAction { get; set; }
        public Action<object> SelectAction { get; set; }
        public Action<object> SelectAllAction { get; set; }
        public Action<object> NewAction { get; set; }
        public Action<object> DeleteAction { get; set; }
        public Action<object> ImportAction { get; set; }
        public Action<T> SelectedItemAction { get; set; }
        public Action<object> ExportAction { get; set; }
        public Action<object> CreateFromAction { get; set; }
        public Action<object> ItemDoubleClickAction { get; set; }
        public Func<string, IEnumerable<T>> FilterTextChangedAction { get; set; }
        public Func<string, Task<IEnumerable<T>>> FilterTextChangedActionAsync { get; set; }
        public Action<object> ClearAction { get; set; }
        public Action<object> EnterKeyPressedAction { get; set; }
        public bool IsFilterTextFocusedOnStart { get; set; }

        public ObjectHasherAlgorithmTypeEnum HashAlgorithm { get; set; } = ObjectHasherAlgorithmTypeEnum.MD5;

        public bool IsSelectEnabled { get; set; }

        public SideBarConfiguration(Action<object> newExecute = null, Action<object> deleteExecute = null, Func<T, bool> preSelectAction = null,
            Action<object> importExecute = null, Action<T> selectedItemExecute = null, Action<T> selectAllAction = null, Action<object> exportExecute = null,
            Action<object> createFromExecute = null, Func<string, List<T>> filterTextChangedAction = null, Action<object> selectAction = null)
        {
            PreSelectAction = preSelectAction;
            NewAction = newExecute;
            DeleteAction = deleteExecute;
            ImportAction = importExecute;
            SelectedItemAction = selectedItemExecute;
            ExportAction = exportExecute;
            CreateFromAction = createFromExecute;
            FilterTextChangedAction = filterTextChangedAction;
            SelectAction = selectAction;
            SelectAllAction = selectAction;
        }
    }
}
