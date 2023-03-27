namespace TvShowSite.Domain.Common
{
    public class BaseEntity : CommonEntity
    {
        public int Id { get; set; }
        
    }

    public class CommonEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
