using AC.Desktop.Controls.ViewModelBase;

namespace AC.Desktop.Controls.Helpers
{
    public interface IValidateable : IViewModel
    {
        bool HasChanged();

        void DiscardChanges();

        void Clear();
    }
}
