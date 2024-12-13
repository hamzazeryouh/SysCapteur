namespace SysCapteur.Exceptions
{
    public class CustomException 
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        public CustomException(int errorCode, string message) 
        {
            ErrorCode = errorCode;
            Message = message;
        }
    }
}
