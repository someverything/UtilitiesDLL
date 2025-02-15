using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UtilitiesDLL.Message.Abstract;

namespace UtilitiesDLL.Message.Concrete
{
    public class SmsManager : ISmsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SmsManager(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> SendOtpSmsAsync(string phoneNumber, string otp)
        {
            try
            {
                var apiUrl = _configuration["Sms: Uri"];
                var data = new
                {
                    Message = $"To confirm process write otp: {otp}",
                    Recievers = new[] { phoneNumber},
                    SendDate = DateTime.UtcNow.AddHours(4).ToString("yyyyMMdd HH:mm"),
                    ExpireDate = DateTime.UtcNow.AddHours(4).AddMinutes(3).ToString("yyyyMMdd HH:mm"),
                    Username = _configuration["SMS: Username"],
                    Password = _configuration["SMS: Password"],
                };

                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var httpClient = new HttpClient();
                var response = await httpClient.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Log.Information($"Sms send successfully to {phoneNumber}", phoneNumber);
                    return true;
                }
                else
                {
                    Log.Information($"Failed to send SMS to {phoneNumber}. StatusCode: {response.StatusCode}", phoneNumber, response.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while sending SMS to {phoneNumber}", phoneNumber);
                return false;
            }
        }
    }
}
