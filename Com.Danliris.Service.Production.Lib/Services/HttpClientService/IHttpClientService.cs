using System.Net.Http;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> PutAsync(string url, HttpContent content);
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, string token, HttpContent content);
    }
}
