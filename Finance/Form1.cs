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

            TextMoneyBalance.Text = $"{balance:C}";
        }

        private void enterCardNumber_TextChanged(object sender, EventArgs CardNumberChangedEvent)
        {

        }


        private void enterTransferSum_TextChanged(object sender, EventArgs SumChangedEvent)
        {

        }

        private void buttonTransfer_Click(object sender, EventArgs TransferEvent)
        {
            int.TryParse(enterCardNumber.Text, out CardNumber);
            decimal.TryParse(enterTrunsferSum.Text, out TransferSum);

            if (TransferSum > balance)
            {
                MessageBox.Show("Недостаточно средств на счете!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                enterCardNumber.Text = "";
                enterTrunsferSum.Text = "";
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

        private void buttonHistory_Click(object sender, EventArgs HistoryClickEvent)
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
            char number = TransferSumKeyPressEvent.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                TransferSumKeyPressEvent.Handled = true;
            }
        }

        private void MoneyBalance_Click(object sender, EventArgs e)
        {

        }
    }
}
