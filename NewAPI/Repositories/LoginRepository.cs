using MySql.Data.MySqlClient;
using NewAPI.DataBase;
using NewAPI.Models;
using System.Data;

namespace NewAPI.Repositories
{
    public class LoginRepository
    {
        private readonly MySqlAWS _connectionString;

        string commandQuery = string.Empty;
        MySqlDataReader _mySqlDataReader = null;
        MySqlConnection _mySqlConnection = null;
        MySqlCommand _mySqlCommand = null;

        public LoginRepository()
        {
            MySqlAWS connectionString = new MySqlAWS();
            _connectionString = connectionString;
        }

        public List<User> Select(Login login)
        {
            List<User> users;
            User user;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, Name, Email, Password, ProfileImageBase64 FROM Users WHERE Email = '" + login.Email + "' and Password = '" + login.Password + "'";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                users = new List<User>();

                while (_mySqlDataReader.Read())
                {
                    user = new User();
                    user.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    user.Name = _mySqlDataReader["Name"].ToString();
                    user.Email = _mySqlDataReader["Email"].ToString();
                    user.Password = _mySqlDataReader["Password"].ToString();
                    user.ProfileImageBase64 = _mySqlDataReader["ProfileImageBase64"].ToString();

                    users.Add(user);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return users;
        }
    }
}
