using ClientHub.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using System.Net;

public class UpdateClientFunction
{
    private readonly ClientService _service;

    public UpdateClientFunction(ClientService service)
    {
        _service = service;
    }

    [Function("UpdateClient")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var client = JsonConvert.DeserializeObject<Client>(body);

        var updated = _service.Update(client);

        var response = req.CreateResponse(
            updated == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

        await response.WriteAsJsonAsync(updated);

        return response;
    }
}