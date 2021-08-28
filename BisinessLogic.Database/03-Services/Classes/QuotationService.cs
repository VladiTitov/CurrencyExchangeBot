using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.DataBaseLayer;
using BusinessLogic.Database.Interfaces;

namespace BusinessLogic.Database.Classes
{
    public class QuotationService : IQuotationService
    {
        private readonly IQuotationRepository _quotationRepository;
        private readonly IMapper _mapper;

        public QuotationService(IQuotationRepository quotationRepository, IMapper mapper)
        {
            _quotationRepository = quotationRepository;
            _mapper = mapper;
        }

        public IEnumerable<QuotationDTO> GetData() =>
            _mapper.Map<List<QuotationDTO>>(_quotationRepository.GetAll());

        public async Task Add(QuotationDTO item)
        {
            if (await IsExist(item)) _quotationRepository.Add(_mapper.Map<Quotation>(item));
            else await Update(item);
        }


        public async Task Update(QuotationDTO item)
        {
            var itemInDb = await _quotationRepository.GetByIdAsync(item.Id);
            itemInDb.Buy = item.Buy;
            itemInDb.Sale = item.Sale;
            _quotationRepository.Update(_mapper.Map<Quotation>(itemInDb));
        }
        
        public void Delete(QuotationDTO item) =>
            _quotationRepository.Delete(_mapper.Map<Quotation>(item));

        public async Task<bool> IsExist(QuotationDTO item)
        {
           var obj = await _quotationRepository.GetByIdAsync(item.Id);
           return obj == null;
        }
    }
}
