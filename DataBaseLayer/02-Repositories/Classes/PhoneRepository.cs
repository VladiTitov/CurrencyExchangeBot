using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataBaseLayer
{
    public class PhoneRepository : GenericRepository<Phone>, IPhoneRepository
    {
        private readonly DataContext _context;

        public PhoneRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task Add(Phone item)
        {
            var branchDto = item.Branch;

            item.Branch =
            await _context.Branches.FirstOrDefaultAsync(i =>
                i.Name.Equals(branchDto.Name) && i.Adr.Equals(branchDto.Adr));
            _context.Set<Phone>().Add(item);
            _context.SaveChanges();
        }

        public override IEnumerable<Phone> GetAll() => 
            _context.Phones.Include(i => i.Branch).ToList();
    }
}
