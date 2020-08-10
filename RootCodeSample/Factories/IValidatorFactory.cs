using RootCodeSample.Validators;

namespace RootCodeSample.Factories
{
    public interface IValidatorFactory
    {
        IValidator GetValidator<T>() where T : class, new();
    }
}