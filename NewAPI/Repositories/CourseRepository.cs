using MySql.Data.MySqlClient;
using NewAPI.DataBase;
using NewAPI.Models;
using Org.BouncyCastle.Asn1;
using System.Data;

namespace NewAPI.Repositories
{
    public class CourseRepository
    {

        private readonly MySqlAWS _connectionString;

        string commandQuery = string.Empty;
        MySqlDataReader _mySqlDataReader = null;
        MySqlConnection _mySqlConnection = null;
        MySqlCommand _mySqlCommand = null;

        public CourseRepository()
        {
            MySqlAWS connectionString = new MySqlAWS();
            _connectionString = connectionString;
        }


        public List<Course> Select(Guid? id)
        {
            List<Course> courses;
            Course course;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, Title, Position from Courses WHERE 1=1";

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

                courses = new List<Course>();

                while (_mySqlDataReader.Read())
                {
                    course = new Course();
                    course.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    course.Title = _mySqlDataReader["Title"].ToString();
                    course.Position = Convert.ToInt32(_mySqlDataReader["Position"]);

                    courses.Add(course);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return courses;
        }

        public List<Course> SelectByUserId(Guid userId)
        {
            List<Course> courses;
            Course course;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT c.Id, c.Title, c.Position " +
                       "FROM Courses c " +
                       "INNER JOIN UsersCourses uc ON c.Id = uc.CourseId " +
                       "WHERE uc.UserId = @userId";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlCommand.Parameters.AddWithValue("@userId", userId);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                courses = new List<Course>();
                while (_mySqlDataReader.Read())
                {
                    course = new Course();
                    course.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    course.Title = _mySqlDataReader["Title"].ToString();
                    course.Position = Convert.ToInt32(_mySqlDataReader["Position"]);

                    courses.Add(course);
                }

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return courses;
        }

        public Course Insert(Course course)
        {
            Guid newId;
            try
            {
                newId = Guid.NewGuid();
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Insert into Courses (Id, Title, Position) " +
                                "Values(@Id, @Title, @Position)";

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", newId);
                _mySqlCommand.Parameters.AddWithValue("@Title", course.Title);
                _mySqlCommand.Parameters.AddWithValue("@Position", course.Position);

                _mySqlCommand.ExecuteNonQuery();

                course.Id = newId;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return course;
        }

        public Course Update(Guid id, Course course)
        {
            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Update Courses set Id = @Id, Title = @Title, Position = @Position where Id = @Id";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.Parameters.AddWithValue("@Id", id);
                _mySqlCommand.Parameters.AddWithValue("@Title", course.Title);
                _mySqlCommand.Parameters.AddWithValue("@Position", course.Position);

                _mySqlCommand.ExecuteNonQuery();

                course.Id = id;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return course;
        }


        public bool Delete(Guid id)
        {
            int rowsCommand;
            bool isDeleted;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Delete from Courses where Id = @Id";

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
