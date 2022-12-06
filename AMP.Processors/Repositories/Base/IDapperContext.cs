using System.Data;
using System.Data.Common;
using Dapper;
namespace AMP.Processors.Repositories.Base;

public interface IDapperContext : IDisposable
{
    DbConnection GetDbconnection();    
    Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    Task<T> GetAsync<T>(string sp, DynamicParameters parms,
        DbTransaction transaction, DbConnection connection, CommandType commandType = CommandType.StoredProcedure);
    Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);    
    Task<int> Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);    
    T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);    
    Task<int> Update(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);    
}