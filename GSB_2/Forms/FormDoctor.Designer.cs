namespace GSB_2.Forms
{
    partial class FormDoctor
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
            ContentPanel = new Panel();
            SidePanel = new Panel();
            ContentPanel.SuspendLayout();
            SuspendLayout();
            // 
            // ContentPanel
            // 
            ContentPanel.Controls.Add(SidePanel);
            ContentPanel.Dock = DockStyle.Fill;
            ContentPanel.Location = new Point(0, 0);
            ContentPanel.Name = "ContentPanel";
            ContentPanel.Size = new Size(800, 450);
            ContentPanel.TabIndex = 2;
            // 
            // SidePanel
            // 
            SidePanel.Location = new Point(0, 0);
            SidePanel.Name = "SidePanel";
            SidePanel.Size = new Size(144, 450);
            SidePanel.TabIndex = 0;
            // 
            // FormDoctor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ContentPanel);
            Name = "FormDoctor";
            Text = "FormDoctor";
            ContentPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel ContentPanel;
        private Panel SidePanel;
    }
}