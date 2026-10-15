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

            InterestEarningAccount interest = new InterestEarningAccount("Ivan", 1000);
            interest.PerformMonthAndTransaction();

            Console.WriteLine(account1.GetAccountHistory());

            LineOfCreditAccount lineOfCredit = new LineOfCreditAccount("Ivan", 1000, 1000m);
            lineOfCredit.MakeWithdrawal(200m, DateTime.UtcNow, "credit");

            GiftCardAccount giftCard = new GiftCardAccount("Ivan", 1000m, 5000m);

            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(account1);
            accounts.Add(interest);
            accounts.Add(lineOfCredit);
            accounts.Add(giftCard);

            foreach (BankAccount account in accounts)
            {
                Console.WriteLine(account);
                account.PerformMonthAndTransaction();
                Console.WriteLine(account.GetAccountHistory());
            }

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

            // ЗАДАЧА 1: проверка статусов
            Console.WriteLine("\n=== Проверка статусов ===");

            BankAccount regular = new BankAccount("Regular Guy", 100_000m) { Status = ClientStatus.Regular };
            regular.PerformMonthAndTransaction();
            Console.WriteLine(regular);
            Console.WriteLine(regular.GetAccountHistory());

            BankAccount premium = new BankAccount("Premium Guy", 100_000m) { Status = ClientStatus.Premium };
            premium.PerformMonthAndTransaction();
            Console.WriteLine(premium);

            BankAccount vip = new BankAccount("VIP Guy", 100_000m) { Status = ClientStatus.Vip };
            vip.MakeWithdrawal(10_000m, DateTime.UtcNow, "Покупка");
            vip.PerformMonthAndTransaction();
            Console.WriteLine(vip);
            Console.WriteLine(vip.GetAccountHistory());
        }
    }
}
