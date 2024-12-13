namespace SysCapteur.Exceptions
{
    public class Error
    {
        /// <summary>
        /// the error message
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// the code associated with the exception
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// the source of the error
        /// </summary>
        public string? ErrorSource { get; set; }

        /// <summary>
        /// Generate an Error Object from an exception
        /// </summary>
        /// <param name="exception">the exception</param>
        /// <returns>an Error Object instant</returns>
        public static Error MapFromException(System.Exception exception)
            => new Error()
            {
                ErrorMessage = exception.Message,
                ErrorSource = exception.Source!,
                ErrorCode = exception.HResult,
            };

        /// <summary>
        /// create an instant of Error object from an exception
        /// </summary>
        /// <param name="exception"></param>
        public static implicit operator Error(System.Exception exception)
            => exception is null ? null : MapFromException(exception);
    }

}

