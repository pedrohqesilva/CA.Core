using Microsoft.Extensions.DependencyInjection;
using NSwag;
using System.Linq;

namespace Api.Extensions
{
    public static class SwaggerExtension
    {
        public static void UseSwaggerMiddleware(this IServiceCollection services, string apiVersion, string domainName, string apiDescription)
        {
            services.AddOpenApiDocument(config =>
            {
                config.DocumentName = $"V{apiVersion}";
                config.PostProcess = document =>
                {
                    document.Info.Version = apiVersion;
                    document.Info.Title = $"CAIXAASSIM - {domainName}";
                    document.Info.Description = apiDescription;

                    SetMultipartFormDataForUploadFile(document);
                };
            });
        }

        private static void SetMultipartFormDataForUploadFile(OpenApiDocument document)
        {
            foreach (var operation in document.Operations)
            {
                var fileParameters = operation.Operation.Parameters.Where(p => p.Type == NJsonSchema.JsonObjectType.File).ToList();
                if (fileParameters.Any())
                {
                    operation.Operation.RequestBody = new OpenApiRequestBody();
                    var requestBodyContent = new OpenApiMediaType
                    {
                        Schema = new NJsonSchema.JsonSchema()
                    };
                    requestBodyContent.Schema.Type = NJsonSchema.JsonObjectType.Object;
                    foreach (var fileParameter in fileParameters)
                    {
                        requestBodyContent.Schema.Properties.Add(fileParameter.Name, new NJsonSchema.JsonSchemaProperty
                        {
                            Type = NJsonSchema.JsonObjectType.String,
                            Format = "binary",
                            Description = fileParameter.Description
                        });
                        operation.Operation.Parameters.Remove(fileParameter);
                    }
                    operation.Operation.RequestBody.Content.Add("multipart/form-data", requestBodyContent);
                }
            }
        }
    }
}