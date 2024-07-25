using Dapper;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using TaskManagement.Common;

namespace TaskManagement.Data
{
    public class DataProvider
    {
        private readonly SqlConnection connection;
        public DataProvider() 
        { 
            this.connection = new SqlConnection(Constants.ConnectionString);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string tableName)
        {
                string query = string.Format(Constants.GetAll, tableName);
                return await connection.QueryAsync<T>(query);
        }


        public async Task<T> GetByIdAsync<T>(string tableName,string id)
        {
                string query = string.Format(Constants.GetById, tableName);
                return await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task<int> IncertAsync<T>(string tableName, T entity)
        {
            try
            {
                PropertyInfo[] Properties = typeof(T).GetProperties();
                string columns = string.Join(',', Properties.Select(p => $"{p.Name}"));
                string values = string.Join(',', Properties.Select(p => $"@{p.Name}"));
                string query = string.Format(Constants.Insert, tableName,columns,values);
                return await connection.ExecuteAsync(query, entity);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public async Task<int> UpdateByIdAsync<T>(string tableName, T entity)
        {
            try
            {
                PropertyInfo[] Properties = typeof(T).GetProperties();
                string UpdatingData = string.Join(',', Properties.Where(p=>p.Name != "Id").Select(p => $"{p.Name} = @{p.Name}"));
                string query = string.Format(Constants.UpdateById, tableName, UpdatingData);
                return await connection.ExecuteAsync(query, entity);
            }
            catch(Exception ex) { return 0; }
        }


        public async Task<int> DeleteByIdAsync(string tableName, string id)
        {
            try
            {
                string query = string.Format(Constants.DeleteById, tableName);
                return await connection.ExecuteAsync(query,new {Id=id});
            }
            catch (Exception ex) { return 0; }
        }
    }
}
