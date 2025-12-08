using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Finance
{
    public partial class Bank : Form
    {
        public static Queue<string> history = new Queue<string>();

        private List<Transaction> transactionHistory = new List<Transaction>();

        private Dictionary<string, int> recipientTransferCount = new Dictionary<string, int>();

        private TimeSpan averageTransactionTime = TimeSpan.Zero;
        private decimal averageTransactionAmount = 0;

        private int CardNumber;
        private decimal TransferSum;
        private decimal balance = 500000;

        public Bank()
        {
            InitializeComponent();

            this.AcceptButton = buttonTransfer;

            TextMoneyBalance.Text = $"{balance:C}";

            InitializeTestTransactions();
        }

        private void InitializeTestTransactions()
        {
            DateTime today = DateTime.Today;

            var testTransactions = new List<Transaction>
            {
                new Transaction { Amount = 600, Time = new DateTime(today.Year, today.Month, today.Day, 9, 15, 0) },
                new Transaction { Amount = 1000, Time = new DateTime(today.Year, today.Month, today.Day, 10, 30, 0)  },
                new Transaction { Amount = 8000, Time = new DateTime(today.Year, today.Month, today.Day, 12, 45, 0) },
                new Transaction { Amount = 1000, Time = new DateTime(today.Year, today.Month, today.Day, 13, 30, 0) },
                new Transaction { Amount = 10000, Time = new DateTime(today.Year, today.Month, today.Day, 17, 50, 0) },
                new Transaction { Amount = 6000, Time = new DateTime(today.Year, today.Month, today.Day, 21, 30, 0), Recipient = "98765" },
            };


            foreach (var transaction in testTransactions)
            {
                transactionHistory.Add(transaction);

                if (recipientTransferCount.ContainsKey(transaction.Recipient))
                {
                    recipientTransferCount[transaction.Recipient]++;
                }
                else
                {
                    recipientTransferCount[transaction.Recipient] = 1;
                }

                string operation = $"[{transaction.Time:dd.MM.yyyy HH:mm}] Перевод на счет {transaction.Recipient} на сумму {transaction.Amount:C}";
                history.Enqueue(operation);
            }

            CalculateAverages();
        }

        private void CalculateAverages()
        {
            if (transactionHistory.Count == 0)
                return;

            averageTransactionAmount = transactionHistory.Average(t => t.Amount);

            if (transactionHistory.Count > 0)
            {
                double totalSin = 0;
                double totalCos = 0;

                foreach (var transaction in transactionHistory)
                {
                    double angle = 2 * Math.PI * transaction.TimeOfDay.TotalHours / 24;
                    totalSin += Math.Sin(angle);
                    totalCos += Math.Cos(angle);
                }

                double averageAngle = Math.Atan2(totalSin / transactionHistory.Count,
                                                 totalCos / transactionHistory.Count);

                if (averageAngle < 0) averageAngle += 2 * Math.PI;

                double averageHours = averageAngle * 24 / (2 * Math.PI);
                averageTransactionTime = TimeSpan.FromHours(averageHours);
            }
        }

        private class Transaction
        {
            public decimal Amount { get; set; }
            public DateTime Time { get; set; }
            public string Recipient { get; set; } = string.Empty;
            public TimeSpan TimeOfDay => Time.TimeOfDay;
        }

        private int CalculateSuspicionRating(DateTime currentTime, decimal currentAmount, string recipient)
        {
            int rating = 0;

            if (transactionHistory.Count == 0)
                return rating;

            TimeSpan currentTimeOfDay = currentTime.TimeOfDay;

            double hoursDifference = Math.Abs((currentTimeOfDay - averageTransactionTime).TotalHours);

            if (hoursDifference > 12)
            {
                hoursDifference = 24 - hoursDifference;
            }

            if (hoursDifference >= 4)
            {
                rating += 1;
            }

            if (averageTransactionAmount > 0)
            {
                decimal amountRatio = currentAmount / averageTransactionAmount;

                if (amountRatio >= 3.0m)
                {
                    rating += 2;
                }
                else if (amountRatio >= 2.0m)
                {
                    rating += 1;
                }
            }

            if (recipientTransferCount.ContainsKey(recipient))
            {
                if (recipientTransferCount[recipient] < 4)
                {
                    rating += 1;
                }
            }
            else
            {
                rating += 1;
            }

            return rating;
        }

        private void enterCardNumber_TextChanged(object sender, EventArgs CardNumberChangedEvent)
        {

        }

        private void enterTransferSum_TextChanged(object sender, EventArgs TransferSumChangedEvent)
        {

        }

        private void buttonTransfer_Click(object sender, EventArgs TransferButtonClickEvent)
        {
            string recipient = enterCardNumber.Text;
            int.TryParse(enterCardNumber.Text, out CardNumber);
            decimal.TryParse(enterTrunsferSum.Text, out TransferSum);

            if (string.IsNullOrWhiteSpace(enterCardNumber.Text))
            {
                MessageBox.Show("Некоректно введён номер карты или сумма перевода!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                enterCardNumber.Focus();
                return;
            }

            if (enterCardNumber.Text.Length != 5)
            {
                MessageBox.Show("Номер карты должен содержать 5 цифр!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                enterCardNumber.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(enterTrunsferSum.Text))
            {
                MessageBox.Show("Некоректно введён номер карты или сумма перевода!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                enterTrunsferSum.Focus();
                return;
            }

            if (TransferSum == 0)
            {
                MessageBox.Show("Сумма перевода не может быть равна нулю!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                enterCardNumber.Focus();
                return;
            }

            if (TransferSum > balance)
            {
                MessageBox.Show("Недостаточно средств на счете!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TransferSum > balance * 0.7m)
            {
                MessageBox.Show($"Операция приостановлена, требуется дополнительное подтверждение.\nДля безопасности ваших средств мы остановили подозрительную операциию на сумму {TransferSum:C}.\nПожалуйста, позвоните на номер банка для дополнительного подтверждения операции.", "Предупреждение!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime currentTime = DateTime.Now;
            int suspicionRating = CalculateSuspicionRating(currentTime, TransferSum, recipient);

            if (suspicionRating >= 3)
            {
                string reason = GetSuspicionReason(suspicionRating, recipient);

                MessageBox.Show($"Операция приостановлена, требуется дополнительное подтверждение.\nДля безопасности ваших средств мы остановили подозрительную операциию на сумму {TransferSum:C}.\n\n" +
                               $"Причина: {reason}\n\n" +
                               $"Пожалуйста, позвоните на номер банка для дополнительного подтверждения операции.",
                               "Операция заблокирована",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            balance -= TransferSum;
            TextMoneyBalance.Text = $"{balance:C}";

            string operation = $"[{DateTime.Now:dd.MM.yyyy HH:mm}] Перевод на счет {recipient} на сумму {TransferSum:C}";
            history.Enqueue(operation);

            var newTransaction = new Transaction
            {
                Amount = TransferSum,
                Time = currentTime,
                Recipient = recipient
            };
            transactionHistory.Add(newTransaction);

            if (recipientTransferCount.ContainsKey(recipient))
            {
                recipientTransferCount[recipient]++;
            }
            else
            {
                recipientTransferCount[recipient] = 1;
            }

            CalculateAverages();

            enterCardNumber.Text = "";
            enterTrunsferSum.Text = "";

            MessageBox.Show("Операция успешна", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetSuspicionReason(int rating, string recipient)
        {
            var reasons = new List<string>();

            if (rating >= 3)
            {
                reasons.Add("высокий совокупный риск операции");

                TimeSpan currentTimeOfDay = DateTime.Now.TimeOfDay;
                double hoursDifference = Math.Abs((currentTimeOfDay - averageTransactionTime).TotalHours);
                if (hoursDifference > 12) hoursDifference = 24 - hoursDifference;

                if (hoursDifference >= 6)
                {
                    reasons.Add("\nзначительное отклонение от обычного времени операций");
                }

                if (averageTransactionAmount > 0 && TransferSum >= averageTransactionAmount * 3)
                {
                    reasons.Add("\nсумма значительно превышает стандартную");
                }
                else if (averageTransactionAmount > 0 && TransferSum >= averageTransactionAmount * 2)
                {
                    reasons.Add("\nсумма превышает стандартную");
                }

                if (!recipientTransferCount.ContainsKey(recipient) || recipientTransferCount[recipient] < 3)
                {
                    reasons.Add("\nперевод на новый или редко используемый номер");
                }
            }

            return string.Join("; ", reasons);
        }

        private void buttonHistory_Click(object sender, EventArgs HistoryButtonClickEvent)
        {
            HistoryForm newForm = new HistoryForm();
            newForm.Show();

            foreach (var historyElement in history)
                Console.WriteLine(historyElement);
        }

        private void enterCardNumber_KeyPress(object sender, KeyPressEventArgs CardNumberKeyPressEvent)
        {
            char number = CardNumberKeyPressEvent.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                CardNumberKeyPressEvent.Handled = true;
            }
        }

        private void EnterTransferSum_KeyPress(object sender, KeyPressEventArgs TransferSumKeyPressEvent)
        {
            char imput = TransferSumKeyPressEvent.KeyChar;
            if (!Char.IsDigit(imput) && imput != 8)
            {
                TransferSumKeyPressEvent.Handled = true;
            }
        }

        private void MoneyBalance_Click(object sender, EventArgs MoneyBalanceClickEvent)
        {

        }

        private void buttonTransfer_KeyPress(object sender, KeyPressEventArgs TransferButtonKeyPressEvent)
        {

        }
    }
}
