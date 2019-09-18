using AC.Desktop.Controls.ViewModelBase;
using AC.Desktop.Controls.Dialgo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AC.Desktop.Controls.Dialgo.ViewModels
{
    public class DialogViewModel : ViewModel<DialogView>
    {
        public ICloseable Content { get; }
        public string DialogTitle { get; private set; }
        public double DialogWidth { get; set; }
        public double DialogHeight { get; set; }
        public int ContentMargin { get; set; }
        public ResizeMode Resize { get; set; }


        public bool IsCloseVisible { get; set; }

        public DialogViewModel()
        {
            Resize = System.Windows.ResizeMode.NoResize;
        }

        public DialogViewModel(ICloseable content, bool isCloseVisible = false)
        {
            Content = content;
            IsCloseVisible = isCloseVisible;
        }

        public DialogViewModel Title(string title)
        {
            DialogTitle = title;
            return this;
        }

        public DialogViewModel Width(double width)
        {
            DialogWidth = width;
            return this;
        }

        public DialogViewModel Height(double height)
        {
            DialogHeight = height;
            return this;
        }

        public DialogViewModel Margin(int margin)
        {
            ContentMargin = margin;
            return this;
        }

        public void Show()
        {
            Notify(() => DialogTitle);
            Notify(() => DialogHeight);
            Notify(() => DialogWidth);
            Notify(() => ContentMargin);
            View.ShowDialog();
        }

        public DialogViewModel ResizeMode(ResizeMode resize)
        {
            Resize = resize;
            return this;
        }
    }
}
