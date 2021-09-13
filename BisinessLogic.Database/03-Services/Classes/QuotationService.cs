using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.DataBaseLayer;
using BusinessLogic.Database.Interfaces;
using System.Linq;

namespace BusinessLogic.Database.Classes
{
    public class QuotationService : IQuotationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuotationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuotationDTO>> GetData() =>
            _mapper.Map<List<QuotationDTO>>(await _unitOfWork.QuotationRepository.GetAllAsync());

        public async Task Add(QuotationDTO item)
        {
            if (await IsExist(item)) _unitOfWork.QuotationRepository.Add(_mapper.Map<Quotation>(item));
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> IsExist(QuotationDTO item)
        {
            var data = await _unitOfWork.QuotationRepository.GetAllAsync();
            var quotation = data.FirstOrDefault(i => i.CurrencyId.Equals(item.CurrencyId) && i.BranchId.Equals(item.BranchId));
            if (quotation != null)
            {
                quotation.Buy = item.Buy;
                quotation.Sale = item.Sale;

                _unitOfWork.QuotationRepository.Update(quotation);
                return false;
            }
            return true;
        }
    }
}
