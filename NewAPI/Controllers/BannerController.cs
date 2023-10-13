using Microsoft.AspNetCore.Mvc;
using NewAPI.Models;
using NewAPI.Services;

namespace NewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : Controller
    {
        [HttpGet]
        public List<Banner> Get()
        {
            BannerService _bannerService;

            try
            {
                _bannerService = new BannerService();

                return _bannerService.Select();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("{id}")]
        public Banner GetById(Guid id)
        {
            BannerService _bannerService;
            Banner banner;

            try
            {
                _bannerService = new BannerService();

                banner = _bannerService.SelectByID(id);

                return banner;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }


        [HttpGet("GetByUserId/{userId}")]
        public Banner GetByUserId(Guid userId)
        {
            BannerService _bannerService;
            Banner banner;

            try
            {
                _bannerService = new BannerService();

                banner = _bannerService.SelectByUserId(userId);

                return banner;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPost]
        public Banner Post(Banner banner)
        {
            BannerService _bannerService;
            Banner _banner;

            try
            {
                _bannerService = new BannerService();

                _banner = _bannerService.Insert(banner);

                return _banner;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPut("{id}")]
        public Banner Put(Guid id, [FromBody] Banner banner)
        {
            BannerService _bannerService;
            Banner _banner;

            try
            {
                _bannerService = new BannerService();

                _banner = _bannerService.Update(id, banner);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _banner;
        }

        [HttpPut("UpdateByUserId/{userId}")]
        public Banner PutByUserId(Guid userId, [FromBody] Banner banner)
        {
            BannerService _bannerService;
            Banner _banner;

            try
            {
                _bannerService = new BannerService();

                _banner = _bannerService.UpdateByUserId(userId, banner);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return _banner;
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            BannerService _bannerService;
            bool isDeleted = false;

            try
            {
                _bannerService = new BannerService();

                isDeleted = _bannerService.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return isDeleted;
        }
    }
}
