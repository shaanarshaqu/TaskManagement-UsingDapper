namespace TaskManagement.Common
{
    public class Constants
    {
        public const string ConnectionString = "Data Source=SHAAN;Database=TaskManagement;Integrated Security=True;TrustServerCertificate=True";

        public const string GetAll = "SELECT * FROM {0} ORDER BY CreatedAt";
        public const string GetById = "SELECT * FROM {0} WHERE Id=@Id";
        public const string Insert = "INSERT INTO {0}({1}) VALUES({2})";
        public const string UpdateById = "UPDATE {0} SET {1} WHERE Id=@Id";
        public const string DeleteById = "DELETE FROM {0} WHERE Id=@Id";
        public const string GetAllByCondition = "SELECT * FROM {0} WHERE {1}";


        public const string secret = "shbfsinsdv78shfns8?sdhjshdnvskjdnvssdvsdv";


        public enum Tables
        {
            Tasks,
            Users
        }
    }
}
