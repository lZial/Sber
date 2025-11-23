using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finance
{
    public partial class HistoryForm : Form
    {
        private System.Windows.Forms.Timer refreshTimer;

        public HistoryForm()
        {
            InitializeComponent();
            InitializeTimer();
            UpdateHistoryBox();
        }

        private void InitializeTimer()
        {
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 1000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void UpdateHistoryBox()
        {
            HistoryBox.Clear();
            if (Bank.history != null && Bank.history.Count > 0)
            {
                foreach (var operation in Bank.history)
                {
                    HistoryBox.AppendText(operation + Environment.NewLine);
                }
                HistoryBox.SelectionStart = HistoryBox.TextLength;
                HistoryBox.ScrollToCaret();
            }
            else
            {
                HistoryBox.Text = "История операций пуста";
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            UpdateHistoryBox();
        }

        private void HistoryBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
            }
        }
    }
}