namespace SmsBytes.CreditManagement.Business.Deduction
{
    public class DeductionRequest
    {
        public string User { set; get; }
        public int Count { set; get; }
        public string Ref { set; get; }
    }
}
