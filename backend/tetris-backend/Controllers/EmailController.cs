using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tetris_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail(Email request)
        {
            _emailService.SendEmail(request);
            return Ok();
        }
    }
}
