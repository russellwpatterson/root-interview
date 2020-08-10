namespace RootCodeSample.Validators
{
    public interface IValidator
    {
        void Validate(string dataLine, out string[] dataLineParts);
    }
}