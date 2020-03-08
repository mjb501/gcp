
using System.Collections.Generic;
using Google.Cloud.BigQuery.V2;

namespace gcp_demo.BigQuery
{
    public interface IBigQueryWrapper
    {
        IEnumerable<BigQueryRow> Query(string command, IEnumerable<BigQueryParameter> parameters);
		string GetTableFullyQualifiedId(string datasetId, string tableId);
    }
}