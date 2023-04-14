﻿using Dapper;
using System.Reflection;
using TvShowSite.Core.Abstractions.DataAbstractions.Common;
using TvShowSite.Core.ExtensionMethods;
using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;
using static Dapper.SqlMapper;

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

        protected async Task<IEnumerable<T>> QueryAsync(string query, Dictionary<string, object> dict)
        {
            using (var connection = _connection.Connection)
            {
                return await connection.QueryAsync<T>(query, dict);
            }
        }

        protected async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query, Dictionary<string, object> dict)
        {
            using (var connection = _connection.Connection)
            {
                return await connection.QueryAsync<TEntity>(query, dict);
            }
        }

        protected async Task<T> QueryFirstOrDefaultAsync(string query, Dictionary<string, object> dict)
        {
            using (var connection = _connection.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<T>(query, dict);
            }
        }

        protected async Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(string query, Dictionary<string, object> dict)
        {
            using (var connection = _connection.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<TEntity>(query, dict);
            }
        }

        protected async Task InsertAsync(T entity)
        {
            var generatedValues = GenerateInsertSQL(entity);

            using (var connection = _connection.Connection)
            {
                await connection.QueryFirstOrDefaultAsync<T>(generatedValues.query, generatedValues.dict);
            }
        }

        private (string query, Dictionary<string, object> dict) GenerateInsertSQL<TEntity>(TEntity entity)
        {
            var columnNames = new List<string>();

            var dict = new Dictionary<string, object>();

            var props = typeof(TEntity).GetProperties();

            foreach(var prop in props)
            {
                var skipInsert = prop.GetCustomAttribute<SkipInsert>();

                if(skipInsert is null)
                {
                    var propValue = prop.GetValue(entity);

                    if(propValue is not null && !propValue.IsNullOrDefault())
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
