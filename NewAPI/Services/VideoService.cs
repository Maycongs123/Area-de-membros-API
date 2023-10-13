using NewAPI.Models;
using NewAPI.Repositories;

namespace NewAPI.Services
{
    public class VideoService
    {
        public List<Video> Select()
        {
            VideoRepository _videoRepository;
            List<Video> _videos;

            try
            {
                _videos = new List<Video>();

                _videoRepository = new VideoRepository();

                _videos = _videoRepository.Select(null);

                return _videos;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public Video SelectByID(Guid id)
        {
            VideoRepository _videoRepository;
            List<Video> _videos;
            Video _video;

            try
            {
                _videoRepository = new VideoRepository();

                _videos = _videoRepository.Select(id);
                _video = _videos.FirstOrDefault();

                return _video;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public List<Video> SelectByCourseModuleId(Guid courseModuleId)
        {
            VideoRepository _videoRepository;
            List<Video> _videos;

            try
            {
                _videoRepository = new VideoRepository();

                _videos = _videoRepository.SelectByCourseId(courseModuleId);

                return _videos;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public Video Insert(Guid userId, Video video)
        {
            VideoRepository _videoRepository;
            UserVideoProgressRepository _userVideoProgressRepository;
            Video _video;
            UserVideoProgress _userVideoProgress;

            try
            {
                _userVideoProgressRepository = new UserVideoProgressRepository();
                _videoRepository = new VideoRepository();

                _video = _videoRepository.Insert(video);


                if (_video != null)
                {
                    _userVideoProgress = new UserVideoProgress();
                    _userVideoProgress.UserId = userId;
                    _userVideoProgress.VideoId = video.Id;
                    _userVideoProgress.CourseModuleId = video.CourseModuleId;

                    _userVideoProgressRepository.Insert(_userVideoProgress);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _video;
        }

        public Video Update(Guid id, Video video)
        {
            VideoRepository _videoRepository;
            Video _video;

            try
            {
                _videoRepository = new VideoRepository();

                _video = _videoRepository.Update(id, video);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return video;
        }

        public bool Delete(Guid id)
        {
            VideoRepository _videoRepository;
            bool isDeleted = false;

            try
            {
                _videoRepository = new VideoRepository();

                isDeleted = _videoRepository.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
