using System.Data;
using System.Data.Common;
using Dapper;
namespace AMP.Processors.Repositories.Base;

public interface IDapperContext : IDisposable
{
    DbConnection GetDbconnection();    
    Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);    
    Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);    
    int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);    
    T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);    
    T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);    
}