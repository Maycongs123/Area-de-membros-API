using NewAPI.Models;
using NewAPI.Repositories;

namespace NewAPI.Services
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;
        public NotificationService(NotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public List<Notification> SelectAll()
        {
            List<Notification> _Comentarios;

            try
            {
                _Comentarios = new List<Notification>();

                _Comentarios = _notificationRepository.Select(null);

                return _Comentarios;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public Notification SelectById(Guid id)
        {
            List<Notification> _comments;
            Notification _comment;

            try
            {
                _comments = new List<Notification>();

                _comments = _notificationRepository.Select(id);
                _comment = _comments.FirstOrDefault();


                return _comment;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public List<Notification> SelectNotificationsByUserId(Guid userId)
        {
            List<Notification> _Comentarios;

            try
            {
                _Comentarios = new List<Notification>();

                _Comentarios = _notificationRepository.SelectNotificationsByUserId(userId);

                return _Comentarios;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public Notification Insert(Notification notification)
        {
            Notification _notification;

            try
            {
                _notification = _notificationRepository.Insert(notification);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _notification;
        }

        public Notification Update(Guid id, Notification notification)
        {
            NotificationRepository _notificationRepository;
            Notification _notification;

            try
            {
                _notificationRepository = new NotificationRepository();

                _notification = _notificationRepository.Update(id, notification);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return notification;
        }

        public bool Delete(Guid id)
        {
            bool isDeleted;

            try
            {
                isDeleted = _notificationRepository.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
