using AC.BLL.Common;
using AC.BLL.Interfaces.Attribuets;
using AC.BLL.Models.Attribuets;
using AC.BLL.Resources;
using AC.DAL.Repositories.Interfaces.Attribuets;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Implementations.Attribuet
{
    public class AttribuetDescriptionService : BaseService, IAttribuetDescriptionService
    {
        IAttribuetDescriptionRepository _attribuetDescriptionRepository;
        IMapper _mapper;
        public AttribuetDescriptionService(IAttribuetDescriptionRepository attribuetDescriptionRepository, IMapper mapper) : base(attribuetDescriptionRepository)
        {
            _attribuetDescriptionRepository = attribuetDescriptionRepository;
            _mapper = mapper;
        }
        public async Task<BlResult> DeleteAsync(int id)
        {
            BlResult blResult = new BlResult();

            try
            {
                var entity = await _attribuetDescriptionRepository.GetByIdAsync(id);

                if (entity != null)
                {

                    EnsureTransaction();
                    _attribuetDescriptionRepository.Delete(id);
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

        public async Task<BlResult<IEnumerable<AttributeDescriptionModel>>> GetAllAsync()
        {
            BlResult<IEnumerable<AttributeDescriptionModel>> blResult = new BlResult<IEnumerable<AttributeDescriptionModel>>();

            try
            {
                var entity = await _attribuetDescriptionRepository.GetAllAsync();
                blResult.Success(_mapper.Map<IEnumerable<AttributeDescriptionModel>>(entity));
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex);
            }

            return blResult;
        }

        public async Task<BlResult<AttributeDescriptionModel>> GetByIdAsync(int id)
        {
            BlResult<AttributeDescriptionModel> blResult = new BlResult<AttributeDescriptionModel>();

            try
            {
                var entity = await _attribuetDescriptionRepository.GetByIdAsync(id);
                blResult.Success(_mapper.Map<AttributeDescriptionModel>(entity));
            }
            catch (Exception ex)
            {
                blResult.Fail(BLErrorCodeTypeEnum.Unknown, ex);
            }

            return blResult;
        }

        public async Task<BlResult<AttributeDescriptionModel>> SaveAsync(AttributeDescriptionModel attributeDescriptionModel)
        {
            BlResult<AttributeDescriptionModel> blResult = new BlResult<AttributeDescriptionModel>();

            try
            {
                if (attributeDescriptionModel is null)
                {
                    throw new ArgumentNullException();
                }

                if (attributeDescriptionModel.Id == 0 && await _attribuetDescriptionRepository.ExistsEntityWithSuchAName(attributeDescriptionModel.Name))
                {
                    throw new Exception("Duplicated Attribute Name");
                }

                EnsureTransaction();

                var fetchedEntity = await _attribuetDescriptionRepository.GetByIdAsync(attributeDescriptionModel.Id);
                fetchedEntity = _mapper.Map(attributeDescriptionModel, fetchedEntity);

                if (attributeDescriptionModel.Id > 0)
                {
                    _attribuetDescriptionRepository.Edit(fetchedEntity);
                }
                else
                {
                    fetchedEntity = _attribuetDescriptionRepository.Add(fetchedEntity);
                }

                await SaveChangesAsync();

                blResult.Success(_mapper.Map<AttributeDescriptionModel>(fetchedEntity));
            }
            catch (ArgumentNullException)
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
