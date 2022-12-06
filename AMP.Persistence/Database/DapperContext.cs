using System.Data;
using System.Data.Common;
using AMP.Processors.Repositories.Base;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AMP.Persistence.Database;

public class DapperContext : IDapperContext
{  
    private readonly IConfiguration _config;
    private readonly IDbConnection _db;

    //private const string ConnectionString = "AmpDevDb";
    private const string ConnectionString = "AmpProdDb";

    public DapperContext(IConfiguration config)
    {
        _config = config;
        _db = new SqlConnection(_config.GetConnectionString(ConnectionString));
    }  
    
    public void Dispose()  
    {  
         _db.Dispose();
    }  

    public async Task<int> Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
    {
        return await _db.ExecuteAsync(sp, parms, commandType: commandType);
    }  

    public async Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
    {
        return (await _db.QueryAsync<T>(sp, parms, commandType: commandType)).FirstOrDefault();  
    }
    
    public async Task<T> GetAsync<T>(string sp, DynamicParameters parms,
        DbTransaction transaction, DbConnection connection, CommandType commandType = CommandType.StoredProcedure)
    {
        return (await connection.QueryAsync<T>(sp, parms, commandType: commandType,
            transaction: transaction)).FirstOrDefault();  
    }  

    public async Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)  
    {  
        return (await _db.QueryAsync<T>(sp, parms, commandType: commandType)).ToList();  
    }  

    public DbConnection GetDbconnection() => new SqlConnection(_config.GetConnectionString(ConnectionString));

    public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)  
    {  
        T result;  
        try  
        {  
            if (_db.State == ConnectionState.Closed)  
                _db.Open();  

            using var tran = _db.BeginTransaction();  
            try  
            {  
                result = _db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();  
                tran.Commit();  
            }  
            catch (Exception ex)  
            {  
                tran.Rollback();  
                throw;  
            }  
        }  
        catch (Exception ex)  
        {  
            throw;  
        }  
        finally  
        {  
            if (_db.State == ConnectionState.Open)  
                _db.Close();  
        }  

        return result;  
    }  

    public async Task<int> Update(string sp, DynamicParameters parms, 
        CommandType commandType = CommandType.StoredProcedure)  
    {  
        int result;  
        try  
        {  
            if (_db.State == ConnectionState.Closed)  
                _db.Open();  

            using var tran = _db.BeginTransaction();  
            try  
            {  
                result = await _db.ExecuteAsync(sp, parms, commandType: commandType);
                tran.Commit();  
            }  
            catch (Exception ex)  
            {  
                tran.Rollback();  
                throw;  
            }  
        }  
        catch (Exception ex)  
        {  
            Console.WriteLine();
            throw;  
        }  
        finally  
        {  
            if (_db.State == ConnectionState.Open)  
                _db.Close();  
        }  

        return result;  
    }

    /// <summary>
    /// Set DateModified field
    /// </summary>
    /// <param name="table">Table being worked on</param>
    /// <param name="whereField">Field for writing WHERE clause (TSql)</param>
    /// <param name="whereResult">Value for WHERE clause field</param>
    /// <returns></returns>
    private async Task<int> SetLastModified(string table, string whereField, string whereResult)
    {
        return await _db.ExecuteAsync($"UPDATE {table} SET DateModified = GETDATE() WHERE {whereField} = '{whereResult}'",
            null, commandType: CommandType.Text);
    }
}  