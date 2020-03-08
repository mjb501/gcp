
using System.Collections.Generic;
using System.Linq;
using Google.Cloud.Datastore.V1;

namespace gcp_demo.Datastore
{
    public class GoogleDatastore : IDatastoreWrapper
    {
        private readonly DatastoreDb _db;
        private readonly string _projectId;
        public GoogleDatastore(string projectId, string namespaceId = "")
        {
            _projectId = projectId;
            _db = DatastoreDb.Create(projectId, namespaceId);
        }

        public Key CreateKey(string kind, long key)
        {
            var keyFactory = _db.CreateKeyFactory(kind);
            return keyFactory.CreateKey(key);
        }

        public void Delete(Entity entity)
        {
             using  (DatastoreTransaction transaction = _db.BeginTransaction())
            {
                transaction.Delete(entity);
                transaction.Commit();
            }
        }

        public IEnumerable<Entity> Query(Query query)
        {
            var results = _db.RunQuery(query).Entities;
            return results.ToList();
        }

        public void Upsert(Entity entity)
        {
            using  (DatastoreTransaction transaction = _db.BeginTransaction())
            {
                transaction.Upsert(entity);
                transaction.Commit();
            }
        }
    }
}