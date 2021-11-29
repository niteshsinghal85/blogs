using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace DemoSwaggerAnnotation
{
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Parameter |
        AttributeTargets.Property |
        AttributeTargets.Enum,
        AllowMultiple = false)]
    public class SwaggerSchemaExampleAttribute : Attribute
    {
        public SwaggerSchemaExampleAttribute(string example)
        {
            Example = example;
        }

        public string Example { get; set; }
    }

    public class SwaggerSchemaExampleFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if(context.MemberInfo != null)
            {
                var schemaAttribute = context.MemberInfo.GetCustomAttributes<SwaggerSchemaExampleAttribute>()
               .FirstOrDefault();
                if (schemaAttribute != null)
                    ApplySchemaAttribute(schema, schemaAttribute);
            }
        }

        private void ApplySchemaAttribute(OpenApiSchema schema, SwaggerSchemaExampleAttribute schemaAttribute)
        {
            if (schemaAttribute.Example != null)
            {
                schema.Example = new Microsoft.OpenApi.Any.OpenApiString(schemaAttribute.Example);
            }
        }
    }
}
