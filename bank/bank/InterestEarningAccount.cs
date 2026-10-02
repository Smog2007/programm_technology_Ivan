namespace bank
{
    internal class InterestEarningAccount: BankAccount
    {
        public InterestEarningAccount(string name, decimal initialBalance): base(name, initialBalance)
        {

        }

        public virtual void PerformMonthAndTransaction()
        {
            if (Balance > 500m)
            {
                decimal interest = Balance * 0.02m;
                MakeDeposite(interest, DateTime.UtcNow, "Apply month interest");
            }
        }
    }
}
