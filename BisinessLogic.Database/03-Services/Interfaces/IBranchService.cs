using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDTO>> GetData();
        Task<BranchDTO> GetWithInclude(BranchDTO item);
        Task Add(BranchDTO item);
        Task Update(BranchDTO item);
        Task Delete(BranchDTO item);
        Task<bool> IsExist(BranchDTO item);
    }
}
