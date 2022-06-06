using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

namespace NSwag.CodeGeneration.CSharp;

public class SISCSharpTypeResolver : CSharpTypeResolver
{
    public SISCSharpTypeResolver(CSharpGeneratorSettings settings) : base(settings)
    {
    }

    public SISCSharpTypeResolver(CSharpGeneratorSettings settings, JsonSchema exceptionSchema) : base(settings, exceptionSchema)
    {
    }

    public override string Resolve(JsonSchema schema, bool isNullable, string typeNameHint)
    {
        if (schema.ActualSchema.ExtensionData?.TryGetValue("x-sis-type", out var typeName) == true)
        {
            return typeName.ToString();
        }

        if (schema.ActualSchema.ExtensionData?.TryGetValue("x-sis-feature", out var feature) == true)
        {
            var featureName = feature.ToString()!.Replace(".", "");
            return base.Resolve(schema, isNullable, typeNameHint).Replace($"{featureName}_", $"{feature}.");
        }

        return base.Resolve(schema, isNullable, typeNameHint);
    }
}
