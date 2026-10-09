namespace bank
{
    public class GiftCardAccount : BankAccount
    {
        private readonly decimal _monthlyDeposit = 0m;


        public GiftCardAccount(string name, decimal initialBalance, decimal monthltDeposit = 0) : base(name, initialBalance) => _monthlyDeposit = _monthlyDeposit;
        public override void PerformMonthAndTransaction()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposite(_monthlyDeposit, DateTime.UtcNow, "Add monthly deposit");
            }
        }
        public override string ToString() => base.ToString() + $"monthly deposit: {_monthlyDeposit}";
    }
}
