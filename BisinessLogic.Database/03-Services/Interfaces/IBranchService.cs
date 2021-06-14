using System.Collections.Generic;

namespace BisinessLogic.Database
{
    interface IBranchService
    {
        IEnumerable<BranchDTO> GetData();
        void Add(BranchDTO item);
        void Update(BranchDTO item);
        void Delete(BranchDTO item);
    }
}
