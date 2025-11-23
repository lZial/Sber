namespace Finance
{
    partial class Bank
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            enterTrunsferSum = new TextBox();
            Where = new Label();
            HowMuch = new Label();
            enterCardNumber = new TextBox();
            buttonTransfer = new Button();
            buttonHistory = new Button();
            RemainingMoney = new Label();
            MoneyBalance = new Label();
            SuspendLayout();
            // 
            // enterTrunsferSum
            // 
            enterTrunsferSum.AllowDrop = true;
            enterTrunsferSum.Font = new Font("Segoe UI", 10F);
            enterTrunsferSum.Location = new Point(12, 205);
            enterTrunsferSum.Name = "enterTrunsferSum";
            enterTrunsferSum.Size = new Size(250, 34);
            enterTrunsferSum.TabIndex = 3;
            enterTrunsferSum.TextChanged += enterTransferSum_TextChanged;
            enterTrunsferSum.KeyPress += EnterTransferSum_KeyPress;
            // 
            // Where
            // 
            Where.AutoSize = true;
            Where.Font = new Font("Segoe UI", 10F);
            Where.Location = new Point(12, 85);
            Where.Name = "Where";
            Where.Size = new Size(55, 28);
            Where.TabIndex = 0;
            Where.Text = "Куда";
            // 
            // HowMuch
            // 
            HowMuch.AutoSize = true;
            HowMuch.Font = new Font("Segoe UI", 10F);
            HowMuch.Location = new Point(12, 174);
            HowMuch.Name = "HowMuch";
            HowMuch.Size = new Size(89, 28);
            HowMuch.TabIndex = 1;
            HowMuch.Text = "Сколько";
            // 
            // enterCardNumber
            // 
            enterCardNumber.AllowDrop = true;
            enterCardNumber.Font = new Font("Segoe UI", 10F);
            enterCardNumber.Location = new Point(12, 116);
            enterCardNumber.MaxLength = 5;
            enterCardNumber.Name = "enterCardNumber";
            enterCardNumber.Size = new Size(250, 34);
            enterCardNumber.TabIndex = 2;
            enterCardNumber.TextChanged += enterCardNumber_TextChanged;
            enterCardNumber.KeyPress += enterCardNumber_KeyPress;
            // 
            // buttonTransfer
            // 
            buttonTransfer.Font = new Font("Segoe UI", 13F);
            buttonTransfer.Location = new Point(86, 297);
            buttonTransfer.Name = "buttonTransfer";
            buttonTransfer.Size = new Size(190, 50);
            buttonTransfer.TabIndex = 4;
            buttonTransfer.Text = "Перевести";
            buttonTransfer.UseVisualStyleBackColor = true;
            buttonTransfer.Click += buttonTransfer_Click;
            // 
            // buttonHistory
            // 
            buttonHistory.Font = new Font("Segoe UI", 9F);
            buttonHistory.Location = new Point(86, 392);
            buttonHistory.Name = "buttonHistory";
            buttonHistory.Size = new Size(190, 40);
            buttonHistory.TabIndex = 5;
            buttonHistory.Text = "История переводов";
            buttonHistory.UseVisualStyleBackColor = true;
            buttonHistory.Click += buttonHistory_Click;
            // 
            // RemainingMoney
            // 
            RemainingMoney.AutoSize = true;
            RemainingMoney.Font = new Font("Segoe UI", 10F);
            RemainingMoney.Location = new Point(12, 9);
            RemainingMoney.Name = "RemainingMoney";
            RemainingMoney.Size = new Size(62, 28);
            RemainingMoney.TabIndex = 6;
            RemainingMoney.Text = "У вас:";
            // 
            // MoneyBalance
            // 
            MoneyBalance.AutoSize = true;
            MoneyBalance.Font = new Font("Segoe UI", 10F);
            MoneyBalance.Location = new Point(80, 9);
            MoneyBalance.Name = "MoneyBalance";
            MoneyBalance.Size = new Size(23, 28);
            MoneyBalance.TabIndex = 7;
            MoneyBalance.Text = "1";
            MoneyBalance.Click += MoneyBalance_Click;
            // 
            // Bank
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(378, 444);
            Controls.Add(MoneyBalance);
            Controls.Add(RemainingMoney);
            Controls.Add(buttonHistory);
            Controls.Add(buttonTransfer);
            Controls.Add(enterTrunsferSum);
            Controls.Add(enterCardNumber);
            Controls.Add(HowMuch);
            Controls.Add(Where);
            Name = "Bank";
            Text = "Перевод";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Where;
        private Label HowMuch;
        private Button buttonTransfer;
        private Button buttonHistory;
        public TextBox enterCardNumber;
        public TextBox enterTrunsferSum;
        private Label RemainingMoney;
        private Label MoneyBalance;
    }
}
