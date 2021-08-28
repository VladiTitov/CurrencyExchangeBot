using System.Collections.Generic;

namespace BusinessLogic.Database.Interfaces
{
    public interface IBranchService
    {
        IEnumerable<BranchDTO> GetData();
        BranchDTO GetWithInclude(BranchDTO item);
        void Add(BranchDTO item);
        void Update(BranchDTO item);
        void Delete(BranchDTO item);
        bool IsExist(BranchDTO item);
    }
}
