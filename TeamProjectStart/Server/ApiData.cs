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
        const string ServerUrl = "http://pw.heck.today/api";

        public async Task<List<Deadline>> GetDeadlines()
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.GetAsync($"{ServerUrl}/deadlines");
                var responseString = await response.Content.ReadAsStringAsync();
                var deadlines = Deadline.ListFromJson(responseString);
                return deadlines;
            }
        }

        public async Task<List<DTO.Task>> GetTasksForToday()
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.GetAsync($"{ServerUrl}/tasks");
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
                var response = await client.PostAsync($"{ServerUrl}/deadlines", content);
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<ApiError> DeleteDeadline(int id)
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.DeleteAsync($"{ServerUrl}/deadlines/{id}");
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<ApiError> EditDeadline(int id, string name, DateTime? finish)
        {
            using (var client = TokenClient.GetClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"name", name},
                    {"finish", finish.Value.ToString("yyyy-MM-dd'T'HH:mm:ss")},
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PutAsync($"{ServerUrl}/deadlines/{id}", content);
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
                var response = await client.PostAsync($"{ServerUrl}/deadlines/{id}", content);
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<ApiError> SetDone(int id)
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.PostAsync($"{ServerUrl}/tasks/{id}/done", null);
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<ApiError> SetUndone(int id)
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.PostAsync($"{ServerUrl}/tasks/{id}/undone", null);
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }

        public async Task<List<DTO.Task>> GetTasksForDeadline(int id)
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.GetAsync($"{ServerUrl}/deadlines/{id}");
                var responseString = await response.Content.ReadAsStringAsync();
                var tasks = DTO.Task.ListFromJson(responseString);
                return tasks;
            }
        }

        public async Task<ApiError> DeleteTask(int id)
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.DeleteAsync($"{ServerUrl}/tasks/{id}");
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }
    }
}