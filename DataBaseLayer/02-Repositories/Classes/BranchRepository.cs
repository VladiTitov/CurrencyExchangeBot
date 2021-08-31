using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataBaseLayer
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        private readonly DataContext _context;

        public BranchRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task Add(Branch item)
        {
            item.Bank = 
                await _context.Banks.FirstOrDefaultAsync(i => i.NameRus.Equals(item.Bank.NameRus));
            item.City = 
                await _context.Cities.FirstOrDefaultAsync(i => i.NameRus.Equals(item.City.NameRus));

            _context.Set<Branch>().Add(item);
            _context.SaveChanges();
        }

        public override IEnumerable<Branch> GetAll() => 
            _context.Branches.Include(i => i.Bank).Include(p=>p.City).ToList();
    }
}
