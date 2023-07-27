
namespace FreeMyPlay
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Setup";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(144, 39);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "DNS 192.168.1.1",
            "DNS 10.0.0.1",
            "DNS 9.9.9.9",
            "DNS 149.112.112.112",
            "DNS 1.1.1.1",
            "DNS 1.0.0.1",
            "DNS 64.6.64.6",
            "DNS 64.6.65.6",
            "DNS 8.8.8.8",
            "DNS 8.8.4.4",
            "DNS 84.200.69.80",
            "DNS 84.200.70.40",
            "DNS 176.103.130.130",
            "DNS 176.103.130.131",
            "DNS 216.146.35.35",
            "DNS 216.146.36.252",
            "DNS 80.67.169.12",
            "DNS 80.67.169.40",
            "DNS 80.80.80.80",
            "DNS 80.80.81.81",
            "DNS 37.235.1.174",
            "DNS 37.235.1.177",
            "DNS 156.154.70.4",
            "DNS 156.154.71.4",
            "DNS 195.46.39.39",
            "DNS 195.46.39.40",
            "DNS 208.76.50.50",
            "DNS 208.76.51.51",
            "DNS 77.88.8.7",
            "DNS 77.88.8.3",
            "DNS 185.228.168.9",
            "DNS 185.228.169.9",
            "DNS 4.2.2.1",
            "DNS 4.2.2.2",
            "DNS 4.2.2.3",
            "DNS 4.2.2.4",
            "DNS 45.90.28.218",
            "DNS 45.90.30.218"});
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(126, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "DNS 192.168.1.1";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Game Call of Duty",
            "Game Fortnite"});
            this.comboBox2.Location = new System.Drawing.Point(144, 12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(126, 21);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.Text = "Game Call of Duty";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FreeMyPlay.Properties.Resources.fmw;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(280, 73);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FreeMyPlay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}

