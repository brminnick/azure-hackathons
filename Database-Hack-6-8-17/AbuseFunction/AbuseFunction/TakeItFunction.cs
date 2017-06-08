using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Configuration;
using System;
using System.Data.SqlClient;
using Microsoft.Azure.Documents.Client;

namespace AbuseFunction
{
    public static class TakeItFunction
    {
        [FunctionName("TakeIt")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            //We will use the DocumentClient for Cosmos
            //var docClient = new DocumentClient(
            //    new Uri(ConfigurationManager.AppSettings["endpoint"]),
            //    ConfigurationManager.AppSettings["authKey"],
            //    new ConnectionPolicy { EnableEndpointDiscovery = false }
            //    );
            //var docLink = UriFactory.CreateDocumentUri("DatabaseId", "CollectionId", "IdOfDocument");
            //var result = await docClient.ReadDocumentAsync<UserProfile>(docLink);

            //if (result.StatusCode != HttpStatusCode.OK)
            //    return null;

            //return req.CreateResponse<UserProfile>(result);

            //We will use the ConnectionStrings for all Azure SQL and Azure PostgreSql
            var sqlConnectionString = ConfigurationManager.ConnectionStrings["sql_connection"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.OpenAsync();
                DbContext dbContext = new DbContext(sqlConnection);

                try
                {
                    var profile = dbContext.GetTable<UserProfile>().FirstOrDefault();
                    log.Info($"Got Name: {profile?.FirstName}");

                    return req.CreateResponse(profile);
                }
                catch (Exception e)
                {
                    log.Info("**********");
                    log.Info(e.Message);
                    log.Info(e.ToString());
                    log.Info("**********");
                    return req.CreateResponse(HttpStatusCode.InternalServerError, "User not found and something happened");
                }
            }
        }
    }
}