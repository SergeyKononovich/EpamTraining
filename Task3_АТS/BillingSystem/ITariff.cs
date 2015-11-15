using Task3_АТS.ATS;

namespace Task3_АТS.BillingSystem
{
    public interface ITariff
    {
        int GetCost(CallInfo callInfo, bool source);
    }
}
