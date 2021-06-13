namespace SmsBytes.CreditManagement.Common.Uuid
{
    public interface IUuidService
    {
        string GenerateUuId(string prefix);
    }

    public class UuidService : IUuidService
    {
        private string GenerateUuId()
        {
            return System.Guid.NewGuid().ToString();
        }

        public string GenerateUuId(string prefix)
        {
            return $"{prefix}_{GenerateUuId()}";
        }
    }
}
