
namespace IMS.Interfaces
{
    
        public interface IWhatsAppService
        {
            // Method to send a WhatsApp message asynchronously
            Task<bool> SendMessageAsync(string phoneNumber, string message);
        }
    }


