using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pos_show
{
    public partial class Form1 : Form
    {
        SerialPort port;
        List<Point> points;
        object p_lock = new object();
        float scale_denominator;
        System.Drawing.Drawing2D.Matrix scale;
        bool new_data = false;
        System.Windows.Forms.Timer tmr;

        public Form1()
        {
            InitializeComponent();

            points = new List<Point>();
            set_scale();

            //for (int i = 0; i < 100; i++)
            //{
            //    points.Add(new Point(i * 600, i * 630));
            //    //points.Add(new Point(i, i));
            //}

            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 50;
            tmr.Tick += try_plot;
        }

        private void try_plot(object sender, EventArgs e)
        {
            if (new_data)
            {
                new_data = false;
                plotter.Refresh();
            }
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            var connection = tb_port.Text.Split(new char[] { ':' });

            if (connection.Length != 3)
            {
                MessageBox.Show("Port string error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int baud = 0;
            if (!int.TryParse(connection[1], out baud))
            {
                MessageBox.Show("Port string: baud rate parse error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (connection[2].Length != 3)
            {
                MessageBox.Show("Port string: bits description length <> 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int data_bits=0;
            if (connection[2][0] == '7')
                data_bits = 7;
            else if (connection[2][0] == '8')
                data_bits = 8;
            else if (connection[2][0] == '9')
                data_bits = 9;
            else
            {
                MessageBox.Show("Port string: data bits  <> 7, 8 or 9", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Parity par = Parity.None;
            if (connection[2][1] == 'E')
                par = Parity.Even;
            else if (connection[2][1] == 'M')
                par = Parity.Mark;
            else if (connection[2][1] == 'N')
                par = Parity.None;
            else if (connection[2][1] == 'O')
                par = Parity.Odd;
            else if (connection[2][1] == 'S')
                par = Parity.Space;
            else
            {
                MessageBox.Show("Port string: parity <> E,M,N,O,S for Even,Mark,None,Odd,Space", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StopBits stop_bits = StopBits.One;
            if (connection[2][2] == '1')
                stop_bits = StopBits.One;
            else if (connection[2][2] == '2')
                stop_bits = StopBits.OnePointFive;
            else if (connection[2][2] == '3')
                stop_bits = StopBits.OnePointFive;
            else
            {
                MessageBox.Show("Port string: stop bits  <> 1, 2, 3 for 1, 2, 1.5", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (port != null && port.IsOpen)
                port.Close();

            try
            {
                port = new SerialPort(connection[0], baud, par, data_bits, stop_bits);
                port.Open();
                port.NewLine = "\n";
                btn_open.Enabled = false;
                btn_close.Enabled = true;
                ThreadPool.QueueUserWorkItem(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                port = null;
            }

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            if (port != null && port.IsOpen)
                port.Close();

            btn_close.Enabled = false;
        }

        private void reader(object state)
        {
            tmr.Start();

            var splitter = new char[] { ',' };
            while (port != null && port.IsOpen)
            {
                try
                {
                    var str = port.ReadLine();
                    var items = str.Split(splitter, StringSplitOptions.None);

                    var cnt = items.Length / 2;

                    int x, y;
                    var pnts = new List<Point>(cnt);
                    for (int i = 0; i < cnt; i++)
                    {
                        if (int.TryParse(items[i * 2], out x) && int.TryParse(items[i * 2 + 1], out y))
                            pnts.Add(new Point(x, y));
                        else
                            Log(string.Format("Error convert {0} pair to point: {1}, {2}", i, items[i * 2], items[i * 2 + 1]));
                    }
                    lock (p_lock)
                    {
                        points = pnts;
                    }
                    new_data = true;
                }
                catch (Exception ex)
                {
                    break;
                }
            }

            btn_open.Enabled = true;

            tmr.Stop();
        }

        private void Log(string msg)
        {
        }

        private void btn_scale_Click(object sender, EventArgs e)
        {
            set_scale();
        }

        private void set_scale()
        {
            if (float.TryParse(tb_scale.Text, out scale_denominator))
            {
                scale = new System.Drawing.Drawing2D.Matrix();
                scale.Scale(1.0f / scale_denominator, 1.0f / scale_denominator);
            }
        }

        private void plotter_Paint(object sender, PaintEventArgs e)
        {
            using (var gfx = plotter.CreateGraphics())
            {
                gfx.Clear(Color.White);
                lock (p_lock)
                {
                    foreach (var p in points)
                    {
                        gfx.FillRectangle(Brushes.Black, p.X / scale_denominator, plotter.Height - p.Y / scale_denominator, 1, 1);
                    }
                }
            }
        }
    }
}
