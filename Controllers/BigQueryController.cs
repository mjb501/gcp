using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using gcp.Models;
using gcp_demo.BigQuery;

namespace gcp.Controllers
{
    public class BigQueryController : Controller
    {
        private readonly ILogger<BigQueryController> _logger;

        private readonly IBigQueryWrapper _bigQuery;

        public BigQueryController(ILogger<BigQueryController> logger, IBigQueryWrapper bigQuery)
        {
            _logger = logger;
            _bigQuery = bigQuery;
        }

        public IActionResult Index()
        {
            var table = _bigQuery.GetTableFullyQualifiedId("Requests","appengine_googleapis_com_nginx_request_*");
            
            string sql = $"SELECT httpRequest.requestUrl FROM `{table}`";

            var result = _bigQuery.Query(sql, null);
            
            var model = new BigQueryModel();
            
            foreach(var item in result)
            {
                var url = item.RawRow.F[0].V as string;
                if(url.Contains("Datastore"))
                {
                    model.DatastoreRequests++;
                }
                else if(url.Contains("Storage"))
                {
                    model.StorageRequests++;
                }
            }

            return View(model);
        }
    }
}