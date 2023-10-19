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
            this.lbl_frame = new System.Windows.Forms.Label();
            this.tb_size = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Test = new System.Windows.Forms.Button();
            this.tb_scale = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_open = new System.Windows.Forms.Button();
            this.plotter = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_port
            // 
            this.tb_port.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_port.Location = new System.Drawing.Point(32, 5);
            this.tb_port.Margin = new System.Windows.Forms.Padding(2);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(109, 20);
            this.tb_port.TabIndex = 1;
            this.tb_port.Text = "COM1:115200:8N1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_frame);
            this.panel1.Controls.Add(this.tb_size);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btn_Test);
            this.panel1.Controls.Add(this.tb_scale);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_open);
            this.panel1.Controls.Add(this.tb_port);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 32);
            this.panel1.TabIndex = 2;
            // 
            // lbl_frame
            // 
            this.lbl_frame.AutoSize = true;
            this.lbl_frame.Location = new System.Drawing.Point(371, 8);
            this.lbl_frame.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_frame.Name = "lbl_frame";
            this.lbl_frame.Size = new System.Drawing.Size(13, 13);
            this.lbl_frame.TabIndex = 11;
            this.lbl_frame.Text = "0";
            // 
            // tb_size
            // 
            this.tb_size.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_size.Location = new System.Drawing.Point(336, 5);
            this.tb_size.Margin = new System.Windows.Forms.Padding(2);
            this.tb_size.Name = "tb_size";
            this.tb_size.Size = new System.Drawing.Size(27, 20);
            this.tb_size.TabIndex = 4;
            this.tb_size.Text = "3";
            this.tb_size.TextChanged += new System.EventHandler(this.tb_size_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Dot size";
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(5, 3);
            this.btn_Test.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(23, 24);
            this.btn_Test.TabIndex = 0;
            this.btn_Test.Text = "G";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Visible = false;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // tb_scale
            // 
            this.tb_scale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_scale.Location = new System.Drawing.Point(252, 5);
            this.tb_scale.Margin = new System.Windows.Forms.Padding(2);
            this.tb_scale.Name = "tb_scale";
            this.tb_scale.Size = new System.Drawing.Size(27, 20);
            this.tb_scale.TabIndex = 3;
            this.tb_scale.Text = "100";
            this.tb_scale.TextChanged += new System.EventHandler(this.tb_scale_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Scale 1/";
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(148, 2);
            this.btn_open.Margin = new System.Windows.Forms.Padding(2);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(56, 24);
            this.btn_open.TabIndex = 0;
            this.btn_open.Text = "Open";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // plotter
            // 
            this.plotter.BackColor = System.Drawing.Color.White;
            this.plotter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotter.Location = new System.Drawing.Point(0, 32);
            this.plotter.Margin = new System.Windows.Forms.Padding(2);
            this.plotter.Name = "plotter";
            this.plotter.Size = new System.Drawing.Size(492, 525);
            this.plotter.TabIndex = 2;
            this.plotter.Paint += new System.Windows.Forms.PaintEventHandler(this.plotter_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 557);
            this.Controls.Add(this.plotter);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "dot_show";
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
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Panel plotter;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.TextBox tb_size;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_frame;
    }
}

