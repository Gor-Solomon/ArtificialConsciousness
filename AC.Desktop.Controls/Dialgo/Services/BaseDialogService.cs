using AC.Desktop.Controls.Dialgo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AC.Desktop.Controls.Dialgo
{
    public abstract class BaseDialogService 
    {
        protected const int WindowHeight = 600;
        protected const int WindowWidth = 900;
        //private PreviewImageViewModel _previewImageViewModel;

        public T ShowDialog<T>(T viewModel, string title, bool isCloseVisible = false, 
            int width = WindowWidth, int height = WindowHeight, int margin = 0) where T : ICloseable
        {
            var dialog = new DialogViewModel(viewModel, isCloseVisible);
            dialog.Title(title).Width(width).Height(height).Margin(margin).Show();
            (dialog.Content as ICloseable).InvokeClose();
            return (T)dialog.Content;
        }

        //protected void ShowPreviewImageDialog(byte[] imageData, string title = null)
        //{
        //    if (string.IsNullOrEmpty(title)) title = CommonStrings.PreviewImageLbl;

        //    _previewImageViewModel = _previewImageViewModel ?? new PreviewImageViewModel();
        //    _previewImageViewModel.SetImage(imageData);
        //    var dialog = new DialogViewModel(_previewImageViewModel);
        //    dialog.Title(title).Height(WindowHeight).Width(WindowWidth).ResizeMode(System.Windows.ResizeMode.CanResize).Show();
        //}

     
    }
}
