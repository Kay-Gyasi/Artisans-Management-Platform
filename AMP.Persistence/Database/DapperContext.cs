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
    //private const string ConnectionString = "AmpDevDb";
    private const string ConnectionString = "AmpProdDb";

    public  DapperContext(IConfiguration config)  
    {  
        _config = config;  
    }  
    public void Dispose()  
    {  
         
    }  

    public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)  
    {  
        throw new NotImplementedException();  
    }  

    public async Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)  
    {
        using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));  
        return (await db.QueryAsync<T>(sp, parms, commandType: commandType)).FirstOrDefault();  
    }  

    public async Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)  
    {  
        using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));  
        return (await db.QueryAsync<T>(sp, parms, commandType: commandType)).ToList();  
    }  

    public DbConnection GetDbconnection()  
    {  
        return new SqlConnection(_config.GetConnectionString(ConnectionString));  
    }  

    public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)  
    {  
        T result;  
        using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));  
        try  
        {  
            if (db.State == ConnectionState.Closed)  
                db.Open();  

            using var tran = db.BeginTransaction();  
            try  
            {  
                result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();  
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
            if (db.State == ConnectionState.Open)  
                db.Close();  
        }  

        return result;  
    }  

    public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)  
    {  
        T result;  
        using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));  
        try  
        {  
            if (db.State == ConnectionState.Closed)  
                db.Open();  

            using var tran = db.BeginTransaction();  
            try  
            {  
                result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();  
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
            if (db.State == ConnectionState.Open)  
                db.Close();  
        }  

        return result;  
    }
}  