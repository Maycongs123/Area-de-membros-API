using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NewAPI.Models;
using NewAPI.Services;

namespace NewAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;
        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public List<Comment> Get()
        {
            try
            {
                return _commentService.SelectAll();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }


        [HttpGet("{Id}")]
        public Comment GetById(Guid Id)
        {
            try
            {
                return _commentService.SelectById(Id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("GetByCourseModuleId/{Id}")]
        public List<Comment> GetByCourseModuleId(Guid Id)
        {
            try
            {
                return _commentService.SelectCommentsByCourseModule(Id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPost]
        public Comment Post(Comment comment)
        {
            Comment _comment;

            try
            {
                _comment = _commentService.Insert(comment);

                return _comment;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPut("{id}")]
        public Comment Put(Guid id, Comment comment)
        {
            Comment _comment;

            try
            {
                _comment = _commentService.Update(id, comment);

                return _comment;

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }


        [HttpDelete("id")]
        public bool Delete(Guid id)
        {
            bool _isDeleted;

            try
            {
                _isDeleted = _commentService.Delete(id);

                return _isDeleted;

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
    }
}
