using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeamProjectStart.DTO;
using TeamProjectStart.Helpers;

namespace TeamProjectStart
{
    public class ApiData
    {
        public async Task<List<Deadline>> GetDeadlines()
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.GetAsync("http://pw.heck.today/api/deadlines");
                var responseString = await response.Content.ReadAsStringAsync();
                var deadlines = Deadline.ListFromJson(responseString);
                return deadlines;
            }
        }

        public async Task<List<DTO.Task>> GetTasksForToday()
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.GetAsync("http://pw.heck.today/api/tasks");
                var responseString = await response.Content.ReadAsStringAsync();
                var tasks = DTO.Task.ListFromJson(responseString);
                return tasks?.Where(t => t.WorkTime != null && t.WorkTime.Value.Day == DateTime.Now.Day).OrderByDescending(t => t.WorkTime).ToList();
            }
        }

        public async Task<ApiError> AddDeadline(string name, DateTime? finish)
        {
            using (var client = TokenClient.GetClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"name", name},
                    {"finish", finish.Value.ToString("yyyy-MM-dd'T'HH:mm:ss")},
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("http://pw.heck.today/api/deadlines", content);
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<ApiError> DeleteDeadline(int id)
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.DeleteAsync($"http://pw.heck.today/api/deadlines/{id}");
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<ApiError> EditDeadline(int id, string name, DateTime finish)
        {
            using (var client = TokenClient.GetClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"name", name},
                    {"finish", finish.ToString()}, // TO DO: with value
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PutAsync($"http://pw.heck.today/api/deadlines/{id}", content);
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<ApiError> AddTask(int id, string name, DateTime? workTime)
        {
            using (var client = TokenClient.GetClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"name", name},
                    {"worktime", workTime.Value.ToString("yyyy-MM-dd'T'HH:mm:ss")}
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync($"http://pw.heck.today/api/deadlines/{id}", content);
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<ApiError> SetDone(int id)
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.PostAsync($"http://pw.heck.today/api/tasks/{id}/done", null);
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<ApiError> SetUndone(int id)
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.PostAsync($"http://pw.heck.today/api/tasks/{id}/undone", null);
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<List<DTO.Task>> GetTasksForDeadline(int id)
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.GetAsync($"http://pw.heck.today/api/deadlines/{id}");
                var responseString = await response.Content.ReadAsStringAsync();
                var tasks = DTO.Task.ListFromJson(responseString);
                return tasks;
            }
        }

        public async Task<ApiError> DeleteTask(int id)
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.DeleteAsync($"http://pw.heck.today/api/tasks/{id}");
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }
    }
}