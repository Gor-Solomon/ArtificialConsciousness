using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Models
{
    [Serializable]
    public abstract class BlModelBase : INotifyPropertyChanged, IBllModel
    {
        int _id;
        string _name;
        public virtual int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    Notify();
                }
            }
        }

        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    Notify();
                }
            }
        }
      
        public virtual string DisplayName => Name;
        public virtual bool IsSelectedItemForView { get; set; }

        protected void Notify([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //protected void Notify(bool notifyMessenger = true, [CallerMemberName] string propertyName = null)
        //{
        //    if (notifyMessenger)
        //        //Messenger.BroadcastPropertyChanged();
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
