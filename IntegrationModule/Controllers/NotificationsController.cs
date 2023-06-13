using IntegrationModule.BLModels;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

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
        public ActionResult<IEnumerable<NotificationResponse>> GetAll()
        {
            try
            {
                var allNotifications =
                    _dbContext.Notifications.Select(dbNotification =>
                    new NotificationResponse
                    {
                        Id = dbNotification.Id,
                        ReceiverEmail = dbNotification.ReceiverEmail,
                        Subject = dbNotification.Subject,
                        Body = dbNotification.Body,
                        SentAt = dbNotification.SentAt
                    });
                return Ok(allNotifications);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<NotificationResponse> Get(int id)
        {
            try
            {
                var dbNotification = _dbContext.Notifications.FirstOrDefault(x => x.Id == id);
                if (dbNotification == null)
                    return NotFound();

                return Ok(new NotificationResponse
                {
                    Id = dbNotification.Id,
                    ReceiverEmail = dbNotification.ReceiverEmail,
                    Subject = dbNotification.Subject,
                    Body = dbNotification.Body,
                    SentAt = dbNotification.SentAt
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // retrieve unsent notifications count
        [HttpGet("[action]")]
        public ActionResult<int> GetUnsentCount()
        {
            try
            {
                var unsentCount = _dbContext.Notifications.Count(x => x.SentAt == null);
                return Ok(unsentCount);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost()]
        public ActionResult<NotificationResponse> Create(NotificationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var dbNotification = new Notification
                {
                    CreatedAt = DateTime.UtcNow,
                    ReceiverEmail = request.ReceiverEmail,
                    Subject = request.Subject,
                    Body = request.Body
                };

                _dbContext.Notifications.Add(dbNotification);

                _dbContext.SaveChanges();

                return Ok(new NotificationResponse
                {
                    Id = dbNotification.Id,
                    ReceiverEmail = dbNotification.ReceiverEmail,
                    Subject = dbNotification.Subject,
                    Body = dbNotification.Body
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("[action]/{id}")]
        public ActionResult<NotificationResponse> Modify(int id, [FromBody] NotificationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var dbNotification = _dbContext.Notifications.FirstOrDefault(x => x.Id == id);
                if (dbNotification == null)
                    return NotFound();

                dbNotification.ReceiverEmail = request.ReceiverEmail;
                dbNotification.Subject = request.Subject;
                dbNotification.Body = request.Body;
                dbNotification.SentAt = DateTime.UtcNow;

                _dbContext.SaveChanges();

                return Ok(new NotificationResponse
                {
                    Id = dbNotification.Id,
                    ReceiverEmail = dbNotification.ReceiverEmail,
                    Subject = dbNotification.Subject,
                    Body = dbNotification.Body
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("[action]/{id}")]
        public ActionResult<NotificationResponse> Remove(int id)
        {
            try
            {
                var dbNotification = _dbContext.Notifications.FirstOrDefault(x => x.Id == id);
                if (dbNotification == null)
                    return NotFound();

                _dbContext.Notifications.Remove(dbNotification);

                _dbContext.SaveChanges();

                return Ok(new NotificationResponse
                {
                    Id = dbNotification.Id,
                    ReceiverEmail = dbNotification.ReceiverEmail,
                    Subject = dbNotification.Subject,
                    Body = dbNotification.Body
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("[action]")]
        public ActionResult SendAllNotifications()
        {
            var client = new SmtpClient("127.0.0.1", 25);
            var sender = "admin@video.com";

            try
            {
                var unsentNotifications =
                    _dbContext.Notifications.Where(
                        x => !x.SentAt.HasValue);

                foreach (var notification in unsentNotifications)
                {
                    try
                    {
                        var mail = new MailMessage(
                            from: new MailAddress(sender),
                            to: new MailAddress(notification.ReceiverEmail));

                        mail.Subject = notification.Subject;
                        mail.Body = notification.Body;

                        client.Send(mail);

                        notification.SentAt = DateTime.UtcNow;
                        _dbContext.SaveChanges();

                    }
                    catch (Exception)
                    {
                        // Black hole for notification is bad handling :(
                    }
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
 