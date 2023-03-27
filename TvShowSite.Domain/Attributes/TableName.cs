namespace TvShowSite.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableName : Attribute
    {
        public string? schemaName { get; set; }
        public string? tableName { get; set; }
        public TableName(string? tableName, string? schemaName = "site")
        {
            this.schemaName = schemaName;
            this.tableName = tableName;
        }
    }
}
