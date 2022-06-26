using NJsonSchema;
using NJsonSchema.CodeGeneration.TypeScript;

namespace NSwag.CodeGeneration.TypeScript;

public class SISTypeScriptTypeResolver : TypeScriptTypeResolver
{
    public SISTypeScriptTypeResolver(TypeScriptGeneratorSettings settings) : base(settings)
    {
    }

    public override string Resolve(JsonSchema schema, bool isNullable, string typeNameHint)
    {
        if (schema.ActualSchema.ExtensionData?.TryGetValue("x-typescript-shared-type", out var typeName) == true)
        {
            return typeName.ToString();
        }

        if (schema.ActualSchema.ExtensionData?.TryGetValue("x-namespace", out var featureNamespace) == true
            && schema.ActualSchema.ExtensionData?.TryGetValue("x-type", out var featureType) == true)
        {
            return $"{featureNamespace}.{featureType}";
        }

        return base.Resolve(schema, isNullable, typeNameHint);
    }
}
