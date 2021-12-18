using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DemoFileUpload
{
public class FileUploadFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
        var formParameters = context.ApiDescription.ParameterDescriptions
            .Where(paramDesc => paramDesc.IsFromForm());
        // already taken care by swashbuckle
        if(formParameters.Any())
		{
            return;
		}
        if(operation.RequestBody!=null)
		{
            return;
		}
        if (context.ApiDescription.HttpMethod == HttpMethod.Post.Method)
        {
            var uploadFileMediaType = new OpenApiMediaType()
            {
                Schema = new OpenApiSchema()
                {
                    Type = "object",
                    Properties =
                    {
                        ["files"] = new OpenApiSchema()
                        {
                            Type = "array",
                            Items = new OpenApiSchema()
                            {
                                Type = "string",
                                Format = "binary"
                            }
                        }
                    },
                    Required = new HashSet<string>() { "files" }
                }
            };

            operation.RequestBody = new OpenApiRequestBody
            {
                Content = { ["multipart/form-data"] = uploadFileMediaType }
            };
        }
    }
}

public static class Helper
{
    internal static bool IsFromForm(this ApiParameterDescription apiParameter)
    {
        var source = apiParameter.Source;
        var elementType = apiParameter.ModelMetadata?.ElementType;

        return (source == BindingSource.Form || source == BindingSource.FormFile)
            || (elementType != null && typeof(IFormFile).IsAssignableFrom(elementType));
    }
}
}
