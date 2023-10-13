using MySql.Data.MySqlClient;
using NewAPI.DataBase;
using NewAPI.Models;
using System.Data;

namespace NewAPI.Repositories
{
    public class CommentRepository
    {
        private readonly MySqlAWS _connectionString;

        string commandQuery = string.Empty;
        MySqlDataReader _mySqlDataReader = null;
        MySqlConnection _mySqlConnection = null;
        MySqlCommand _mySqlCommand = null;

        public CommentRepository()
        {
            MySqlAWS connectionString = new MySqlAWS();
            _connectionString = connectionString;
        }

        public List<Comment> Select(Guid? id)
        {
            List<Comment> comments;
            Comment comment;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, CourseModuleId, UserId , DoubtFileNameAws , IsDoubt, Text, IsResponse, IdRootComment, CreatedAt from Comments WHERE 1=1";

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

                comments = new List<Comment>();
                while (_mySqlDataReader.Read())
                {
                    comment = new Comment();
                    comment.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    comment.CourseModuleId = new Guid(_mySqlDataReader["CourseModuleId"].ToString());
                    comment.UserId = new Guid(_mySqlDataReader["UserId"].ToString());
                    comment.DoubtFileNameAws = _mySqlDataReader["DoubtFileNameAws"].ToString();
                    comment.Text = _mySqlDataReader["Text"].ToString();
                    comment.IsDoubt = Convert.ToBoolean(_mySqlDataReader["IsDoubt"]);
                    comment.IsResponse = Convert.ToBoolean(_mySqlDataReader["IsResponse"]);
                    comment.CreatedAt = Convert.ToDateTime(_mySqlDataReader["CreatedAt"]);
                    object idRootComentObj = _mySqlDataReader["IdRootComment"];
                    comment.IdRootComment = idRootComentObj != DBNull.Value ? new Guid(idRootComentObj.ToString()) : (Guid?)null;

                    comments.Add(comment);
                }
                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            return comments;
        }

        public List<Comment> SelectCommentByCourseModule(Guid Id)
        {
            List<Comment> comments;
            Comment comment;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "SELECT Id, CourseModuleId, UserId , DoubtFileNameAws , IsDoubt, IsResponse, IdRootComment, CreatedAt, Text from Comments WHERE CourseModuleId = '" + Id + "' ";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);

                _mySqlConnection.Open();

                _mySqlDataReader = _mySqlCommand.ExecuteReader();

                comments = new List<Comment>();
                while (_mySqlDataReader.Read())
                {
                    comment = new Comment();
                    comment.Id = new Guid(_mySqlDataReader["Id"].ToString());
                    comment.CourseModuleId = new Guid(_mySqlDataReader["CourseModuleId"].ToString());
                    comment.UserId = new Guid(_mySqlDataReader["UserId"].ToString());
                    comment.DoubtFileNameAws = _mySqlDataReader["DoubtFileNameAws"].ToString();
                    comment.Text = _mySqlDataReader["Text"].ToString();
                    comment.IsDoubt = (bool)_mySqlDataReader["IsDoubt"];
                    comment.IsResponse = (bool)_mySqlDataReader["IsResponse"];
                    comment.CreatedAt = Convert.ToDateTime(_mySqlDataReader["CreatedAt"]);
                    object idRootComentObj = _mySqlDataReader["IdRootComment"];
                    comment.IdRootComment = idRootComentObj != DBNull.Value ? new Guid(idRootComentObj.ToString()) : (Guid?)null;

                    comments.Add(comment);
                }
                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            return comments;
        }

        public Comment Insert(Comment comment)
        {
            Guid newId;
            try
            {
                newId = Guid.NewGuid();
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Insert into Comments (Id, CourseModuleId, UserId , DoubtFileNameAws , IsDoubt, IsResponse, IdRootComment, CreatedAt, Text)" +
                    "Values(@Id, @CourseModuleId, @UserId, @DoubtFileNameAws, @IsDoubt, @IsResponse, @IdRootComment, @CreatedAt, @Text)";

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();


                var date = DateTime.UtcNow;

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", newId);
                _mySqlCommand.Parameters.AddWithValue("@CourseModuleId", comment.CourseModuleId);
                _mySqlCommand.Parameters.AddWithValue("@UserId", comment.UserId);
                _mySqlCommand.Parameters.AddWithValue("@DoubtFileNameAws", comment.DoubtFileNameAws);
                _mySqlCommand.Parameters.AddWithValue("@IsDoubt", comment.IsDoubt);
                _mySqlCommand.Parameters.AddWithValue("@IsResponse", comment.IsResponse);
                _mySqlCommand.Parameters.AddWithValue("@IdRootComment", comment.IdRootComment);
                _mySqlCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                _mySqlCommand.Parameters.AddWithValue("@Text", comment.Text);

                _mySqlCommand.ExecuteNonQuery();

                comment.Id = newId;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return comment;
        }

        public Comment Update(Guid id, Comment comment)
        {
            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Update Comments set Id = @Id, CourseModuleId = @CourseModuleId, UserId = @UserId, DoubtFileNameAws = @DoubtFileNameAws, IsDoubt = @IsDoubt, " +
                    "Text, = @Text, IsResponse = @IsResponse, IdRootComment = @IdRootComment, CreatedAt = @CreatedAt " +
                    "where Id = @Id";

                _mySqlCommand.CommandText = commandQuery;
                _mySqlCommand.CommandType = CommandType.Text;

                _mySqlConnection = new MySqlConnection(_connectionString.ConnectionString);
                _mySqlConnection.Open();

                _mySqlCommand = new MySqlCommand(commandQuery, _mySqlConnection);
                _mySqlCommand.CommandType = CommandType.Text;
                _mySqlCommand.Parameters.AddWithValue("@Id", id);
                _mySqlCommand.Parameters.AddWithValue("@CourseModuleId", comment.CourseModuleId);
                _mySqlCommand.Parameters.AddWithValue("@UserId", comment.UserId);
                _mySqlCommand.Parameters.AddWithValue("@DoubtFileNameAws", comment.DoubtFileNameAws);
                _mySqlCommand.Parameters.AddWithValue("@Text", comment.Text);
                _mySqlCommand.Parameters.AddWithValue("@IsDoubt", comment.IsDoubt);
                _mySqlCommand.Parameters.AddWithValue("@IsResponse", comment.IsResponse);
                _mySqlCommand.Parameters.AddWithValue("@IdRootComment", comment.IdRootComment);
                _mySqlCommand.Parameters.AddWithValue("@CreatedAt", comment.CreatedAt);

                _mySqlCommand.ExecuteNonQuery();

                comment.Id = id;

                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return comment;
        }

        public bool Delete(Guid id)
        {
            int rowsCommand;
            bool isDeleted = true;

            try
            {
                _mySqlCommand = new MySqlCommand();

                commandQuery = "Delete from Comments where Id = @Id";

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

