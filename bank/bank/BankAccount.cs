using System.Text;

namespace bank;
public enum ClientStatus
{
    Regular,
    Premium,
    Vip
}
public class BankAccount
{
    private readonly decimal _minimumBalance;
    private List<Transaction> _allTransactions = new List<Transaction>();
    public string Owner { get; private set; }

    // ЗАДАЧА 1: новое поле
    public ClientStatus Status { get; set; } = ClientStatus.Regular;
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var transaction in _allTransactions)
            {
                balance += transaction.Amount;
            }
            return balance;
        }
    }
    public string Number { get; }
    private static int s_accountNumberSeed = 1000000000;
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
    {
    }

    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Owner = name;

        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;

        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
            MakeDeposite(initialBalance, DateTime.UtcNow, "initial balance");
    }

    public void MakeDeposite(decimal amount, DateTime date, string note)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount off deposite must be positive");
        }
        var deposite = new Transaction(amount, date, note);
        _allTransactions.Add(deposite);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        Transaction? overdraftTransaction
            = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);

        _allTransactions.Add(withdrawal);

        if (overdraftTransaction is not null)
            _allTransactions.Add(overdraftTransaction);
    }
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdrawal");
        }
        else
        {
            return default;
        }
    }
    public string GetAccountHistory()
    {
        var report = new StringBuilder();

        decimal balance = 0;
        report.AppendLine("Data\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"" +
                $"{item.Date.ToShortDateString()}\t" +
                $"{item.Amount}\t{balance}\t{item.Note}");
        }
        return report.ToString();
    }
    // ЗАДАЧА 1: тело метода заполнено логикой статусов
    public virtual void PerformMonthAndTransaction()
    {
        ApplyStatusFees();
    }
    private void ApplyStatusFees()
    {
        switch (Status)
        {
            case ClientStatus.Regular:
                MakeWithdrawal(50m, DateTime.UtcNow, "Monthly fee (Regular)");
                break;

            case ClientStatus.Premium:
                if (Balance > 10000m)
                {
                    decimal interest = (Balance - 10000m) * 0.01m;
                    MakeDeposite(interest, DateTime.UtcNow, "Premium interest");
                }
                break;

            case ClientStatus.Vip:
                if (Balance > 10000m)
                {
                    decimal interest = (Balance - 10000m) * 0.03m;
                    MakeDeposite(interest, DateTime.UtcNow, "VIP interest");
                }
                ApplyCashback();
                break;
        }
    }

    private void ApplyCashback()
    {
        decimal totalWithdrawals = 0;
        foreach (var t in _allTransactions)
        {
            if (t.Amount < 0)
                totalWithdrawals += -t.Amount;
        }
        if (totalWithdrawals > 0)
        {
            decimal cashback = totalWithdrawals * 0.05m;
            MakeDeposite(cashback, DateTime.UtcNow, "VIP cashback");
        }
    }
    public override string ToString()
        => $"Type: {GetType().Name}\t" +
        $"Owner: {Owner}\t" +
        $"Number of account: {Number}\t" +
        $"Balance: {Balance}\t" +
        $"Status: {Status}";
}