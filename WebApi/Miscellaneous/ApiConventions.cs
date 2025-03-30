using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace WebApi.Miscellaneous;

public static class ApiConventions
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<uint>(StatusCodes.Status404NotFound)]
    public static void Get(uint id) { }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public static void Get([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] object request) { }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public static void Create([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] object request) { }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<uint>(StatusCodes.Status404NotFound)]
    public static void Replace(uint id, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] object request) { }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<uint>(StatusCodes.Status404NotFound)]
    public static void Update(uint id, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] object patchDocument) { }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<uint>(StatusCodes.Status404NotFound)]
    public static void Delete(uint id) { }
}
