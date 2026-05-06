using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

public class DeleteClientFunction
{
    private readonly ClientService _service;

    public DeleteClientFunction(ClientService service)
    {
        _service = service;
    }

    [Function("DeleteClient")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete")] HttpRequestData req)
    {
        var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
        var idValue = query["id"];

        if (!int.TryParse(idValue, out int id))
        {
            var bad = req.CreateResponse(HttpStatusCode.BadRequest);
            await bad.WriteStringAsync("Invalid or missing id");
            return bad;
        }

        var success = _service.Delete(id);

        var response = req.CreateResponse(
            success ? HttpStatusCode.OK : HttpStatusCode.NotFound);

        await response.WriteStringAsync(
            success ? "Deleted successfully" : "Client not found");

        return response;
    }
}