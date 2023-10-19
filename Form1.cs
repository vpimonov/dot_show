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
        SerialPort gen_port;
        SerialPort port;
        List<Point> points;
        int dot_size;
        int n_frame;
        object p_lock = new object();
        float scale_denominator;
        bool new_data = false;
        System.Windows.Forms.Timer tmr;

        public Form1()
        {
            InitializeComponent();

            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                tb_port.Text = args[1];
            }

            if (args.Length > 2)
            {
                tb_scale.Text = args[2];
            }

            if (args.Length > 3)
            {
                tb_size.Text = args[3];
            }

            if (args.Length > 4)
            {
                gen_port = CreatePort(args[4]);
                btn_Test.Visible = true;
            }

            points = new List<Point>();
            set_scale();
            set_size();

            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 50;
            tmr.Tick += try_plot;
            tmr.Enabled = true;
        }

        private void try_plot(object sender, EventArgs e)
        {
            if (new_data)
            {
                new_data = false;
                n_frame++;
                lbl_frame.Text = n_frame.ToString();
                plotter.Refresh();
            }
        }

        private SerialPort CreatePort(string conn_str)
        {
            var connection = conn_str.Split(new char[] { ':' });

            if (connection.Length != 3)
            {
                MessageBox.Show("Port string error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int baud = 0;
            if (!int.TryParse(connection[1], out baud))
            {
                MessageBox.Show("Port string: baud rate parse error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (connection[2].Length != 3)
            {
                MessageBox.Show("Port string: bits description length <> 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int data_bits = 0;
            if (connection[2][0] == '7')
                data_bits = 7;
            else if (connection[2][0] == '8')
                data_bits = 8;
            else if (connection[2][0] == '9')
                data_bits = 9;
            else
            {
                MessageBox.Show("Port string: data bits  <> 7, 8 or 9", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
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
                return null;
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
                return null;
            }

            return new SerialPort(connection[0], baud, par, data_bits, stop_bits);
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            if (port != null && port.IsOpen)
                port.Close();

            try
            {
                port = CreatePort(tb_port.Text);
                port.Open();
                n_frame = 0;
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

            btn_open.Enabled = true;
            btn_close.Enabled = false;
        }

        private void reader(object state)
        {
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
        }

        private void Log(string msg)
        {
        }

        private void set_scale()
        {
            float s = 0;
            if (float.TryParse(tb_scale.Text, out s) && s > 0)
            {
                scale_denominator = s;
                new_data = true;
            }
        }

        private void set_size()
        {
            int s = 0;
            if (int.TryParse(tb_size.Text, out s) && s > 0 && s < 10)
            {
                dot_size = s;
                new_data = true;
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
                        gfx.FillRectangle(Brushes.Black, p.X / scale_denominator - dot_size / 2, plotter.Height - p.Y / scale_denominator - dot_size/2, dot_size, dot_size);
                    }
                }
            }
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            if (gen_port.IsOpen)
            {
                gen_port.Close();
                btn_Test.Text = "G";
            }
            else
            {
                gen_port.Open();
                btn_Test.Text = "X";
                ThreadPool.QueueUserWorkItem(generate_data);
            }
        }

        private void tb_scale_TextChanged(object sender, EventArgs e)
        {
            set_scale();
        }

        private void tb_size_TextChanged(object sender, EventArgs e)
        {
            set_size();
        }

        private void generate_data(object state)
        {
            var r = new Random();

            for (int i = 0; i < 1000; i++)
            {
                var str = r.Next(65535).ToString();
                for (int j = 0; j < 99; j++)
                {
                    str += ", " + r.Next(65535).ToString();
                }
                str += "\n";

                if (!gen_port.IsOpen)
                    return;

                gen_port.Write(str);
                Thread.Sleep(100);
            }

            gen_port.Close();
        }
    }
}
