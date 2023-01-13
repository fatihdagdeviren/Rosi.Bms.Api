namespace Rosi.BMS.API.Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message)
            : base(false, message)
        {
        }

        public ErrorResult()
            : base(false)
        {
        }
    }
}