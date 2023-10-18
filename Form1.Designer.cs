namespace pos_show
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
            this.tb_port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_scale = new System.Windows.Forms.Button();
            this.tb_scale = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_open = new System.Windows.Forms.Button();
            this.plotter = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_port
            // 
            this.tb_port.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_port.Location = new System.Drawing.Point(56, 7);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(181, 22);
            this.tb_port.TabIndex = 0;
            this.tb_port.Text = "COM1:115200:8N1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_scale);
            this.panel1.Controls.Add(this.tb_scale);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.btn_open);
            this.panel1.Controls.Add(this.tb_port);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(656, 39);
            this.panel1.TabIndex = 2;
            // 
            // btn_scale
            // 
            this.btn_scale.Location = new System.Drawing.Point(530, 3);
            this.btn_scale.Name = "btn_scale";
            this.btn_scale.Size = new System.Drawing.Size(75, 30);
            this.btn_scale.TabIndex = 7;
            this.btn_scale.Text = "Set scale";
            this.btn_scale.UseVisualStyleBackColor = true;
            this.btn_scale.Click += new System.EventHandler(this.btn_scale_Click);
            // 
            // tb_scale
            // 
            this.tb_scale.Location = new System.Drawing.Point(473, 8);
            this.tb_scale.Name = "tb_scale";
            this.tb_scale.Size = new System.Drawing.Size(51, 22);
            this.tb_scale.TabIndex = 6;
            this.tb_scale.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Scale 1/";
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(324, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 30);
            this.btn_close.TabIndex = 4;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(243, 3);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 30);
            this.btn_open.TabIndex = 3;
            this.btn_open.Text = "Open";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // plotter
            // 
            this.plotter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotter.Location = new System.Drawing.Point(0, 39);
            this.plotter.Name = "plotter";
            this.plotter.Size = new System.Drawing.Size(656, 647);
            this.plotter.TabIndex = 2;
            this.plotter.Paint += new System.Windows.Forms.PaintEventHandler(this.plotter_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 686);
            this.Controls.Add(this.plotter);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_scale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Panel plotter;
        private System.Windows.Forms.Button btn_scale;
    }
}

