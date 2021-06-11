namespace DataAccess.DataBaseLayer
{
    public class QuotationRepository : GenericRepository<Quotation>, IQuotationRepository
    {
        public QuotationRepository(DataContext context) : base(context)
        {
        }
    }
}
