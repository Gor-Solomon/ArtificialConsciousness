using AC.BLL.Models;
using AC.Desktop.Controls.Extensions;

namespace AC.Desktop.Controls.Helpers
{
    public class ImageModel : BlModelBase
    {
        private int _index;
        private byte[] _data;
        private string _color;

        public string Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    Notify();
                }
            }
        }

        public int Index
        {
            get { return _index; }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    Notify();
                }
            }
        }

        public byte[] Data
        {
            get { return _data; }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    Notify();
                }
            }
        }
    }
}
