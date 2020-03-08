
using System;
using System.Linq;
using gcp.Models;
using gcp_demo.Datastore;
using Google.Cloud.Datastore.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gcp.Controllers
{
    public class DatastoreController : Controller
    {
        private const string Kind = "Demo";
        private readonly ILogger<DatastoreController> _logger;
        private readonly IDatastoreWrapper _datastore;

        public DatastoreController(ILogger<DatastoreController> logger, IDatastoreWrapper datastore)
        {
            _logger = logger;
            _datastore = datastore;
        }

        public IActionResult Index()
        {
            var query = new Query(Kind);

            var result =_datastore.Query(query);

            var items = result.Select(i => new DatastoreModel() 
            { 
                Id = i.Key.Path.FirstOrDefault().Id, 
                Message = i["Message"].StringValue, 
                Count = (int)i["Count"].IntegerValue
            });

            return View(items);
        }

        [HttpPost]
        public IActionResult Add(string message, int count)
        {
            var key = _datastore.CreateKey(Kind, new Random().Next());

            var newEntity = new Entity
            {
                Key = key,
                ["Message"] = message,
                ["Count"] = count
            };
            _datastore.Upsert(newEntity);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(long id)
        {
            var key = _datastore.CreateKey(Kind, id);
            var entity = new Entity
            {
               Key = key
            };
            _datastore.Delete(entity);
            return RedirectToAction("Index");
        }
    }
}