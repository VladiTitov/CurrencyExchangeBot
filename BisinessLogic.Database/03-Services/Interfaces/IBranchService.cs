using System.Collections.Generic;
using System.Threading.Tasks;

namespace BisinessLogic.Database
{
    public interface IBranchService
    {
        IEnumerable<BranchDTO> GetData();
        public IEnumerable<BranchDTO> GetBranchInCity(int id);
        BranchDTO GetWithInclude(BranchDTO item);
        Task Add(BranchDTO item);
        Task Update(BranchDTO item);
        Task Delete(BranchDTO item);
    }
}
