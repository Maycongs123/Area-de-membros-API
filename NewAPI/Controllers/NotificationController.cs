using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NewAPI.Models;
using NewAPI.Services;

namespace NewAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly NotificationService _notificationService;
        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public List<Notification> Get()
        {
            try
            {
                return _notificationService.SelectAll();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }


        [HttpGet("{Id}")]
        public Notification GetById(Guid Id)
        {
            try
            {
                return _notificationService.SelectById(Id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("GetByUserId/{userId}")]
        public List<Notification> GetByNotificationId(Guid userId)
        {
            try
            {
                return _notificationService.SelectNotificationsByUserId(userId);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPost]
        public Notification Post(Notification notification)
        {
            Notification _notification;

            try
            {
                _notification = _notificationService.Insert(notification);

                return _notification;

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }


        [HttpPut("{id}")]
        public Notification Put(Guid id, [FromBody] Notification notification)
        {
            Notification _notification;

            try
            {

                _notification = _notificationService.Update(id, notification);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _notification;
        }

        [HttpDelete("{Id}")]
        public bool Delete(Guid Id)
        {
            bool isDeleted;

            try
            {
                isDeleted = _notificationService.Delete(Id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
