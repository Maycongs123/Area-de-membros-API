using MySql.Data.MySqlClient;
using NewAPI.DataBase;
using NewAPI.Models;
using System.Data;
using System.Reflection;

namespace NewAPI.Repositories
{
    public class CourseModuleRepository
    {
        private readonly MySqlAWS _connectionString;

        string commandQuery = string.Empty;
        MySqlDataReader _mySqlDataReader = null;
        MySqlConnection _mySqlConnection = null;
        MySqlCommand _mySqlCommand = null;

        public CourseModuleRepository()
        {
            MySqlAWS connectionString = new MySqlAWS();
            _connectionString = connectionString;
        }

        public List<CourseModule> Select(Guid? id)
        {
            List<CourseModule> courseModules;
            CourseModule courseModule;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, Title, Description, CoverImage, Position, IsEnable, CourseId  " +
                    "from CourseModules WHERE 1=1";


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

                courseModules = new List<CourseModule>();

                while (_mySqlDataReader.Read())
                {
                    courseModule = new CourseModule();
                    courseModule.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    courseModule.Title = _mySqlDataReader["Title"].ToString();
                    courseModule.Description = _mySqlDataReader["Description"].ToString();
                    courseModule.CoverImage = _mySqlDataReader["CoverImage"].ToString();
                    courseModule.Position = Convert.ToInt32(_mySqlDataReader["Position"]);
                    courseModule.IsEnable = (bool)_mySqlDataReader["IsEnable"];
                    courseModule.CourseId = new Guid(_mySqlDataReader["CourseId"].ToString());

                    courseModules.Add(courseModule);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return courseModules;
        }

        public List<CourseModule> SelectByCourseId(Guid? courseId)
        {
            List<CourseModule> courseModules;
            CourseModule courseModule;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, Title, Description, CoverImage, Position, IsEnable, CourseId " +
                    "from CourseModules WHERE 1=1";


                if (!string.IsNullOrEmpty(courseId.ToString()))
                {
                    commandQuery = commandQuery + " and CourseId = '" + courseId.ToString() + "'";
                }

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                courseModules = new List<CourseModule>();

                while (_mySqlDataReader.Read())
                {
                    courseModule = new CourseModule();
                    courseModule.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    courseModule.Title = _mySqlDataReader["Title"].ToString();
                    courseModule.Description = _mySqlDataReader["Description"].ToString();
                    courseModule.CoverImage = _mySqlDataReader["CoverImage"].ToString();
                    courseModule.Position = Convert.ToInt32(_mySqlDataReader["Position"]);
                    courseModule.IsEnable = (bool)_mySqlDataReader["IsEnable"];
                    courseModule.CourseId = new Guid(_mySqlDataReader["CourseId"].ToString());

                    courseModules.Add(courseModule);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return courseModules;
        }

        public CourseModule Insert(CourseModule courseModule)
        {
            Guid courseModuleID;

            try
            {
                courseModuleID = Guid.NewGuid();

                _mySqlCommand = new MySqlCommand();

                commandQuery = "Insert into CourseModules (Id, Title, Description, CoverImage, Position, IsEnable, CourseId) " +
                               "Values(@Id, @Title, @Description, @CoverImage, @Position, @IsEnable, @CourseId)";

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", courseModuleID);
                _mySqlCommand.Parameters.AddWithValue("@Title", courseModule.Title);
                _mySqlCommand.Parameters.AddWithValue("@Description", courseModule.Description);
                _mySqlCommand.Parameters.AddWithValue("@CoverImage", courseModule.CoverImage);
                _mySqlCommand.Parameters.AddWithValue("@Position", courseModule.Position);
                _mySqlCommand.Parameters.AddWithValue("@IsEnable", courseModule.IsEnable);
                _mySqlCommand.Parameters.AddWithValue("@CourseId", courseModule.CourseId);

                _mySqlCommand.ExecuteNonQuery();

                courseModule.Id = courseModuleID;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return courseModule;
        }

        public CourseModule Update(Guid id, CourseModule courseModule)
        {
            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Update CourseModules set Id = @Id, Title = @Title, Description = @Description, CoverImage = @CoverImage, Position = @Position, IsEnable = @IsEnable  " +
                    "where Id = @Id";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.Parameters.AddWithValue("@Id", id);
                _mySqlCommand.Parameters.AddWithValue("@Title", courseModule.Title);
                _mySqlCommand.Parameters.AddWithValue("@Description", courseModule.Description);
                _mySqlCommand.Parameters.AddWithValue("@CoverImage", courseModule.CoverImage);
                _mySqlCommand.Parameters.AddWithValue("@Position", courseModule.Position);
                _mySqlCommand.Parameters.AddWithValue("@IsEnable", courseModule.IsEnable);
                _mySqlCommand.Parameters.AddWithValue("@CourseId", courseModule.CourseId);

                _mySqlCommand.ExecuteNonQuery();

                courseModule.Id = id;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return courseModule;
        }


        public bool Delete(Guid id)
        {
            int rowsCommand;
            bool isDeleted = true;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Delete from CourseModules where Id = @Id";

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
