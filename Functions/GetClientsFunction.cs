using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

public class GetClientsFunction
{
    private readonly ClientService _service;

    public GetClientsFunction(ClientService service)
    {
        _service = service;
    }

    [Function("GetClients")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        var clients = _service.GetAll();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(clients);

        return response;
    }
}