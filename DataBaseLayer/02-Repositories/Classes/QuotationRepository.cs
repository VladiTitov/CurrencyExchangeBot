using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataBaseLayer
{
    public class QuotationRepository : GenericRepository<Quotation>, IQuotationRepository
    {
        private readonly DataContext _context;

        public QuotationRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        //public override async Task Add(Quotation item)
        //{
        //    var branchDto = item.Branch;
        //    var currencyDto = item.Currency;

        //    item.Branch = 
        //        await _context.Branches.FirstOrDefaultAsync(i => i.Name.Equals(branchDto.Name) && i.Adr.Equals(branchDto.Adr));
        //    item.Currency = 
        //        await _context.Currencies.FirstOrDefaultAsync(i => i.NameRus.Equals(currencyDto.NameRus));

        //    _context.Set<Quotation>().Add(item);
        //    _context.SaveChanges();
        //}

        //public override IEnumerable<Quotation> GetAll() => 
        //    _context.Quotations.Include(i => i.Branch).Include(p => p.Currency).ToList();
    }
}
