using AC.BLL.Common;
using AC.BLL.Interfaces.Graph;
using AC.BLL.Interfaces.Node;
using AC.BLL.Models.Attribuets;
using AC.BLL.Models.Graphs;
using AC.BLL.Models.Nodes;
using AC.BLL.Resources;
using AC.DAL.Repositories.Interfaces.Graphs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Implementations.Graph
{
    public class GraphService : BaseService, IGraphService
    {
        IGraphRepository _graphRepository;
        INodeService _nodeService;
        IMapper _mapper;
        public GraphService(IGraphRepository graphRepository, INodeService nodeService, IMapper mapper)
            : base(graphRepository)

        {
            _graphRepository = graphRepository;
            _nodeService = nodeService;
            _mapper = mapper;
        }
        public async Task<BlResult> DeleteAsync(int id)
        {
            BlResult blResult = new BlResult();

            try
            {
                var entity = await _graphRepository.GetByIdWithReferencAsync(id);

                if (entity != null)
                {
                    if (entity.Nodes.Count > 0)
                    {
                        blResult.Fail(ConstDictionary.CantDeleteGraphWithChildes);
                    }
                    else
                    {
                        EnsureTransaction();
                        _graphRepository.Delete(id);
                        await SaveChangesAsync();

                        blResult.Success();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                blResult.Fail(BLErrorCodeTypeEnum.NoSuchEntityExistsWithID);
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex);
            }

            return blResult;
        }

        public async Task<BlResult<IEnumerable<GraphModel>>> GetAllAsync()
        {
            BlResult<IEnumerable<GraphModel>> blResult = new BlResult<IEnumerable<GraphModel>>();

            try
            {
                var entity = await _graphRepository.GetAllAsync();
                blResult.Success(_mapper.Map<IEnumerable<GraphModel>>(entity));
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex);
            }

            return blResult;
        }

        public async Task<BlResult<GraphModel>> GetByIdAsync(int graphId)
        {
            BlResult<GraphModel> blResult = new BlResult<GraphModel>();

            try
            {
                var entity = await _graphRepository.GetByIdWithReferencAsync(graphId);
                blResult.Success(_mapper.Map<GraphModel>(entity));
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex);
            }

            return blResult;

        }

        public async Task<BlResult<GraphModel>> SaveAsync(GraphModel graphModel)
        {
            BlResult<GraphModel> blResult = new BlResult<GraphModel>();

            try
            {
                if (graphModel is null)
                {
                    throw new ArgumentException();
                }

                EnsureTransaction();

                var fetchedEntity = await _graphRepository.GetByIdWithReferencAsync(graphModel.Id);
                fetchedEntity = _mapper.Map(graphModel, fetchedEntity);

                if (graphModel.Id > 0)
                {
                    _graphRepository.Edit(fetchedEntity);
                }
                else
                {

                    fetchedEntity = _graphRepository.Add(fetchedEntity);
                    await SaveChangesAsync();

                    NodeModel nodeModel = new NodeModel()
                    {
                        Name = fetchedEntity.Name,
                        GraphId = fetchedEntity.Id,
                        Data = "Entery node for graph " + fetchedEntity.Name,
                        NodeAttributes = new System.Collections.ObjectModel.ObservableCollection<AttributeDescriptionModel>()
                        { new AttributeDescriptionModel(){ Id = (int)AttributesTypesEnum.EnteryNode  }  }
                    };

                    if (fetchedEntity.IsAbstract)
                    {
                        nodeModel.NodeAttributes.Add(new AttributeDescriptionModel() { Id = (int)AttributesTypesEnum.Abstract });
                    }

                    var result = await _nodeService.SaveAsync(nodeModel);

                    if (result.Succeed)
                    {
                        EnsureTransaction();
                        fetchedEntity.EnteryNodeId = result.Value.Id;
                        _graphRepository.Edit(fetchedEntity);
                    }
                    else
                    {
                        RollbackTransaction();
                        throw result.Exception;
                    }
                }

                await SaveChangesAsync();

                blResult.Success(_mapper.Map<GraphModel>(fetchedEntity));
            }
            catch (ArgumentException)
            {
                blResult.Fail(ConstDictionary.CantSaveNullModel);
            }
            catch (Exception ex)
            {
                blResult.Fail(ex);
            }

            return blResult;
        }

        public override void Dispose()
        {
            base.Dispose();
            _nodeService.Dispose();
        }
    }
}
