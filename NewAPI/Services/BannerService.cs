using NewAPI.Controllers;
using NewAPI.Models;
using NewAPI.Repositories;
using System.Reflection;

namespace NewAPI.Services
{
    public class BannerService
    {
        public List<Banner> Select()
        {
            BannerRepository _bannerRepository;
            List<Banner> _banners;

            try
            {
                _banners = new List<Banner>();

                _bannerRepository = new BannerRepository();

                _banners = _bannerRepository.SelectAll(null);

                return _banners;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public Banner SelectByID(Guid id)
        {
            BannerRepository _bannerRepository;
            List<Banner> _banners;
            Banner _banner;

            try
            {
                _bannerRepository = new BannerRepository();
                _banner = new Banner();
                _banners = new List<Banner>();

                _banners = _bannerRepository.SelectAll(id);
                _banner = _banners.FirstOrDefault();

                return _banner;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }


        public Banner SelectByUserId(Guid userId)
        {
            BannerRepository _bannerRepository;
            Banner _banner;

            try
            {
                _bannerRepository = new BannerRepository();
                _banner = new Banner();

                _banner = _bannerRepository.SelectByUserId(userId);

                return _banner;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public Banner Insert(Banner banner)
        {
            BannerRepository _bannerRepository;
            Banner _banner;

            try
            {
                _bannerRepository = new BannerRepository();
                _banner = new Banner();

                _banner = _bannerRepository.Insert(banner);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _banner;
        }

        public Banner Update(Guid id, Banner banner)
        {
            BannerRepository _bannerRepository;
            Banner _banner;

            try
            {
                _bannerRepository = new BannerRepository();

                _banner = _bannerRepository.Update(id, banner);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _banner;
        }

        public Banner UpdateByUserId(Guid userId, Banner banner)
        {
            BannerRepository _bannerRepository;
            Banner _banner;
            Banner _bannerExistent;

            try
            {
                _bannerRepository = new BannerRepository();

                _bannerExistent = _bannerRepository.SelectByUserId(userId);

                if (_bannerExistent.Id == null)
                {
                    banner.UserId = userId;
                    _banner = _bannerRepository.Insert(banner);
                }
                else
                {
                    banner.Id = _bannerExistent.Id;
                    _banner = _bannerRepository.UpdateByUserId(userId, banner);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _banner;
        }

        public bool Delete(Guid id)
        {
            BannerRepository _bannerRepository;
            bool isDeleted = false;

            try
            {
                _bannerRepository = new BannerRepository();

                isDeleted = _bannerRepository.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
