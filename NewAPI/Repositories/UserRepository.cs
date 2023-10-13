using MySql.Data.MySqlClient;
using NewAPI.DataBase;
using NewAPI.Models;
using System.Data;

namespace NewAPI.Repositories
{
    public class UserRepository
    {
        private readonly MySqlAWS _connectionString;

        string commandQuery = string.Empty;
        MySqlDataReader _mySqlDataReader = null;
        MySqlConnection _mySqlConnection = null;
        MySqlCommand _mySqlCommand = null;

        public UserRepository()
        {
            MySqlAWS connectionString = new MySqlAWS();
            _connectionString = connectionString;
        }

        public List<User> Select(Guid? id)
        {
            List<User> users;
            User user;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, Name, Email, Password, ProfileImageBase64, isAdm " +
                    "from Users WHERE 1=1";


                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    commandQuery = commandQuery + " and Id = '" + id.ToString() + "'";
                }

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
                    user.IsAdm = Convert.ToBoolean(_mySqlDataReader["IsAdm"]);

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

        public User Insert(User user)
        {
            Guid userID;

            try
            {
                userID = Guid.NewGuid();

                _mySqlCommand = new MySqlCommand();

                commandQuery = "Insert into Users (Id, Name, Email, Password, ProfileImageBase64, isAdm) " +
                               "Values(@Id, @Name, @Email, @Password, @ProfileImageBase64, @IsAdm)";

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", userID);
                _mySqlCommand.Parameters.AddWithValue("@Name", user.Name);
                _mySqlCommand.Parameters.AddWithValue("@Email", user.Email);
                _mySqlCommand.Parameters.AddWithValue("@Password", user.Password);
                _mySqlCommand.Parameters.AddWithValue("@ProfileImageBase64", user.ProfileImageBase64);
                _mySqlCommand.Parameters.AddWithValue("@IsAdm", user.IsAdm);

                _mySqlCommand.ExecuteNonQuery();

                user.Id = userID;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return user;
        }

        public User Update(Guid id, User user)
        {
            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Update Users set Id = @Id, Name = @Name, Email = @Email, Password = @Password, ProfileImageBase64 = @ProfileImageBase64, " +
                    "isAdm = @isAdm WHERE Id = @Id";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.Parameters.AddWithValue("@Id", id);
                _mySqlCommand.Parameters.AddWithValue("@Name", user.Name);
                _mySqlCommand.Parameters.AddWithValue("@Email", user.Email);
                _mySqlCommand.Parameters.AddWithValue("@Password", user.Password);
                _mySqlCommand.Parameters.AddWithValue("@ProfileImageBase64", user.ProfileImageBase64);
                _mySqlCommand.Parameters.AddWithValue("@IsAdm", user.IsAdm);

                _mySqlCommand.ExecuteNonQuery();

                user.Id = id;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return user;
        }


        public bool Delete(Guid id)
        {
            int rowsCommand;
            bool isDeleted = true;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Delete from Users where Id = @Id";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", id);

                rowsCommand = _mySqlCommand.ExecuteNonQuery();

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();

                if (rowsCommand == 0)
                {
                    throw new Exception("Erro - nenhum registro foi deletado para o id informado: " + id);
                }
                else
                {
                    isDeleted = true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }

    }
}
