using Microsoft.AspNetCore.Mvc;

namespace tetris_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VerificationController : ControllerBase
    {
        private readonly VerificationService _verificationService;

        public VerificationController(VerificationService verificationService) =>
            _verificationService = verificationService;


        [HttpGet]
        public async Task<List<Verification>> Get() =>
            await _verificationService.GetAsync();


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Verification>> Get(string id)
        {
            var verification = await _verificationService.GetAsync(id);

            if (verification is null)
            {
                return NotFound();
            }

            return verification;
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<Verification>> GetBasedOnEmail(string email)
        {
            var verification = await _verificationService.GetBasedOnEmailAsync(email);

            if (verification is null)
            {
                return null;
            }

            return verification;
        }


        [HttpPost]
        public async Task<IActionResult> Post(Verification newVerification)
        {
            //newVerification.Code = "123456";

            await _verificationService.CreateAsync(newVerification);

            return CreatedAtAction(nameof(Get), new { id = newVerification.Id }, newVerification);
        }


        [HttpPatch("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Verification updatedVerification)
        {
            var verification = await _verificationService.GetAsync(id);

            if (verification is null)
            {
                return NotFound();
            }

            updatedVerification.Id = verification.Id;

            await _verificationService.UpdateAsync(id, updatedVerification);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var verification = await _verificationService.GetAsync(id);

            if (verification is null)
            {
                return NotFound();
            }

            await _verificationService.RemoveAsync(id);

            return NoContent();
        }

    }
}
