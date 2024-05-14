namespace Questionaire.Helper
{
    public class BaseResponse
    {
        public BaseResponse(bool status, object message)
        {
            Status = status;
            Data = message;
        }
        
        
        public bool Status { get; set; }
        public object Data { get; set; }
    }

    public enum ProcessStatus
    {
        Successful,
        Failed,
        Blocked,
        Pending,
        Rejected,
        Invalid,
        InActive
    }

    public class ResponseMessages
    {
        public const string ServerError = "Internal server error. Please try again later";

        public const string InternalError = "Sorry, we encountered error while processing your request. Retry later.";

        public const string NetworkError = "Unfortunately, we are unable to process your request further at the moment. Please try again later.";

        public const string Unauthorized = "You are not authorized to access this application";

        public const string StatementTicket = "Your ticket has been sent to the email and mobile number registered on your account. Proceed to confirm by entering your ticket details";

        public const string InvalidRequest = "Could not serve your request at the moment. Please check your and try again later";

        public const string ApplicationRequestFailed = "Thank you for your interest in BLP. Please note that your application cannot be concluded online, kindly send an email to sme.advisoryhelpdesk@sterling.ng";
    }

    public class FieldValidator
    {
        public object error { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
    }

    public class EnumData
{
    public string Key { get; set; }
    public int Value { get; set; }
}
}