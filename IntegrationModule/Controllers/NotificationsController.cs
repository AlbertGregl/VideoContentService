using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly RwaMoviesContext _dbContext;
        public NotificationsController(RwaMoviesContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("[action]")]
        public ActionResult<bool> TestConnection()
        {
            return _dbContext.Database.CanConnect();
        }

        //// *** CRUD GetAll() method implementation ***
        //[HttpGet("[action]")]
        //public ActionResult<IEnumerable<NotificationResponse>> GetAll()
        //{
        //    try
        //    {
        //        var allNotifications =
        //            _dbContext.Notifications.Select(dbNotification =>
        //            new NotificationResponse
        //            {
        //                Id = dbNotification.Id,
        //                Guid = dbNotification.Guid,
        //                Receiver = dbNotification.Receiver,
        //                Subject = dbNotification.Subject,
        //                Body = dbNotification.Body
        //            });
        //        return Ok(allNotifications);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //// *** CRUD HTTP GET method implementation ***
        //[HttpGet("{id}")]
        //public ActionResult<NotificationResponse> Get(int id)
        //{
        //    try
        //    {
        //        var dbNotification = _dbContext.Notifications.FirstOrDefault(x => x.Id == id);
        //        if (dbNotification == null)
        //            return NotFound();

        //        return Ok(new NotificationResponse
        //        {
        //            Id = dbNotification.Id,
        //            Guid = dbNotification.Guid,
        //            Receiver = dbNotification.Receiver,
        //            Subject = dbNotification.Subject,
        //            Body = dbNotification.Body
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //// *** CRUD HTTP POST method implementation ***
        //[HttpPost()]
        //public ActionResult<NotificationResponse> Create(NotificationRequest request)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var dbNotification = new Notification
        //        {
        //            Receiver = request.Receiver,
        //            Subject = request.Subject,
        //            Body = request.Body
        //        };

        //        _dbContext.Notifications.Add(dbNotification);

        //        _dbContext.SaveChanges();

        //        return Ok(new NotificationResponse
        //        {
        //            Id = dbNotification.Id,
        //            Guid = dbNotification.Guid,
        //            Receiver = dbNotification.Receiver,
        //            Subject = dbNotification.Subject,
        //            Body = dbNotification.Body
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //// *** CRUD HTTP PUT method implementation ***
        //[HttpPut("{id}")]
        //public ActionResult<NotificationResponse> Modify(int id, [FromBody] NotificationRequest request)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var dbNotification = _dbContext.Notifications.FirstOrDefault(x => x.Id == id);
        //        if (dbNotification == null)
        //            return NotFound();

        //        dbNotification.Receiver = request.Receiver;
        //        dbNotification.Subject = request.Subject;
        //        dbNotification.Body = request.Body;
        //        dbNotification.UpdatedAt = DateTime.UtcNow;

        //        _dbContext.SaveChanges();

        //        return Ok(new NotificationResponse
        //        {
        //            Id = dbNotification.Id,
        //            Guid = dbNotification.Guid,
        //            Receiver = dbNotification.Receiver,
        //            Subject = dbNotification.Subject,
        //            Body = dbNotification.Body
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //// *** CRUD HTTP DELETE method implementation ***
        //[HttpDelete("{id}")]
        //public ActionResult<NotificationResponse> Remove(int id)
        //{
        //    try
        //    {
        //        var dbNotification = _dbContext.Notifications.FirstOrDefault(x => x.Id == id);
        //        if (dbNotification == null)
        //            return NotFound();

        //        _dbContext.Notifications.Remove(dbNotification);

        //        _dbContext.SaveChanges();

        //        return Ok(new NotificationResponse
        //        {
        //            Id = dbNotification.Id,
        //            Guid = dbNotification.Guid,
        //            Receiver = dbNotification.Receiver,
        //            Subject = dbNotification.Subject,
        //            Body = dbNotification.Body
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
    }
}
