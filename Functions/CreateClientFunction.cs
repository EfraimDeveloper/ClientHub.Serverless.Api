using ClientHub.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using System.Net;

public class CreateClientFunction
{
    private readonly ClientService _service;

    public CreateClientFunction(ClientService service)
    {
        _service = service;
    }

    [Function("CreateClient")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var client = JsonConvert.DeserializeObject<Client>(body);

        var result = _service.Create(client);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);

        return response;
    }
}