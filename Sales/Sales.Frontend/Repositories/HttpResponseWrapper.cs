using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Sales.Frontend.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }

        public bool Error { get; set; }

        public T? Response { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }

        //public Dictionary<HttpStatusCode, string> StatusCodeMessage = new()
        //{
        //    {HttpStatusCode.NotFound,"Recurso no encontrado" },
        //    {HttpStatusCode.BadRequest,await HttpResponseMessage.Content.ReadAsStringAsync() },
        //    {HttpStatusCode.Unauthorized,"Tienes que logearte para hacer esta operación" },
        //    {HttpStatusCode.Forbidden,"No tienes permisos para hacer esta operación" }
        //};

        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }
            
            Dictionary<HttpStatusCode, string> statusCodeMessage = new Dictionary<HttpStatusCode, string>()
            {
                { HttpStatusCode.NotFound, "Recurso no encontrado" },
                { HttpStatusCode.BadRequest, await HttpResponseMessage.Content.ReadAsStringAsync() },
                { HttpStatusCode.Unauthorized, "Tienes que logearte para hacer esta operación" },
                { HttpStatusCode.Forbidden, "No tienes permisos para hacer esta operación" }
            };

            if (statusCodeMessage.ContainsKey(HttpResponseMessage.StatusCode))
            {
                return statusCodeMessage[HttpResponseMessage.StatusCode];
            }

            //var statusCode = HttpResponseMessage.StatusCode;
            //if (statusCode == HttpStatusCode.NotFound)
            //{
            //    return "Recurso no encontrado";
            //}
            //else if (statusCode == HttpStatusCode.BadRequest)
            //{
            //    return await HttpResponseMessage.Content.ReadAsStringAsync();
            //}
            //else if (statusCode == HttpStatusCode.Unauthorized)
            //{
            //    return "Tienes que logearte para hacer esta operación";
            //}
            //else if (statusCode == HttpStatusCode.Forbidden)
            //{
            //    return "No tienes permisos para hacer esta operación";
            //}

            return "Ha ocurrido un error inesperado";
        }
    }
}
