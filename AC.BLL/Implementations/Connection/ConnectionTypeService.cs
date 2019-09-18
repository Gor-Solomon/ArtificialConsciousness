using AC.BLL.Common;
using AC.BLL.Interfaces.Connections;
using AC.BLL.Models.Connections;
using AC.BLL.Resources;
using AC.DAL.Repositories.Interfaces.Connections;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Implementations.Connection
{
    public class ConnectionTypeService : BaseService, IConnectionTypeService
    {
        IConnectionTypeRepository _connectionTypeRepository;
        IMapper _mapper;

        public ConnectionTypeService(IConnectionTypeRepository connectionTypeRepository, IMapper mapper) : base(connectionTypeRepository)
        {
            _connectionTypeRepository = connectionTypeRepository;
            _mapper = mapper;
        }

        public async Task<BlResult> DeleteAsync(int id)
        {
            BlResult blResult = new BlResult();

            try
            {
                var entity = await _connectionTypeRepository.GetByIdWithReferencAsync(id);

                if (entity != null)
                {
                    EnsureTransaction();
                    _connectionTypeRepository.Delete(id);
                    await SaveChangesAsync();

                    blResult.Success();
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

        public async Task<BlResult<IEnumerable<ConnectionTypeModel>>> GetAllAsync()
        {
            BlResult<IEnumerable<ConnectionTypeModel>> blResult = new BlResult<IEnumerable<ConnectionTypeModel>>();

            try
            {
                var entity = await _connectionTypeRepository.GetAllWithReferencsAsync();
                blResult.Success(_mapper.Map<IEnumerable<ConnectionTypeModel>>(entity));
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex);
            }

            return blResult;
        }

        public async Task<BlResult<ConnectionTypeModel>> GetByIdAsync(int id)
        {
            BlResult<ConnectionTypeModel> blResult = new BlResult<ConnectionTypeModel>();

            try
            {
                var entity = await _connectionTypeRepository.GetByIdAsync(id);
                blResult.Success(_mapper.Map<ConnectionTypeModel>(entity));
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex);
            }

            return blResult;
        }

        public async Task<BlResult<ConnectionTypeModel>> SaveAsync(ConnectionTypeModel connectionTypeModel)
        {
            BlResult<ConnectionTypeModel> blResult = new BlResult<ConnectionTypeModel>();

            try
            {
                if (connectionTypeModel is null)
                {
                    throw new ArgumentException();
                }

                EnsureTransaction();

                var fetchedEntity = await _connectionTypeRepository.GetByIdAsync(connectionTypeModel.Id);
                fetchedEntity = _mapper.Map(connectionTypeModel, fetchedEntity);
                var attributesToDelete = fetchedEntity.ConnectionTypeAttributes.
                   Where(x => !connectionTypeModel.ConnectionTypeAttributes.Any(y => y.Id == x.Id)).ToList();
                var attributesToAdd = connectionTypeModel.ConnectionTypeAttributes.
                    Where(x => !fetchedEntity.ConnectionTypeAttributes.Any(y => y.Id == x.Id)).Select(z => z.Id);

                if (connectionTypeModel.Id > 0)
                {
                    _connectionTypeRepository.Edit(fetchedEntity);
                }
                else
                {
                    fetchedEntity = _connectionTypeRepository.Add(fetchedEntity);
                }

                await SaveChangesAsync(false);

                if (attributesToDelete != null && attributesToDelete.Count() > 0)
                {
                    foreach (var item in attributesToDelete)
                    {
                        fetchedEntity.ConnectionTypeAttributes.Remove(item);
                    }
                    await SaveChangesAsync(false);
                }

                if (attributesToAdd != null && attributesToAdd.Count() > 0)
                {
                    await _connectionTypeRepository.AddAttribuets(fetchedEntity, attributesToAdd);
                    await SaveChangesAsync(false);
                }

                await SaveChangesAsync();

                blResult.Success(_mapper.Map<ConnectionTypeModel>(fetchedEntity));
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
    }
}
