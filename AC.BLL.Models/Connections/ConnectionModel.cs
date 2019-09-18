using AC.BLL.Models.Attribuets;
using AC.BLL.Models.Graphs;
using AC.BLL.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Models.Connections
{
    public class ConnectionModel : BlModelBase, IBllModel
    {
        #region Fields
        private int _nodeAId;
        private int _nodeBId;
        private int _A2BConnectionTypeId;
        private int _B2AConnectionTypeId;
        private decimal _weightA2B;
        private decimal _weightB2A;
        private decimal _overwritesId;
        private int _graphId;
        private ConnectionTypeModel _a2BConnectionType;
        private ConnectionTypeModel _b2AConnectionType;
        private List<ConnectionModel> _overwriters;
        private ConnectionModel _overwrite;
        private GraphModel _graph;
        private NodeModel _nodeA;
        private NodeModel _nodeB;
        #endregion

        #region Constructor
        public ConnectionModel()
        {
            A2BConnectionType = new ConnectionTypeModel();
            B2AConnectionType = new ConnectionTypeModel();
            Overwriters = new List<ConnectionModel>();
            Graph = new GraphModel();
            NodeA = new NodeModel();
            NodeB = new NodeModel();
        }
        #endregion

        #region Properties

        public override string Name
        {
          get => NodeA.Name + ' ' + A2BConnectionType.Name + ' ' + NodeB.Name;
          set => base.Name = value;
        }
        public int NodeAId
        {
            get { return _nodeAId; }
            set
            {
                if (_nodeAId != value)
                {
                    _nodeAId = value;
                    Notify();
                }
            }
        }

        public int NodeBId
        {
            get { return _nodeBId; }
            set
            {
                if (_nodeBId != value)
                {
                    _nodeBId = value;
                    Notify();
                }
            }
        }

        public int A2BConnectionTypeId
        {
            get { return _A2BConnectionTypeId; }
            set
            {
                if (_A2BConnectionTypeId != value)
                {
                    _A2BConnectionTypeId = value;
                    Notify();
                }
            }
        }

        public int B2AConnectionTypeId
        {
            get { return _B2AConnectionTypeId; }
            set
            {
                if (_B2AConnectionTypeId != value)
                {
                    _B2AConnectionTypeId = value;
                    Notify();
                }
            }
        }

        public decimal WeightA2B
        {
            get { return _weightA2B; }
            set
            {
                if (_weightA2B != value)
                {
                    _weightA2B = value;
                    Notify();
                }
            }
        }

        public decimal WeightB2A
        {
            get { return _weightB2A; }
            set
            {
                if (_weightB2A != value)
                {
                    _weightB2A = value;
                    Notify();
                }
            }
        }

        public decimal OverwritesId
        {
            get { return _overwritesId; }
            set
            {
                if (_overwritesId != value)
                {
                    _overwritesId = value;
                    Notify();
                }
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
                }
            }
        }

        public ConnectionTypeModel A2BConnectionType
        {
            get { return _a2BConnectionType; }
            set
            {
                if (_a2BConnectionType != value)
                {
                    _a2BConnectionType = value;
                    Notify();
                }
            }
        }

        public ConnectionTypeModel B2AConnectionType
        {
            get { return _b2AConnectionType; }
            set
            {
                if (_b2AConnectionType != value)
                {
                    _b2AConnectionType = value;
                    Notify();
                }
            }
        }

        public List<ConnectionModel> Overwriters
        {
            get { return _overwriters; }
            set
            {
                if (_overwriters != value)
                {
                    _overwriters = value;
                    Notify();
                }
            }
        }

        public ConnectionModel Overwrite
        {
            get { return _overwrite; }
            set
            {
                if (_overwrite != value)
                {
                    _overwrite = value;
                    Notify();
                }
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
                }
            }
        }

        public NodeModel NodeA
        {
            get { return _nodeA; }
            set
            {
                if (_nodeA != value)
                {
                    _nodeA = value;
                    Notify();
                }
            }
        }

        public NodeModel NodeB
        {
            get { return _nodeB; }
            set
            {
                if (_nodeB != value)
                {
                    _nodeB = value;
                    Notify();
                }
            }
        }
        #endregion
    }
}
