using MySql.Data.MySqlClient;
using NewAPI.DataBase;
using NewAPI.Models;
using System.Data;
using System.Xml.Linq;

namespace NewAPI.Repositories
{
    public class NotificationRepository
    {
        private readonly MySqlAWS _connectionString;

        string commandQuery = string.Empty;
        MySqlDataReader _mySqlDataReader = null;
        MySqlConnection _mySqlConnection = null;
        MySqlCommand _mySqlCommand = null;

        public NotificationRepository()
        {
            MySqlAWS connectionString = new MySqlAWS();
            _connectionString = connectionString;
        }

        public List<Notification> Select(Guid? id)
        {
            List<Notification> notifications;
            Notification notification;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, Title, UserId , Description , IsViewed, CreatedAt from Notifications WHERE 1=1";


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

                notifications = new List<Notification>();
                while (_mySqlDataReader.Read())
                {
                    notification = new Notification();
                    notification.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    notification.Title = _mySqlDataReader["Title"].ToString();
                    notification.UserId = new Guid(_mySqlDataReader["UserId"].ToString());
                    notification.Description = _mySqlDataReader["Description"].ToString();
                    notification.IsViewed = Convert.ToBoolean(_mySqlDataReader["IsViewed"]);

                    notifications.Add(notification);
                }
                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            return notifications;
        }

        public List<Notification> SelectNotificationsByUserId(Guid userId)
        {
            List<Notification> notifications;
            Notification notification;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, Title, UserId , Description , IsViewed, CreatedAt from Notifications WHERE UserId = '" + userId + "' ";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                notifications = new List<Notification>();
                while (_mySqlDataReader.Read())
                {
                    notification = new Notification();
                    notification.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    notification.Title = _mySqlDataReader["Title"].ToString();
                    notification.UserId = new Guid(_mySqlDataReader["UserId"].ToString());
                    notification.Description = _mySqlDataReader["Description"].ToString();
                    notification.IsViewed = Convert.ToBoolean(_mySqlDataReader["IsViewed"]);
                    notification.CreatedAt = Convert.ToDateTime(_mySqlDataReader["CreatedAt"]);

                    notifications.Add(notification);
                }
                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            return notifications;
        }

        public Notification Insert(Notification notification)
        {
            Guid newId;
            try
            {
                newId = Guid.NewGuid();
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Insert into Notifications (Id, UserId, Title , Description , IsViewed, CreatedAt)" +
                    "Values(@Id, @UserId, @Title, @Description, @IsViewed, @CreatedAt)";

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();


                var date = DateTime.UtcNow;

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", newId);
                _mySqlCommand.Parameters.AddWithValue("@UserId", notification.UserId);
                _mySqlCommand.Parameters.AddWithValue("@Title", notification.Title);
                _mySqlCommand.Parameters.AddWithValue("@Description", notification.Description);
                _mySqlCommand.Parameters.AddWithValue("@IsViewed", notification.IsViewed);
                _mySqlCommand.Parameters.AddWithValue("@CreatedAt", notification.CreatedAt);

                _mySqlCommand.ExecuteNonQuery();

                notification.Id = newId;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return notification;
        }

        public Notification Update(Guid id, Notification notification)
        {
            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Update Notifications set Id = @Id, Title = @Title, UserId = @UserId, Description = @Description, IsViewed = @IsViewed, " +
                    "CreatedAt = @CreatedAt where Id = @Id";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", id);
                _mySqlCommand.Parameters.AddWithValue("@UserId", notification.UserId);
                _mySqlCommand.Parameters.AddWithValue("@Title", notification.Title);
                _mySqlCommand.Parameters.AddWithValue("@Description", notification.Description);
                _mySqlCommand.Parameters.AddWithValue("@IsViewed", notification.IsViewed);
                _mySqlCommand.Parameters.AddWithValue("@CreatedAt", notification.CreatedAt);

                _mySqlCommand.ExecuteNonQuery();

                notification.Id = id;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return notification;
        }

        public bool Delete(Guid id)
        {
            int rowsCommand;
            bool isDeleted = true;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Delete from Notifications where Id = @Id";

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
