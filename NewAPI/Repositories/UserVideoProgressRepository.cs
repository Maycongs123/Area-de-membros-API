using MySql.Data.MySqlClient;
using NewAPI.DataBase;
using NewAPI.Models;
using System.Data;

namespace NewAPI.Repositories
{
    public class UserVideoProgressRepository
    {
        private readonly MySqlAWS _connectionString;

        string commandQuery = string.Empty;
        MySqlDataReader _mySqlDataReader = null;
        MySqlConnection _mySqlConnection = null;
        MySqlCommand _mySqlCommand = null;

        public UserVideoProgressRepository()
        {
            MySqlAWS connectionString = new MySqlAWS();
            _connectionString = connectionString;
        }

        public List<UserVideoProgress> Select(Guid? id, Guid? courseModuleId, Guid? userId)
        {
            List<UserVideoProgress> usersVideosProgress;
            UserVideoProgress userVideoProgress;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, VideoId, UserId, CourseModuleId, IsWatched from UsersVideosProgress WHERE 1=1";

                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    commandQuery += " AND Id = '" + id.ToString() + "'";
                }

                if (!string.IsNullOrEmpty(userId.ToString()))
                {
                    commandQuery += " AND UserId = '" + userId.ToString() + "'";
                }

                if (!string.IsNullOrEmpty(courseModuleId.ToString()))
                {
                    commandQuery += " AND CourseModuleId = '" + courseModuleId.ToString() + "'";
                }

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                usersVideosProgress = new List<UserVideoProgress>();

                while (_mySqlDataReader.Read())
                {
                    userVideoProgress = new UserVideoProgress();
                    userVideoProgress.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    userVideoProgress.VideoId = new Guid(_mySqlDataReader["VideoId"].ToString());
                    userVideoProgress.UserId = new Guid(_mySqlDataReader["UserId"].ToString());
                    userVideoProgress.CourseModuleId = new Guid(_mySqlDataReader["CourseModuleId"].ToString());
                    userVideoProgress.IsWatched = Convert.ToBoolean(_mySqlDataReader["IsWatched"].ToString());

                    usersVideosProgress.Add(userVideoProgress);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return usersVideosProgress;
        }

        public UserVideoProgress Insert(UserVideoProgress userVideoProgress)
        {
            Guid UserVideoProgressId;

            try
            {
                UserVideoProgressId = Guid.NewGuid();

                _mySqlCommand = new MySqlCommand();

                commandQuery = "Insert into UsersVideosProgress (Id, VideoId, UserId, CourseModuleId, IsWatched) " +
                    "Values(@Id, @VideoId, @UserId, @CourseModuleId, @isWatched)";

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", UserVideoProgressId);
                _mySqlCommand.Parameters.AddWithValue("@VideoId", userVideoProgress.VideoId);
                _mySqlCommand.Parameters.AddWithValue("@UserId", userVideoProgress.UserId);
                _mySqlCommand.Parameters.AddWithValue("@CourseModuleId", userVideoProgress.CourseModuleId);
                _mySqlCommand.Parameters.AddWithValue("@IsWatched", false);

                _mySqlCommand.ExecuteNonQuery();

                userVideoProgress.Id = UserVideoProgressId;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return userVideoProgress;
        }

        public UserVideoProgress Update(Guid id, UserVideoProgress userVideoProgress)
        {
            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Update UsersVideosProgress set IsWatched = @IsWatched where Id = @Id";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", id);
                _mySqlCommand.Parameters.AddWithValue("@IsWatched", userVideoProgress.IsWatched);


                _mySqlCommand.ExecuteNonQuery();

                var Update = _mySqlCommand.ExecuteScalar();

                userVideoProgress.Id = id;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return userVideoProgress;
        }


        public bool Delete(Guid id)
        {
            int rowsCommand;
            bool isDeleted;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Delete from UsersVideosProgress where Id = @Id";

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
