using Microsoft.AspNetCore.Mvc;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevopsIntegration.Controllers;

public class TicketsController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromServices] VssConnection vssConnection)
    {
        var wids = new[] { 740, 742 };
        var client = vssConnection.GetClient<WorkItemTrackingHttpClient>();

        var workItems = await client.GetWorkItemsAsync(wids);
        return Ok(workItems);
    }
}