using EmployeeUI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EmployeeUI.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly HttpClient _httpClient;

        public AttendanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<bool> MarkAttendanceAsync(Attendance attendance)
        //{
        //    var response = await _httpClient.PostAsJsonAsync("", attendance);
        //    return response.IsSuccessStatusCode;
        //}

        //public async Task<bool> MarkAttendanceAsync(Attendance entry)
        //{
        //    Console.WriteLine("Sending Attendance Payload:");
        //    Console.WriteLine($"EmployeeId: {entry.EmployeeId}");
        //    Console.WriteLine($"Status: {entry.Status}");
        //    Console.WriteLine($"Date: {entry.Date}");

        //    var response = await _httpClient.PostAsJsonAsync("", entry);

        //    Console.WriteLine($"Response Status: {response.StatusCode}");

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var errorMessage = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine("API Error Response: " + errorMessage);
        //    }

        //    return response.IsSuccessStatusCode;
        //}

        public async Task<bool> MarkAttendanceAsync(AttendanceEntry entry)
        {
            var payload = new Attendance
            {
                EmployeeId = entry.SelectedEmployeeId,
                Status = entry.Status,
                Date = entry.Date
            };

            var response = await _httpClient.PostAsJsonAsync("", payload);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Attendance>> GetFilteredAsync(AttendanceFilter filter)
        {
            string url = $"employee/{filter.EmployeeId}?from={filter.From:yyyy-MM-dd}&to={filter.To:yyyy-MM-dd}";
            var result = await _httpClient.GetFromJsonAsync<List<Attendance>>(url);
            return result ?? new List<Attendance>();
        }
    }
}
