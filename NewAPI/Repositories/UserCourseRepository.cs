using MySql.Data.MySqlClient;
using NewAPI.DataBase;
using NewAPI.Models;
using System.Data;

namespace NewAPI.Repositories
{
    public class UserCourseCourseRepository
    {
        private readonly MySqlAWS _connectionString;

        string commandQuery = string.Empty;
        MySqlDataReader _mySqlDataReader = null;
        MySqlConnection _mySqlConnection = null;
        MySqlCommand _mySqlCommand = null;

        public UserCourseCourseRepository()
        {
            MySqlAWS connectionString = new MySqlAWS();
            _connectionString = connectionString;
        }

        public List<UserCourse> SelectAll()
        {
            List<UserCourse> userCourses;
            UserCourse userCourse;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, UserId, CourseId from UsersCourses WHERE 1=1";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                userCourses = new List<UserCourse>();

                while (_mySqlDataReader.Read())
                {
                    userCourse = new UserCourse();
                    userCourse.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    userCourse.UserId = new Guid(_mySqlDataReader["UserId"].ToString());
                    userCourse.CourseId = new Guid(_mySqlDataReader["CourseId"].ToString());

                    userCourses.Add(userCourse);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return userCourses;
        }


        public UserCourse Select(Guid id)
        {
            UserCourse userCourse;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, UserId, CourseId from UsersCourses WHERE 1=1";

                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    commandQuery = commandQuery + " and Id = " + id;
                }


                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                _mySqlDataReader.Read();

                userCourse = new UserCourse();
                userCourse.Id = new Guid(_mySqlDataReader["Id"].ToString());
                userCourse.UserId = new Guid(_mySqlDataReader["UserId"].ToString());
                userCourse.CourseId = new Guid(_mySqlDataReader["CourseId"].ToString());

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return userCourse;
        }

        public List<UserCourse> SelectByUserId(Guid userId)
        {
            List<UserCourse> userCourses;
            UserCourse userCourse;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, UserId, CourseId from UsersCourses WHERE 1=1";

                if (!string.IsNullOrEmpty(userId.ToString()))
                {
                    commandQuery = commandQuery + " and UserId = " + userId;
                }

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                userCourses = new List<UserCourse>();

                while (_mySqlDataReader.Read())
                {
                    userCourse = new UserCourse();
                    userCourse.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    userCourse.UserId = new Guid(_mySqlDataReader["UserId"].ToString());
                    userCourse.CourseId = new Guid(_mySqlDataReader["CourseId"].ToString());

                    userCourses.Add(userCourse);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return userCourses;
        }

        public List<UserCourse> SelectByCourseId(Guid courseId)
        {
            List<UserCourse> userCourses;
            UserCourse userCourse;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, UserId, CourseId from UsersCourses WHERE 1=1";

                if (!string.IsNullOrEmpty(courseId.ToString()))
                {
                    commandQuery = commandQuery + " and CourseId = " + courseId;
                }

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                userCourses = new List<UserCourse>();

                while (_mySqlDataReader.Read())
                {
                    userCourse = new UserCourse();
                    userCourse.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    userCourse.UserId = new Guid(_mySqlDataReader["UserId"].ToString());
                    userCourse.CourseId = new Guid(_mySqlDataReader["CourseId"].ToString());

                    userCourses.Add(userCourse);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return userCourses;
        }


        public UserCourse Insert(UserCourse userCourse)
        {
            Guid userCourseID;

            try
            {
                userCourseID = Guid.NewGuid();

                _mySqlCommand = new MySqlCommand();

                commandQuery = "Insert into UsersCourses (Id, UserId, CourseId) " +
                               "Values(@Id, @UserId, @CourseId)";

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", userCourseID);
                _mySqlCommand.Parameters.AddWithValue("@UserId", userCourse.UserId);
                _mySqlCommand.Parameters.AddWithValue("@CourseId", userCourse.CourseId);

                _mySqlCommand.ExecuteNonQuery();

                userCourse.Id = userCourseID;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return userCourse;
        }

        public UserCourse Update(Guid id, UserCourse userCourse)
        {
            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Update UserCourses set Id = @Id Name = @Name, Email = @Email";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.Parameters.AddWithValue("@Id", id);
                _mySqlCommand.Parameters.AddWithValue("@UserId", userCourse.UserId);
                _mySqlCommand.Parameters.AddWithValue("@CourseId", userCourse.CourseId);

                _mySqlCommand.ExecuteNonQuery();

                userCourse.Id = id;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return userCourse;
        }


        public bool Delete(Guid id)
        {
            int rowsCommand;
            bool isDeleted = true;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Delete from UserCourses where Id = @Id";

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
