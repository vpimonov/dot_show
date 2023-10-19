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
        Frames frames;
        int n_frame;
        object p_lock = new object();
        bool new_data = false;
        System.Windows.Forms.Timer tmr;

        FramesPainter painter;


        public Form1()
        {
            InitializeComponent();

            painter = new FramesPainter();

            var args = Environment.GetCommandLineArgs();
            var prms = new TextBox[] { tb_port, tb_scale, tb_size };

            for (int i = 0; i < args.Length-1; i++)
                if (i < prms.Length) prms[i].Text = args[i+1];

            int tmp = 0;
            if (args.Length > 4 && int.TryParse(args[4], out tmp))
                painter = new FramesPainter(tmp+1);
            else
                painter = new FramesPainter(2);

            if (args.Length > 5)
            {
                gen_port = CreatePort(args[5]);
                btn_Test.Visible = true;
            }

            frames = new Frames(painter.depth);
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
            {
                btn_open.Text = "Open";
                port.Close();
                return;
            }

            try
            {
                port = CreatePort(tb_port.Text);
                port.Open();
                n_frame = 0;
                port.NewLine = "\n";
                ThreadPool.QueueUserWorkItem(reader);
                btn_open.Text = "Close";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                port = null;
            }
        }

        private void reader(object state)
        {
            var splitter = new char[] { ',' };
            while (port != null && port.IsOpen)
            {
                try
                {
                    var str = port.ReadLine();
                    var frm = FrameParserH.Parse(str);
                    lock (p_lock)
                    {
                        frames.Ins(frm);
                    }
                    new_data = true;
                }
                catch (Exception ex)
                {
                    break;
                }
            }

            Invoke(new Action(() => { btn_open.Text = "Open"; }));
        }

        private void Log(string msg)
        {
        }

        private void set_scale()
        {
            float s = 0;
            if (float.TryParse(tb_scale.Text, out s) && s > 0)
            {
                painter.scale_denominator = s;
                new_data = true;
            }
        }

        private void set_size()
        {
            int s = 0;
            if (int.TryParse(tb_size.Text, out s) && s > 0 && s < 10)
            {
                painter.dot_size = s;
                new_data = true;
            }
        }

        private void plotter_Paint(object sender, PaintEventArgs e)
        {
            using (var gfx = plotter.CreateGraphics())
            {
                lock (p_lock)
                {
                    painter.Draw(frames, gfx);
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
                /*
                var str = r.Next(65535).ToString("X4");
                for (int j = 0; j < 99; j++)
                {
                    str += ", " + r.Next(65535).ToString("X4");
                }
                str += "\n";

                if (!gen_port.IsOpen)
                    return;
                */
                var str = "1,55,1,5\n";
                gen_port.Write(str);
                Thread.Sleep(10);
            }

            gen_port.Close();

            Invoke(new Action(() => { btn_Test.Text = "G"; }));
        }
    }

    class Frame : List<Point>
    {
    }

    class Frames : List<Frame>
    {
        int depth;

        public Frames(int depth)
        {
            this.depth = depth;
        }

        public void Ins(Frame f)
        {
            while (Count >= depth)
            {
                RemoveAt(Count - 1);
            }

            Insert(0, f);
        }
    }

    class FramesPainter
    {
        Brush[] brushes;
        public int depth { get; private set; }

        public float scale_denominator { get; set; }
        public int dot_size { get; set; }

        public FramesPainter()
            : this(2)
        {
        }

        public FramesPainter(int depth)
        {
            this.depth = depth;
            brushes = new Brush[depth];
            for (int i = 0; i < depth; i++)
            {
                var tmp = 255 / (depth-1) * i;
                brushes[i] = new SolidBrush(Color.FromArgb(tmp, tmp, tmp));
            }
        }

        public void Draw(Frames f, Graphics g)
        {
            //g.Clear(Color.White);
            for (var i = f.Count-1; i >= 0; i--)
            {
                foreach (var p in f[i])
                {
                    g.FillRectangle(brushes[i], p.X / scale_denominator, g.VisibleClipBounds.Height - p.Y / scale_denominator, dot_size, dot_size);
                }
            }
        }
    }

    class FrameParser
    {
        static readonly char[] splitter = new char[] { ',' };

        static public Frame Parse(string str)
        {
            var items = str.Split(splitter, StringSplitOptions.None);

            var cnt = items.Length / 2;

            int x, y;
            var f = new Frame();
            for (int i = 0; i < cnt; i++)
            {
                x = 0;
                y = 0;
                int.TryParse(items[i * 2], out x);
                int.TryParse(items[i * 2 + 1], out y);

                f.Add(new Point(x, y));
            }

            return f;
        }
    }

    class FrameParserH
    {
        static readonly char[] splitter = new char[] { ',' };

        static public Frame Parse(string str)
        {
            var items = str.Split(splitter, StringSplitOptions.None);

            var cnt = items.Length / 2;

            int x, y;
            var f = new Frame();
            for (int i = 0; i < cnt; i++)
            {
                x = Convert.ToInt32(items[i * 2 + 0].Trim(), 16);
                y = Convert.ToInt32(items[i * 2 + 1].Trim(), 16);

                f.Add(new Point(x, y));
            }

            return f;
        }
    }
}
