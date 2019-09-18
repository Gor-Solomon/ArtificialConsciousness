using AC.Agnets.Categories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.BLL.Interfaces.Attribuets;
using AC.BLL.Interfaces.Connections;
using AC.BLL.Interfaces.Graph;
using AC.BLL.Interfaces.Node;

namespace AC.Agnets.Categories.Implementations
{
    public class LogicAgent : Agent, ILogicAgent
    {
        #region Fields
        IAttribuetDescriptionService _attribuetDescriptionService;
        IConnectionService _connectionService;
        IConnectionTypeService _connectionTypeService;
        IGraphService _graphService;
        INodeService _nodeService;
        #endregion

        public LogicAgent(IAttribuetDescriptionService attribuetDescriptionService, IConnectionService connectionService,
                IConnectionTypeService connectionTypeService, IGraphService graphService, INodeService nodeService)
        {
            _attribuetDescriptionService = attribuetDescriptionService;
            _connectionService = connectionService;
            _connectionTypeService = connectionTypeService;
            _graphService = graphService;
            _nodeService = nodeService;
        }

        public string AnswerQuestion(string input)
        {
            throw new NotImplementedException();
        }

        public string ProcessInput(string input)
        {
            throw new NotImplementedException();
        }
    }
}
