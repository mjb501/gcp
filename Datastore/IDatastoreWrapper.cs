
using System.Collections.Generic;
using Google.Cloud.Datastore.V1;

namespace gcp_demo.Datastore
{
    public interface IDatastoreWrapper
    {      
        IEnumerable<Entity> Query(Query query);
        void Upsert(Entity entity);
        void Delete(Entity entity);
        Key CreateKey(string kind, long key);
    }
}