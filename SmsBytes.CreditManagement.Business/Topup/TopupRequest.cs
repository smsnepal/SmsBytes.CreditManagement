namespace SmsBytes.CreditManagement.Business.Topup
{
    public class TopupRequest
    {
        public int Count { set; get; }
        public int Amount { set; get; }
        public string User { set; get; }
    }
}
