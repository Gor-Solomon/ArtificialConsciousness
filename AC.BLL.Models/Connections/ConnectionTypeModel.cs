using AC.BLL.Models.Attribuets;
using AC.BLL.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Models.Connections
{
    public class ConnectionTypeModel : BlModelBase, IBllModel
    {
        private string _connectionRule;
        private ObservableCollection<AttributeDescriptionModel> _connectionTypeAttributes;
        private ObservableCollection<ConnectionModel> _A2BConnections;
        private ObservableCollection<ConnectionModel> _B2AConnections;

        public ConnectionTypeModel()
        {
            ConnectionTypeAttributes = new ObservableCollection<AttributeDescriptionModel>();
            A2BConnections = new ObservableCollection<ConnectionModel>();
            B2AConnections = new ObservableCollection<ConnectionModel>();
        }

        public string ConnectionRule
        {
            get { return _connectionRule; }
            set
            {
                if (_connectionRule != value)
                {
                    _connectionRule = value;
                    Notify();
                }
            }
        }

        public ObservableCollection<AttributeDescriptionModel> ConnectionTypeAttributes
        {
            get { return _connectionTypeAttributes; }
            set
            {
                if (_connectionTypeAttributes != value)
                {
                    _connectionTypeAttributes = value;
                    Notify();
                }
            }
        }

        public ObservableCollection<ConnectionModel> A2BConnections
        {
            get { return _A2BConnections; }
            set
            {
                if (_A2BConnections != value)
                {
                    _A2BConnections = value;
                    Notify();
                }
            }
        }

        public ObservableCollection<ConnectionModel> B2AConnections
        {
            get { return _B2AConnections; }
            set
            {
                if (_B2AConnections != value)
                {
                    _B2AConnections = value;
                    Notify();
                }
            }
        }
    }
}
