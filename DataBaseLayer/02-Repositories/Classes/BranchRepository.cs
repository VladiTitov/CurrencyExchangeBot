namespace DataAccess.DataBaseLayer
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(DataContext context) : base(context)
        {
        }
    }
}
