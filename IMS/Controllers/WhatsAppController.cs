using IMS.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IMS.Models;

namespace IMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsAppController : ControllerBase
    {
        private readonly IWhatsAppService _whatsAppService;

        public WhatsAppController(IWhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }

        // POST: api/WhatsApp/SendMessage
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] WhatsApp request)
        {
            if (request == null || string.IsNullOrEmpty(request.PhoneNumber) || string.IsNullOrEmpty(request.Message))
            {
                return BadRequest(new { success = false, message = "Phone number and message are required." });
            }

            try
            {
                var result = await _whatsAppService.SendMessageAsync(request.PhoneNumber, request.Message);
                if (result)
                {
                    return Ok(new { success = true, message = "Message sent successfully." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to send message." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }
    }
  
}
