namespace TaskEase.Core.Attributes;

[AttributeUsage(validOn: AttributeTargets.Class, Inherited = false)]
public sealed class CachingDecorator : Attribute
{
}