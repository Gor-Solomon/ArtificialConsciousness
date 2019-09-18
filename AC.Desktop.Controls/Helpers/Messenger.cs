
using System;

namespace AC.Desktop.Controls.Helpers
{
    public static class Messenger
    {
        public static Action OnPropertyChanged;
        public static Action<object> OnModelChanged;
        public static Action ReloadPage;

        public static void ModelChanged(object model)
        {
            OnModelChanged?.Invoke(model);
        }

        //public static void Broadcast(INavigationPage _action)
        //{
        //    OnPageChanged?.Invoke(_action);
        //}

        public static void BroadcastPropertyChanged()
        {
            OnPropertyChanged?.Invoke();
        }
    }
}
