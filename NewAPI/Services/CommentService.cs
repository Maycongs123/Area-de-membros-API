using NewAPI.Models;
using NewAPI.Repositories;

namespace NewAPI.Services
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepository;
        public CommentService(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public List<Comment> SelectAll()
        {
            List<Comment> _comments;

            try
            {
                _comments = new List<Comment>();

                _comments = _commentRepository.Select(null);

                return _comments;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public Comment SelectById(Guid id)
        {
            List<Comment> _comments;
            Comment _comment;

            try
            {
                _comments = new List<Comment>();

                _comments = _commentRepository.Select(id);
                _comment = _comments.FirstOrDefault();


                return _comment;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public List<Comment> SelectCommentsByCourseModule(Guid id)
        {
            UserRepository userRepository;
            List<Comment> _comments;
            List<User> _users;
            User _user;

            try
            {
                userRepository = new UserRepository();
                _comments = new List<Comment>();
                _users = new List<User>();
                _user = new User();

                _comments = _commentRepository.SelectCommentByCourseModule(id);

                foreach (var comment in _comments)
                {
                    _users = userRepository.Select(comment.UserId);
                    _user = _users.FirstOrDefault();
                    comment.UserName = _user.Name;
                    comment.ProfileImageBase64 = _user.ProfileImageBase64;
                }

                    return _comments;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public Comment Insert(Comment comment)
        {
            Comment _comment;

            try
            {
                _comment = _commentRepository.Insert(comment);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _comment;
        }

        public Comment Update(Guid id, Comment comment)
        {
            Comment _comment;

            try
            {
                _comment = _commentRepository.Update(id, comment);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _comment;
        }

        public bool Delete(Guid id)
        {
            bool isDeleted;

            try
            {
                isDeleted = _commentRepository.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
