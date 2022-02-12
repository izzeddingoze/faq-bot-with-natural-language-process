namespace FAQBOT
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.temizle_buton = new System.Windows.Forms.Button();
            this.ask_box = new System.Windows.Forms.RichTextBox();
            this.sor_buton = new System.Windows.Forms.Button();
            this.answer_box = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.temizle_buton);
            this.groupBox2.Controls.Add(this.ask_box);
            this.groupBox2.Controls.Add(this.sor_buton);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // temizle_buton
            // 
            this.temizle_buton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.temizle_buton.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.temizle_buton, "temizle_buton");
            this.temizle_buton.ForeColor = System.Drawing.Color.White;
            this.temizle_buton.Name = "temizle_buton";
            this.temizle_buton.UseVisualStyleBackColor = false;
            this.temizle_buton.Click += new System.EventHandler(this.button2_Click);
            // 
            // ask_box
            // 
            this.ask_box.BackColor = System.Drawing.SystemColors.Info;
            this.ask_box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.ask_box, "ask_box");
            this.ask_box.Name = "ask_box";
            // 
            // sor_buton
            // 
            this.sor_buton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sor_buton.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.sor_buton, "sor_buton");
            this.sor_buton.ForeColor = System.Drawing.Color.White;
            this.sor_buton.Name = "sor_buton";
            this.sor_buton.UseVisualStyleBackColor = false;
            this.sor_buton.Click += new System.EventHandler(this.button1_Click);
            // 
            // answer_box
            // 
            this.answer_box.BackColor = System.Drawing.SystemColors.Info;
            this.answer_box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.answer_box, "answer_box");
            this.answer_box.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.answer_box.Name = "answer_box";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.answer_box);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Name = "label2";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button sor_buton;
        private System.Windows.Forms.RichTextBox ask_box;
        private System.Windows.Forms.RichTextBox answer_box;
        private System.Windows.Forms.Button temizle_buton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
    }
}

