using System.Collections.Generic;
using Google.Cloud.BigQuery.V2;

namespace gcp_demo.BigQuery
{
    public class GoogleBigQuery : IBigQueryWrapper
    {
        private readonly BigQueryClient _bigQuery;
        private readonly string _projectId;
        public GoogleBigQuery(string projectId)
        {
            _projectId = projectId;
            _bigQuery = BigQueryClient.Create(projectId);

        }
        public IEnumerable<BigQueryRow> Query(string command, IEnumerable<BigQueryParameter> parameters)
        {
            return _bigQuery.ExecuteQuery(command, parameters);
        }
        
		public string GetTableFullyQualifiedId(string datasetId, string tableId)
		{
		 	return _bigQuery.GetTable(_projectId, datasetId, tableId).FullyQualifiedId;
		}
    }
}