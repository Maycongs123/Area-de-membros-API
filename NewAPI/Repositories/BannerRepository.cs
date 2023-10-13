using MySql.Data.MySqlClient;
using NewAPI.DataBase;
using NewAPI.Models;
using System.Data;

namespace NewAPI.Repositories
{
    public class BannerRepository
    {
        private readonly MySqlAWS _connectionString;

        string commandQuery = string.Empty;
        MySqlDataReader _mySqlDataReader = null;
        MySqlConnection _mySqlConnection = null;
        MySqlCommand _mySqlCommand = null;

        public BannerRepository()
        {
            MySqlAWS connectionString = new MySqlAWS();
            _connectionString = connectionString;
        }

        public List<Banner> SelectAll(Guid? id)
        {
            List<Banner> banners;
            Banner banner;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, ImageBase64, UserId  from Banners WHERE 1=1";

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

                banners = new List<Banner>();

                while (_mySqlDataReader.Read())
                {
                    banner = new Banner();
                    banner.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    banner.ImageBase64 = _mySqlDataReader["ImageBase64"].ToString();
                    banner.UserId = new Guid(_mySqlDataReader["UserId"].ToString());

                    banners.Add(banner);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return banners;
        }

        public Banner SelectByUserId(Guid userId)
        {
            Banner banner;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, ImageBase64, UserId  from Banners WHERE 1=1";


                if (!string.IsNullOrEmpty(userId.ToString()))
                {
                    commandQuery = commandQuery + " and UserId = '" + userId.ToString() + "'";
                }

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                banner = new Banner();

                if (_mySqlDataReader.Read())
                {
                    banner = new Banner();
                    banner.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    banner.ImageBase64 = _mySqlDataReader["ImageBase64"].ToString();
                    banner.UserId = new Guid(_mySqlDataReader["UserId"].ToString());
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return banner;
        }


        public Banner Insert(Banner banner)
        {
            Guid newId;
            try
            {
                newId = Guid.NewGuid();

                _mySqlCommand = new MySqlCommand();

                commandQuery = "Insert into Banners (Id, ImageBase64, UserId) " +
                                "Values(@Id, @ImageBase64, @UserId)";

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlCommand.Parameters.AddWithValue("@Id", newId);
                _mySqlCommand.Parameters.AddWithValue("@ImageBase64", banner.ImageBase64);
                _mySqlCommand.Parameters.AddWithValue("@UserId", banner.UserId);

                _mySqlCommand.ExecuteNonQuery();

                banner.Id = newId;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return banner;
        }

        public Banner Update(Guid id, Banner banner)
        {
            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Update Banners set Id = @Id, ImageBase64 = @ImageBase64, UserId = @UserId where Id = @Id";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.Parameters.AddWithValue("@Id", id);
                _mySqlCommand.Parameters.AddWithValue("@ImageBase64", banner.ImageBase64);
                _mySqlCommand.Parameters.AddWithValue("@UserId", banner.UserId);

                _mySqlCommand.ExecuteNonQuery();

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return banner;
        }

        public Banner UpdateByUserId(Guid userId, Banner banner)
        {
            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Update Banners set ImageBase64 = @ImageBase64 where UserId = @UserId";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.Parameters.AddWithValue("@ImageBase64", banner.ImageBase64);
                _mySqlCommand.Parameters.AddWithValue("@UserId", userId);

                _mySqlCommand.ExecuteNonQuery();

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return banner;
        }


        public bool Delete(Guid id)
        {
            int rowsCommand;
            bool isDeleted = true;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Delete from Banners where Id = @Id";

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
