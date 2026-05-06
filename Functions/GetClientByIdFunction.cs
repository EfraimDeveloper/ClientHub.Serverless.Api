using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

public class GetClientByIdFunction
{
    private readonly ClientService _service;

    public GetClientByIdFunction(ClientService service)
    {
        _service = service;
    }

    [Function("GetClientById")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        int id = int.Parse(req.Query["id"]);

        var client = _service.GetById(id);

        var response = req.CreateResponse(
            client == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

        await response.WriteAsJsonAsync(client);

        return response;
    }
}