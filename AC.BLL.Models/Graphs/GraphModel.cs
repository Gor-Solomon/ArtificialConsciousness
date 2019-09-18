using AC.BLL.Models.Connections;
using AC.BLL.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Models.Graphs
{
    public class GraphModel : BlModelBase, IBllModel
    {
        #region Fildes
        int _enteryNodeId;
        string _description;
        bool _isAbstract;
        NodeModel _enteryNode;
        ObservableCollection<NodeModel> _nodes;
        ObservableCollection<ConnectionModel> _connections;
        #endregion

        #region Properties
        public int EnteryNodeId
        {
            get { return _enteryNodeId; }
            set
            {
                if (_enteryNodeId != value)
                {
                    _enteryNodeId = value;
                    Notify();
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    Notify();
                }
            }
        }
        public NodeModel EnteryNode
        {
            get { return _enteryNode; }
            set
            {
                if (_enteryNode != value)
                {
                    _enteryNode = value;
                    if (value != null)
                    {
                        _enteryNodeId = value.Id;
                    }
                    Notify();
                }
            }
        }

        public bool IsAbstract
        {
            get { return _isAbstract; }
            set
            {
                if (_isAbstract != value)
                {
                    _isAbstract = value;
                    Notify();
                }
            }
        }

        public ObservableCollection<NodeModel> Nodes
        {
            get { return _nodes; }
            set
            {
                if (_nodes != value)
                {
                    _nodes = value;
                    Notify();
                }
            }
        }

        public ObservableCollection<ConnectionModel> Connections
        {
            get { return _connections; }
            set
            {
                if (_connections != value)
                {
                    _connections = value;
                    Notify();
                }
            }
        }
        #endregion
    }
}
