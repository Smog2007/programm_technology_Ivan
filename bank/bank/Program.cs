namespace bank
{
    public class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount("Ivan", 10000000000000);
            BankAccount account2 = new BankAccount("Artur", 100);
            Console.WriteLine($"account: {account1.Owner} {account1.Balance} {account1.Number}");
            Console.WriteLine($"account: {account2.Owner} {account2.Balance} {account2.Number}");
            account1.MakeDeposite(1000, DateTime.UtcNow, "vse good");
            Console.WriteLine(account1.Balance);
            account1.MakeWithdrawal(100, DateTime.UtcNow, "vse ploxo");
            Console.WriteLine(account1.Balance);

            Console.WriteLine(account1.GetAccountHistory());

            try
            {
                account2.MakeWithdrawal(1000, DateTime.UtcNow, "T_T");
                Console.WriteLine(account2.Balance);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            InterestEarningAccount interestEarning = new("Ivan", 1000m);
            interestEarning.MakeDeposite(1000m, DateTime.UtcNow, "UwU");
            interestEarning.MakeWithdrawal(10m, DateTime.UtcNow, "O_O");
            interestEarning.PerformMonthAndTransaction();
            Console.WriteLine(interestEarning);
            Console.WriteLine(interestEarning.GetAccountHistory());

        }
    }
}
