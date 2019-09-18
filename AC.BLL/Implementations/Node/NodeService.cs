using AC.BLL.Common;
using AC.BLL.Interfaces;
using AC.BLL.Interfaces.Node;
using AC.BLL.Models.Nodes;
using AC.BLL.Resources;
using AC.DAL.Repositories.Interfaces;
using AC.DAL.Repositories.Interfaces.Nodes;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Implementations.Node
{
    public class NodeService : BaseService, INodeService
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IMapper _mapper;
        public NodeService(INodeRepository nodeRepository, IMapper mapper) : base(nodeRepository)
        {
            _nodeRepository = nodeRepository;
            _mapper = mapper;
        }

        public async Task<BlResult> DeleteAsync(int id)
        {
            BlResult blResult = new BlResult();

            try
            {
                var entity = await _nodeRepository.GetByIdWithReferencAsync(id);

                if (entity != null)
                {
                    if (entity.ConnectionsFrom.Count + entity.ConnectionsTo.Count > 0)
                    {
                        blResult.Fail(ConstDictionary.CantDeleteNodeWithConnections);
                    }
                    else
                    {
                        EnsureTransaction();
                        _nodeRepository.Delete(id);
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

        public async Task<BlResult<IEnumerable<NodeModel>>> GetByGraphsAsync(int graphId)
        {
            BlResult<IEnumerable<NodeModel>> result = new BlResult<IEnumerable<NodeModel>>();

            try
            {
                if (graphId == 0)
                {
                    throw new ArgumentException(nameof(graphId));
                }

                //EnsureTransaction();

                var entity = await _nodeRepository.GetNodesByGraphIdAsync(graphId);
                IEnumerable<NodeModel> nodeModels = _mapper.Map<IEnumerable<NodeModel>>(entity);
                result.Success(nodeModels);
            }
            catch (ArgumentException)
            {
                result.Fail(BLErrorCodeTypeEnum.CantFetchUncreatedEntity);
            }
            catch (Exception ex)
            {
                result.Fail(BLErrorCodeTypeEnum.Unknown, ex.Message, ex);
            }

            return result;
        }

        public async Task<BlResult<NodeModel>> GetByIdAsync(int id)
        {
            BlResult<NodeModel> blResult = new BlResult<NodeModel>();

            try
            {
                var entity = await _nodeRepository.GetByIdWithReferencAsync(id);
                blResult.Success(_mapper.Map<NodeModel>(entity));
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex);
            }

            return blResult;
        }

        public async Task<BlResult<Dictionary<int, string>>> GetNodesIdAndNameAsync()
        {
            BlResult<Dictionary<int, string>> result = new BlResult<Dictionary<int, string>>();

            try
            {
                var entity = await _nodeRepository.GetNodesIdAndNameAsync();
                result.Success(entity);
            }
            catch (Exception ex)
            {
                result.Fail(BLErrorCodeTypeEnum.Unknown, ex.Message, ex);
            }

            return result;
        }

        public async Task<BlResult<NodeModel>> SaveAsync(NodeModel nodeModel)
        {
            BlResult<NodeModel> blResult = new BlResult<NodeModel>();

            try
            {
                if (nodeModel is null)
                {
                    throw new ArgumentNullException(nameof(NodeModel));
                }

                EnsureTransaction();

                var fetchedEntity = await _nodeRepository.GetByIdAsync(nodeModel.Id);
                fetchedEntity = _mapper.Map(nodeModel, fetchedEntity);
                var attributesToDelete = fetchedEntity.NodeAttributes.
                  Where(x => !nodeModel.NodeAttributes.Any(y => y.Id == x.Id)).ToList();
                var attributesToAdd = nodeModel.NodeAttributes.
                    Where(x => !fetchedEntity.NodeAttributes.Any(y => y.Id == x.Id)).Select(z => z.Id);

                if (nodeModel.Id > 0)
                {
                    _nodeRepository.Edit(fetchedEntity);
                }
                else
                {
                    fetchedEntity = _nodeRepository.Add(fetchedEntity);
                }

                await SaveChangesAsync(false);

                if (attributesToDelete != null && attributesToDelete.Count() > 0)
                {
                    foreach (var item in attributesToDelete)
                    {
                        fetchedEntity.NodeAttributes.Remove(item);
                    }
                    await SaveChangesAsync(false);
                }

                if (attributesToAdd != null && attributesToAdd.Count() > 0)
                {
                    await _nodeRepository.AddAttribuets(fetchedEntity, attributesToAdd);
                    await SaveChangesAsync(false);
                }

                await SaveChangesAsync();

                blResult.Success(_mapper.Map<NodeModel>(fetchedEntity));

            }
            catch (ArgumentNullException)
            {
                blResult.Fail(BLErrorCodeTypeEnum.ArgumentIsNull);
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex.Message, ex);
            }

            return blResult;
        }
    }
}
