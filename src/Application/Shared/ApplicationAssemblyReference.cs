using System.Reflection;

namespace PromotionEngine.Application.Shared;

/// <summary>
/// Provides a reference to the application assembly.
/// </summary>
public static class ApplicationAssemblyReference
{
    /// <summary>
    /// Gets the assembly reference of the application.
    /// </summary>
    public static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}