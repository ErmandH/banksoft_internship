namespace AppointmentSystemDemo
{
    partial class EntryForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnApp = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnPatient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(158, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 44);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ana Sayfa";
            // 
            // btnApp
            // 
            this.btnApp.Location = new System.Drawing.Point(42, 121);
            this.btnApp.Name = "btnApp";
            this.btnApp.Size = new System.Drawing.Size(118, 60);
            this.btnApp.TabIndex = 1;
            this.btnApp.Text = "Randevu";
            this.btnApp.UseVisualStyleBackColor = true;
            this.btnApp.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(176, 121);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 60);
            this.button2.TabIndex = 2;
            this.button2.Text = "Doktor";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnPatient
            // 
            this.btnPatient.Location = new System.Drawing.Point(315, 121);
            this.btnPatient.Name = "btnPatient";
            this.btnPatient.Size = new System.Drawing.Size(131, 60);
            this.btnPatient.TabIndex = 3;
            this.btnPatient.Text = "Hasta";
            this.btnPatient.UseVisualStyleBackColor = true;
            this.btnPatient.Click += new System.EventHandler(this.btnPatient_Click);
            // 
            // EntryForm
            // 
            this.ClientSize = new System.Drawing.Size(495, 223);
            this.Controls.Add(this.btnPatient);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnApp);
            this.Controls.Add(this.label2);
            this.Name = "EntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ana Sayfa";
            this.Load += new System.EventHandler(this.EntryForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApp;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnPatient;
    }
}

