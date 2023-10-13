using MySql.Data.MySqlClient;
using NewAPI.DataBase;
using NewAPI.Models;
using System.Data;

namespace NewAPI.Repositories
{
    public class VideoRepository
    {
        private readonly MySqlAWS _connectionString;

        string commandQuery = string.Empty;
        MySqlDataReader _mySqlDataReader = null;
        MySqlConnection _mySqlConnection = null;
        MySqlCommand _mySqlCommand = null;

        public VideoRepository()
        {
            MySqlAWS connectionString = new MySqlAWS();
            _connectionString = connectionString;
        }

        public List<Video> Select(Guid? id)
        {
            List<Video> videos;
            Video video;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, Title, Description, Url, UrlFileAws, ComplementaryMaterial, ComplementaryMaterialFileNameAws, Position, CourseModuleId from Videos WHERE 1=1";

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

                videos = new List<Video>();

                while (_mySqlDataReader.Read())
                {
                    video = new Video();
                    video.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    video.Title = _mySqlDataReader["Title"].ToString();
                    video.Description = _mySqlDataReader["Description"].ToString();
                    video.UrlFileAws = _mySqlDataReader["UrlFileAws"].ToString();
                    video.Url = _mySqlDataReader["Url"].ToString();
                    video.ComplementaryMaterial = _mySqlDataReader["ComplementaryMaterial"].ToString();
                    video.ComplementaryMaterialFileNameAws = _mySqlDataReader["ComplementaryMaterialFileNameAws"].ToString();
                    video.Position = Convert.ToInt32(_mySqlDataReader["Position"].ToString());
                    video.CourseModuleId = new Guid(_mySqlDataReader["CourseModuleId"].ToString());

                    videos.Add(video);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return videos;
        }

        public List<Video> SelectByCourseId(Guid? courseModuleId)
        {
            List<Video> videos;
            Video video;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, Title, Description, Url, UrlFileAws, ComplementaryMaterial, ComplementaryMaterialFileNameAws, Position, CourseModuleId from Videos WHERE 1=1";

                if (!string.IsNullOrEmpty(courseModuleId.ToString()))
                {
                    commandQuery = commandQuery + " and CourseModuleId = '" + courseModuleId.ToString() + "'";
                }

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                videos = new List<Video>();

                while (_mySqlDataReader.Read())
                {
                    video = new Video();
                    video.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    video.Title = _mySqlDataReader["Title"].ToString();
                    video.Description = _mySqlDataReader["Description"].ToString();
                    video.UrlFileAws = _mySqlDataReader["UrlFileAws"].ToString();
                    video.Url = _mySqlDataReader["Url"].ToString();
                    video.ComplementaryMaterial = _mySqlDataReader["ComplementaryMaterial"].ToString();
                    video.ComplementaryMaterialFileNameAws = _mySqlDataReader["ComplementaryMaterialFileNameAws"].ToString();
                    video.Position = Convert.ToInt32(_mySqlDataReader["Position"].ToString());
                    video.CourseModuleId = new Guid(_mySqlDataReader["CourseModuleId"].ToString());

                    videos.Add(video);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return videos;
        }

        public Video Insert(Video video)
        {
            Guid videoId;

            try
            {
                videoId = Guid.NewGuid();

                _mySqlCommand = new MySqlCommand();

                commandQuery = "Insert into Videos (Id, Title, Description, Url, UrlFileAws, Position, CourseModuleId, ComplementaryMaterial, ComplementaryMaterialFileNameAws) " +
                    "Values(@Id, @Title, @Description, @Url, @UrlFileAws, @Position, @CourseModuleId, @ComplementaryMaterial, ComplementaryMaterialFileNameAws)";

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", videoId);
                _mySqlCommand.Parameters.AddWithValue("@Title", video.Title);
                _mySqlCommand.Parameters.AddWithValue("@Description", video.Description);
                _mySqlCommand.Parameters.AddWithValue("@Url", video.Url);
                _mySqlCommand.Parameters.AddWithValue("@ComplementaryMaterial", video.ComplementaryMaterial);
                _mySqlCommand.Parameters.AddWithValue("@ComplementaryMaterialFileNameAws", video.ComplementaryMaterialFileNameAws);
                _mySqlCommand.Parameters.AddWithValue("@UrlFileAws", video.UrlFileAws);
                _mySqlCommand.Parameters.AddWithValue("@Position", video.Position);
                _mySqlCommand.Parameters.AddWithValue("@CourseModuleId", video.CourseModuleId);

                _mySqlCommand.ExecuteNonQuery();

                video.Id = videoId;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return video;
        }

        public Video Update(Guid id, Video video)
        {
            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Update Videos set Title = @Title, Description = @Description, Description = @Description, UrlFileAws = @UrlFileAws, Url = @Url, Position = @Position, CourseModuleId = @CourseModuleId, ComplementaryMaterial = @ComplementaryMaterial, ComplementaryMaterialFileNameAws = @ComplementaryMaterialFileNameAws  where Id = @Id";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", id);
                _mySqlCommand.Parameters.AddWithValue("@Title", video.Title);
                _mySqlCommand.Parameters.AddWithValue("@Description", video.Description);
                _mySqlCommand.Parameters.AddWithValue("@Url", video.Url);
                _mySqlCommand.Parameters.AddWithValue("@ComplementaryMaterial", video.ComplementaryMaterial);
                _mySqlCommand.Parameters.AddWithValue("@ComplementaryMaterialFileNameAws", video.ComplementaryMaterialFileNameAws);
                _mySqlCommand.Parameters.AddWithValue("@UrlFileAws", video.UrlFileAws);
                _mySqlCommand.Parameters.AddWithValue("@Position", video.Position);
                _mySqlCommand.Parameters.AddWithValue("@CourseModuleId", video.CourseModuleId);

                _mySqlCommand.ExecuteNonQuery();

                var Update = _mySqlCommand.ExecuteScalar();

                video.Id = id;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return video;
        }


        public bool Delete(Guid id)
        {
            int rowsCommand;
            bool isDeleted;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Delete from Videos where Id = @Id";

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
