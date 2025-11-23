namespace Finance
{
    partial class HistoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            HistoryBox = new TextBox();
            SuspendLayout();
            // 
            // HistoryBox
            // 
            HistoryBox.Location = new Point(12, 12);
            HistoryBox.Multiline = true;
            HistoryBox.Name = "HistoryBox";
            HistoryBox.ReadOnly = true;
            HistoryBox.ScrollBars = ScrollBars.Vertical;
            HistoryBox.Size = new Size(654, 520);
            HistoryBox.TabIndex = 0;
            HistoryBox.TextChanged += HistoryBox_TextChanged;
            // 
            // HistoryForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(678, 544);
            Controls.Add(HistoryBox);
            Font = new Font("Segoe UI", 9F);
            Name = "HistoryForm";
            Text = "История переводов";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox HistoryBox;
    }
}