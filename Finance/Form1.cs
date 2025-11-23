using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;

namespace Finance
{
    public partial class Bank : Form
    {
        public static Queue<string> history = new Queue<string>();

        private int CardNumber;
        private decimal TransferSum;
        private decimal balance = 500000;

        public Bank()
        {
            InitializeComponent();

            this.AcceptButton = buttonTransfer;

            TextMoneyBalance.Text = $"{balance:C}";
        }

        private void enterCardNumber_TextChanged(object sender, EventArgs CardNumberChangedEvent)
        {

        }


        private void enterTransferSum_TextChanged(object sender, EventArgs TransferSumChangedEvent)
        {

        }

        private void buttonTransfer_Click(object sender, EventArgs TransferButtonClickEvent)
        {
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
                enterCardNumber.Text = "";
                enterTrunsferSum.Text = "";
                return;
            }

            if (TransferSum > balance * 0.7m)
            {
                MessageBox.Show($"Операция приостановлена, требуется дополнительное подтверждение.\nДля безопасности ваших средств мы остановили подозрительную операциию на сумму {TransferSum:C}.\nПожалуйства, позвоните на номер банка для дополнительного подтверждения операции.", "Предупреждение!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            balance -= TransferSum;
            TextMoneyBalance.Text = $"{balance:C}";

            string operation = $"[{DateTime.Now:dd.MM.yyyy HH:mm}] Перевод на счет {CardNumber} на сумму {TransferSum:C}";
            history.Enqueue(operation);

            enterCardNumber.Text = "";
            enterTrunsferSum.Text = "";

            MessageBox.Show("Операция успешна", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
