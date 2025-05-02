using IMS.Interfaces;
using System.Net.Http;

namespace IMS.Services
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly string _whatsappApiUrl = "https://api.whatsapp.com/send?phone={0}&text={1}";

        // Implement SendMessageAsync method from IWhatsAppService
        public async Task<bool> SendMessageAsync(string phoneNumber, string message)
        {
            var url = string.Format(_whatsappApiUrl, phoneNumber, message);
            try
            {
                // Make a GET request to the WhatsApp API (assuming the endpoint is available)
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
