using Dapper;
using System.Reflection;
using TvShowSite.Core.Abstractions.DataAbstractions.Common;
using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Data.Common
{
    public class BaseBagRepository<T> where T : CommonEntity
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
        public BaseBagRepository(ICustomDbConnection connection)
        {
            _connection = connection;
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

        public async Task<IEnumerable<T>> QueryAsync(string query, Dictionary<string, object> dict)
        {
            using (var connection = _connection.Connection)
            {
                return await connection.QueryAsync<T>(query, dict);
            }
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query, Dictionary<string, object> dict)
        {
            using (var connection = _connection.Connection)
            {
                return await connection.QueryAsync<TEntity>(query, dict);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync(string query, Dictionary<string, object> dict)
        {
            using (var connection = _connection.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<T>(query, dict);
            }
        }

        public async Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(string query, Dictionary<string, object> dict)
        {
            using (var connection = _connection.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<TEntity>(query, dict);
            }
        }

        public async Task InsertAsync(T entity, int userId)
        {
            entity.InsertDate = DateTime.Now;
            entity.InsertedBy = userId;

            var generatedValues = GenerateInsertSQL(entity);

            using (var connection = _connection.Connection)
            {
                await connection.ExecuteAsync(generatedValues.query, generatedValues.dict);
            }
        }

        private (string query, Dictionary<string, object> dict) GenerateInsertSQL<TEntity>(TEntity entity)
        {
            var columnNames = new List<string>();

            var dict = new Dictionary<string, object>();

            var props = typeof(TEntity).GetProperties();

            foreach (var prop in props)
            {
                var skipInsert = prop.GetCustomAttribute<SkipInsert>();

                if (skipInsert is null)
                {
                    var propValue = prop.GetValue(entity);

                    if (propValue is not null)
                    {
                        columnNames.Add(prop.Name);

                        dict.Add(prop.Name, propValue);
                    }
                }
            }

            string sql = $@"
                INSERT INTO {SchemaName}.{TableName} ({string.Join(",", columnNames)})
                VALUES ({string.Join(",", dict.Keys.Select(k => "@" + k))})
            ";

            return (sql, dict);
        }
    }
}
