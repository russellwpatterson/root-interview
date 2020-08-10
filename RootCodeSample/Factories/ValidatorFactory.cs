using RootCodeSample.Models;
using RootCodeSample.Validators;

namespace RootCodeSample.Factories
{
    public class ValidatorFactory : IValidatorFactory
    {
        public IValidator GetValidator<T>() where T : class, new()
        {
            switch (typeof(T))
            {
                case var t when t == typeof(Driver):
                    return new DriverValidator();
                case var t when t == typeof(Trip):
                    return new TripValidator();
                default:
                    return null;
            }
        }
    }
}