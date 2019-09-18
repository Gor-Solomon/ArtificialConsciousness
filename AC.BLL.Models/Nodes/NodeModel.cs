using AC.BLL.Models.Attribuets;
using AC.BLL.Models.Connections;
using AC.BLL.Models.Graphs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Models.Nodes
{
    public class NodeModel : BlModelBase
    {
        string _data;
        int _graphId;
        GraphModel _graph;
        ObservableCollection<ConnectionModel> _connectionsFrom;
        ObservableCollection<ConnectionModel> _connectionsTo;
        ObservableCollection<AttributeDescriptionModel> _nodeAttributes;

        public NodeModel()
        {
            Graph = new GraphModel();
            ConnectionsFrom = new ObservableCollection<ConnectionModel>();
            ConnectionsTo = new ObservableCollection<ConnectionModel>();
            NodeAttributes = new ObservableCollection<AttributeDescriptionModel>();
        }

        public string Data
        {
            get { return _data; }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    Notify();
                };
            }
        }

        public int GraphId
        {
            get { return _graphId; }
            set
            {
                if (_graphId != value)
                {
                    _graphId = value;
                    Notify();
                };
            }
        }

        public GraphModel Graph
        {
            get { return _graph; }
            set
            {
                if (_graph != value)
                {
                    _graph = value;
                    Notify();
                };
            }
        }

        public ObservableCollection<ConnectionModel> ConnectionsFrom
        {
            get { return _connectionsFrom; }
            set
            {
                if (_connectionsFrom != value)
                {
                    _connectionsFrom = value;
                    Notify();
                };
            }
        }

        public ObservableCollection<ConnectionModel> ConnectionsTo
        {
            get { return _connectionsTo; }
            set
            {
                if (_connectionsTo != value)
                {
                    _connectionsTo = value;
                    Notify();
                };
            }
        }

        public ObservableCollection<AttributeDescriptionModel> NodeAttributes
        {
            get { return _nodeAttributes; }
            set
            {
                if (_nodeAttributes != value)
                {
                    _nodeAttributes = value;
                    Notify();
                };
            }
        }
    }
}
