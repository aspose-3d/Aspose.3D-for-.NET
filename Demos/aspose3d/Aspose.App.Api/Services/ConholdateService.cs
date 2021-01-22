using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace Aspose.App.Api.Services
{
    namespace Models
    { 
        public class User
        {
            [JsonPropertyName("username")]
            public string Name { get; set; }
            public string Email { get; set; }
        }
        public class Post
        {
            public string Raw { get; set; }
            public User User { get; set; }
        }
        class Topic
        { 
            public string Title { get; set; }
            [JsonPropertyName("category_id")]
            public int CategoryId { get; set; }
            public bool Notification { get; set; }
            public User User { get; set; }
            public Post[] Posts { get; set; }
            [JsonPropertyName("custom_fields")]
            public Dictionary<String, bool> CustomFields { get; set; }


            public Topic(bool privateAccess)
            {
                CustomFields = new Dictionary<string, bool>();
                CustomFields.Add("is_private", privateAccess);
            }
        }
        class TopicRequest
        { 
            public Topic Topic { get; set; }
        }
        class TopicResponse
        {
            public string Url { get; set; }
            public string Error { get; set; }
        }


    }

    public class ConholdateService
    {
        IHttpClientFactory httpClientFactory;
        string forumUrl;
        string forumKey;
        int categoryId;

        public ConholdateService(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            forumUrl = configuration["SystemConfig:ForumUrl"];
            forumKey = configuration["SystemConfig:ForumKey"];
            categoryId = int.Parse(configuration["SystemConfig:ForumId"]);
            if (string.IsNullOrEmpty(forumUrl))
                throw new InvalidOperationException("ForumUrl is not configured");
            if (string.IsNullOrEmpty(forumKey))
                throw new InvalidOperationException("ForumKey is not configured");
            this.httpClientFactory = httpClientFactory;

        }
        public async Task<String> PostToForum(string title, string email, string username, string content)
        {
            var user = new Models.User() { Name = username, Email = email };
            var topic = new Models.TopicRequest()
            {
                Topic = new Models.Topic(true)
                {
                    Title = title,
                    CategoryId = categoryId,
                    Notification = !string.IsNullOrEmpty(email),
                    User = user,
                    Posts = new Models.Post[]
                    {
                        new Models.Post()
                        {
                            Raw = content,
                            User = user
                        }
                    }
                }
            };
            JsonSerializerOptions opt = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var data = JsonSerializer.Serialize(topic, opt);
            var url = forumUrl + "/port/import.json";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            using var client = httpClientFactory.CreateClient("forum");
            client.DefaultRequestHeaders.Add("X-FORUM-API-Key", forumKey);
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException("Server side error");
            var str = await response.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<Models.TopicResponse>(str, opt);
            if (resp.Error != null)
                throw new InvalidOperationException(resp.Error);
            return resp.Url;
        }
    }
}
