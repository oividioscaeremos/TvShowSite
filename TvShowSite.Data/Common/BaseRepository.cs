using Dapper;
using System.Reflection;
using TvShowSite.Core.Abstractions.DataAbstractions.Common;
using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Data.Common
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private string SchemaName
        {
            get
            {
                var customAttribute = typeof(T).GetCustomAttribute<TableName>();

                if (customAttribute?.schemaName is not null)
                {
                    return customAttribute.schemaName;
                }
                else
                {
                    throw new ArgumentNullException("TableName attribute does not exist on type " + typeof(T).Name);
                }
            }
        }

        private string TableName
        {
            get
            {
                var customAttribute = typeof(T).GetCustomAttribute<TableName>();

                if(customAttribute?.tableName is not null)
                {
                    return customAttribute.tableName;
                }
                else
                {
                    throw new ArgumentNullException("TableName attribute does not exist on type " + typeof(T).Name);
                }
            }
        }

        protected readonly ICustomDbConnection _connection;
        public BaseRepository(ICustomDbConnection connection)
        {
            _connection = connection;
        }

        public async Task DeleteByIdAsync(int id)
        {
            using(var connection = _connection.Connection)
            {
                await connection.ExecuteAsync($@"
                    DELETE FROM ""{SchemaName}"".""{TableName}""
                    WHERE Id = @Id
                ", new Dictionary<string, object>
                {
                    { "Id", id }
                });
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = _connection.Connection)
            {
                return await connection.QueryAsync<T>($@"
                    SELECT * FROM {SchemaName}.{TableName}
                ", new Dictionary<string, object> { });
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (var connection = _connection.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<T>($@"
                    SELECT * FROM {SchemaName}.{TableName}
                    WHERE Id = @Id
                ", new Dictionary<string, object>
                {
                    { "Id", id }
                });
            }
        }
    }
}
