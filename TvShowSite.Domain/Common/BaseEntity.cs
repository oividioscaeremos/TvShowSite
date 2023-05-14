using TvShowSite.Domain.Attributes;

namespace TvShowSite.Domain.Common
{
    public class BaseEntity : CommonEntity
    {
        [SkipInsert]
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

    public class BaseResponse
    {
        public List<string> ErrorList { get; set; }
        public List<string> WarningList { get; set; }
        public bool Status => this.ErrorList.Any() == false;

        public BaseResponse()
        {
            this.ErrorList = new List<string>();
            this.WarningList = new List<string>();
        }
    }

    public class BaseResponse<T> where T : class
    {
        public T? Value { get; set; }
        public List<string> ErrorList { get; set; }
        public List<string> WarningList { get; set; }
        public bool Status => this.ErrorList.Any() == false;

        public BaseResponse()
        {
            this.ErrorList = new List<string>();
            this.WarningList = new List<string>(); 
        }
    }
}
