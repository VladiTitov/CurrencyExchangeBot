using System.Collections.Generic;

namespace BisinessLogic.Database
{
    public interface IBranchService
    {
        IEnumerable<BranchDTO> GetData();
        void Add(BranchDTO item);
        void Update(BranchDTO item);
        void Delete(BranchDTO item);
        BranchDTO GetWithInclude(BranchDTO item);
    }
}
