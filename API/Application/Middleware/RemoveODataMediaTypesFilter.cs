using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Application.Middleware
{
    public class RemoveODataMediaTypesFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var response in operation.Responses.Values)
            {
                if (response.Content.Count > 1)
                {
                    if (response.Content.ContainsKey("application/json"))
                    {
                        var jsonContent = response.Content["application/json"];
                        response.Content.Clear();
                        response.Content.Add("application/json", jsonContent);
                    }
                    else
                    {
                        var first = response.Content.First();
                        response.Content.Clear();
                        response.Content.Add(first.Key, first.Value);
                    }
                }
            }
        }
    }
}
