using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using AC.Desktop.Controls.Commands;
using AC.Common;
using AC.Common.Resources;
using AC.Desktop.Controls.Extensions;

namespace AC.Desktop.Controls.ViewModelBase
{
    public abstract class ViewModel<T> : INotifyPropertyChanged, IDataErrorInfo, IViewModel where T : FrameworkElement
    {
        #region Fields

        //protected const string ImageDialogFilter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg|All files (*.*)|*.*";
        private IDialogCoordinator _dialogCoordinator;
        private static ProgressDialogController _controller;

        #endregion

        #region Properties
        public T View { get; }
        #endregion

        #region Constructors
        protected ViewModel(T view = null)
        {
            View = view ?? ((T)Activator.CreateInstance(typeof(T)));
            View.DataContext = this;
            LoadControlCommand = new BaseCommand(LoadControl);

            _dialogCoordinator = DialogCoordinator.Instance;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Call by specifying the property to Notify
        /// </summary>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="expr"></param>
        protected void Notify<TProp>(Expression<Func<TProp>> expr)
        {
            PropertyChanged.Raise(expr);
        }

        /// <summary>
        /// Calling with null automatically obtains the method or property name of the caller
        /// </summary>
        protected void Notify([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region INotifyPropertyChanged implementation
        FrameworkElement IViewModel.View => View as FrameworkElement;

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        #endregion

        #region IDataErrorInfo implementation

        public virtual string Error => this[null];

        public virtual string this[string columnName] => string.Empty;

        public bool IsValid => string.IsNullOrEmpty(Error);

        private bool _canEdit;
        public bool CanEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if (_canEdit == value) return;
                _canEdit = value;
                Notify();
            }
        }

        #endregion

        #region Async loading of data

        public ICommand LoadControlCommand { get; set; }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// Function for load data async, binding with XAML
        /// </summary>
        /// <param name="param"></param>
        protected virtual async void LoadControl(object param = null) { }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        #endregion

        #region Message boxes

        protected virtual Task<string> ShowInputBoxAsync(string message, string title = null)
        {
            title = title ?? CommonStrings.ACGeneralLabel;
            return _dialogCoordinator.ShowInputAsync(this, title, message);
        }

        protected virtual async Task<MessageDialogResult> ShowQuestion(string message)
        {
            var settings = new MetroDialogSettings
            {
                AnimateHide = true,
                AffirmativeButtonText = CommonStrings.YesLabel,
                DefaultButtonFocus = MessageDialogResult.Affirmative,
                NegativeButtonText = CommonStrings.NoLabel
            };

            var result = await ShowMetroDialog(message, settings, MessageDialogStyle.AffirmativeAndNegative);
            return result;
        }

        protected virtual async Task<MessageDialogResult> ShowQuestion(string message, string affirmativeButtonText, string negativeButtonText)
        {
            var settings = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                DefaultButtonFocus = MessageDialogResult.Affirmative,
                AffirmativeButtonText = affirmativeButtonText,
                NegativeButtonText = negativeButtonText
            };

            var result = await ShowMetroDialog(message, settings, MessageDialogStyle.AffirmativeAndNegative);
            return result;
        }

        protected virtual MessageDialogResult ShowQuestionSync(string message, string title = null)
        {
            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = CommonStrings.YesLabel,
                DefaultButtonFocus = MessageDialogResult.Affirmative,
                NegativeButtonText = CommonStrings.NoLabel
            };
            title = title ?? CommonStrings.ACGeneralLabel;
            return _dialogCoordinator.ShowModalMessageExternal(this, title, message, MessageDialogStyle.AffirmativeAndNegative, settings);
        }

        protected virtual async Task ShowMessage(string message)
        {
            await ShowMetroDialog(message);
        }

        protected virtual async Task ShowMessage(params string[] messages)
        {
            var builder = new StringBuilder();
            foreach (var message in messages)
                builder.Append($"{message}{Environment.NewLine}");

            await ShowMessage(builder.ToString());
        }

        protected virtual void ShowMessageSync(string message, string title = null)
        {
            title = title ?? CommonStrings.ACGeneralLabel;
            _dialogCoordinator.ShowModalMessageExternal(this, title, message, MessageDialogStyle.Affirmative);
        }

        #endregion

        #region MahApps modals 

        async Task<MessageDialogResult> ShowMetroDialog(string message, MetroDialogSettings settings = null, MessageDialogStyle style = MessageDialogStyle.Affirmative, string title = null)
        {
            try
            {
                title = title ?? CommonStrings.ACGeneralLabel;
                return await _dialogCoordinator.ShowMessageAsync(this, title, message, style, settings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return MessageDialogResult.Canceled;
        }

        #endregion

        #region Progress Bars

        protected virtual async Task ShowProgressAsync(string message, string title = null)
        {
            if (_controller != null)
            {
                MessageBox.Show("Progress bar for one place is not closed");
                return;
            }
            title = title ?? CommonStrings.ACGeneralLabel;
            _controller = await _dialogCoordinator.ShowProgressAsync(this, title, message);
        }

        protected async Task CloseProgressAsync()
        {
            if (_controller != null)
            {
                await _controller.CloseAsync();
                _controller = null;
            }
        }

        #endregion

        #region Browse Dialogs

        protected virtual OpenFileDialog BrowseForFile(string filter, out bool? dlgResult,
                                                       string title = null)
        {
            dlgResult = false;

            var dlg = new OpenFileDialog
            {
                Filter = filter,
                FilterIndex = 1,
                RestoreDirectory = true,
                Title = title
            };
            title = title ?? CommonStrings.ACGeneralLabel;
            dlgResult = dlg.ShowDialog();

            return dlg;
        }

        protected virtual System.Windows.Forms.FolderBrowserDialog BrowseForFolder(out bool dlgResult, string defaultPath = null)
        {
            var dlg = new System.Windows.Forms.FolderBrowserDialog
            {
                ShowNewFolderButton = false
            };

            if (!string.IsNullOrWhiteSpace(defaultPath))
                dlg.SelectedPath = defaultPath;

            var result = dlg.ShowDialog();

            dlgResult = result == System.Windows.Forms.DialogResult.OK;

            return dlg;
        }

        protected virtual SaveFileDialog SaveFile(string filer, string title, string fileName, out bool? dlgResult)
        {
            dlgResult = false;

            var dlg = new SaveFileDialog
            {
                Filter = filer,
                FilterIndex = 1,
                RestoreDirectory = true,
                Title = title,
                FileName = fileName
            };
            title = title ?? CommonStrings.ACGeneralLabel;
            dlgResult = dlg.ShowDialog();

            return dlg;
        }

        #endregion
    }
}
