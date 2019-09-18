using AC.BLL.Common;
using AC.BLL.Interfaces.Connections;
using AC.BLL.Models.Common;
using AC.BLL.Models.Connections;
using AC.DAL.Repositories.Interfaces.Connections;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Implementations.Connection
{
    public class ConnectionService : BaseService, IConnectionService
    {
        IConnectionRepository _connectionRepository;
        IMapper _mapper;

        public ConnectionService(IConnectionRepository connectionRepository, IMapper mapper) : base(connectionRepository)
        {
            _connectionRepository = connectionRepository;
            _mapper = mapper;
        }

        public async Task<BlResult> DeleteAsync(int id)
        {
            BlResult blResult = new BlResult();

            try
            {
                var entity = await _connectionRepository.GetByIdAsync(id);

                if (entity != null)
                {
                    EnsureTransaction();
                    _connectionRepository.Delete(id);
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

        public async Task<BlResult<IEnumerable<ConnectionModel>>> GetAllAsync()
        {
            BlResult<IEnumerable<ConnectionModel>> blResult = new BlResult<IEnumerable<ConnectionModel>>();

            try
            {
                var entity = await _connectionRepository.GetAllAsync();
                blResult.Success(_mapper.Map<IEnumerable<ConnectionModel>>(entity));
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex);
            }

            return blResult;
        }

        public async Task<BlResult<IEnumerable<SimpleModel>>> GetAllByNameAndIdAsync()
        {
            BlResult<IEnumerable<SimpleModel>> blResult = new BlResult<IEnumerable<SimpleModel>>();

            try
            {
                var entity = await _connectionRepository.GetConnectionsByNameAndIdAsync();
                var result = entity.ToList().Select(x => new SimpleModel(x.Key, x.Value)).ToList();
                blResult.Success(result);
            }
            catch (Exception ex)
            {
                blResult.Fail(ex);
            }

            return blResult;
        }

        public async Task<BlResult<ConnectionModel>> GetByIdAsync(int connectionId)
        {
            BlResult<ConnectionModel> blResult = new BlResult<ConnectionModel>();

            try
            {
                var entity = await _connectionRepository.GetByIdWithReferencAsync(connectionId);
                blResult.Success(_mapper.Map<ConnectionModel>(entity));
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex);
            }

            return blResult;
        }

        public async Task<BlResult<ConnectionModel>> SaveAsync(ConnectionModel connectionModel)
        {
            BlResult<ConnectionModel> blResult = new BlResult<ConnectionModel>();

            try
            {
                if (connectionModel is null)
                {
                    throw new ArgumentNullException(nameof(connectionModel));
                }

                EnsureTransaction();

                var fetchedEntity = await _connectionRepository.GetByIdWithReferencAsync(connectionModel.Id);
                fetchedEntity = _mapper.Map(connectionModel, fetchedEntity);
                fetchedEntity.OverwritesId = null;

                if (connectionModel.Id > 0)
                {
                    _connectionRepository.Edit(fetchedEntity);
                }
                else
                {
                    fetchedEntity = _connectionRepository.Add(fetchedEntity);
                }

                await SaveChangesAsync();
                blResult.Success(_mapper.Map<ConnectionModel>(fetchedEntity));

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
