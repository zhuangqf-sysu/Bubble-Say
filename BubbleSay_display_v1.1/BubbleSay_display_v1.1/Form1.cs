using CometD.Bayeux;
using CometD.Bayeux.Client;
using CometD.Client;
using CometD.Client.Transport;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BubbleSay_display_v1._1
{
    public partial class Form1 : Form
    {
        private const uint WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);
        private const int LWA_ALPHA = 0x2;
        private const int LWA_COLORKEY = 1;
        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(
            IntPtr hwnd,
            int nIndex,
            uint dwNewLong
            );
        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(
            IntPtr hwnd,
            int nIndex
            );
        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(
            IntPtr hwnd,
            int crKey,
            int bAlpha,
            int dwFlags
            );
        public void CanPenetrate(Form ff, bool mode)
        {
            //取得ff的handle
            uint intExTemp = GetWindowLong(ff.Handle, GWL_EXSTYLE);
            //WS_EX_TRANSPARENT设置鼠标穿透透明的部分，WS_EX_LAYERED设置窗口具有透明属性
            //mode=true的时候鼠标穿透，mode=false的时候鼠标不穿透
            if (mode)
            {
                uint oldGWLEx = SetWindowLong(ff.Handle, GWL_EXSTYLE, intExTemp | WS_EX_TRANSPARENT | WS_EX_LAYERED);
            }
            else
            {
                uint oldGWLEx = SetWindowLong(ff.Handle, GWL_EXSTYLE, intExTemp | WS_EX_LAYERED);
            }
            //LWA_ALPHA | LWA_COLORKEY表示0x00F5F5F5的地方变为透明，其他地方透明度为255（不透明）
            SetLayeredWindowAttributes(ff.Handle, 0x00787878, 255, LWA_ALPHA | LWA_COLORKEY);
        }

        private class userstring
        {
            public string info;
            public int x;
            public int y;
            public Color style = Color.FromArgb(0xCC, 0xCC, 0xCC);

            public userstring(string v, int width, int height)
            {
                this.info = v;
                this.x = width;
                this.y = height;
                //this.style = Color.Black;
            }
            public userstring(string v,int width,int height,Color s)
            {
                this.info = v; this.style = s;
                this.x = width; this.y = height;
            }
        }

        private TextBox chan;
        private Panel p1, p2, e1, e2, e3, e4, settingOK, main_win, setting_win;
        private Label lb, alb;
        private HScrollBar hsb1;
        private Form f = new Form();
        private PictureBox pb;
        private Bitmap bm = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
        private Graphics temp;
        private userstring tempus = new userstring("", 0, 0);
        private static BayeuxClient client;
        private NotifyIcon ni = new NotifyIcon();
        private Icon icon = Resource1.BubbleSay;
        private float emSize = 30;
        //private int statics = 0;

        private enum status
        {
            noResponse,
            success,
            fail
        };
        status enterSuccess = status.noResponse;
        const int MAX_TIME = 5;
        int tryTime = MAX_TIME;

        private System.Collections.Concurrent.ConcurrentQueue<userstring> info_queue = 
            new System.Collections.Concurrent.ConcurrentQueue<userstring>();

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.BackColor = Color.FromArgb(0x78, 0x78, 0x78);
            Bitmap bb = new Bitmap(800, 600);
            Graphics g = Graphics.FromImage(bb);
            g.DrawImage(Resource1.BS_dis, 0, 0);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.BackgroundImage = bb;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = 800;
            this.Height = 600;
            this.Icon = icon;
            ni.Text = "BubbleSay";
            ni.Icon = icon;
            ni.DoubleClick += Ni_DoubleClick;
            MenuItem[] mi = new MenuItem[] {new MenuItem("Maximum",Ni_DoubleClick),new MenuItem("Exit",E1_Click) };
            ni.ContextMenu = new ContextMenu(mi);

            main_win = new Panel(); setting_win = new Panel();
            main_win.Width = 800; main_win.Height = 600;
            setting_win.Width = 800; setting_win.Height = 600;
            main_win.BackColor = Color.Transparent; setting_win.BackColor = Color.Transparent;
            setting_win.Visible = false;
            this.Controls.Add(main_win); this.Controls.Add(setting_win);

            chan = new TextBox();
            main_win.Controls.Add(chan); 
            chan.BorderStyle = BorderStyle.None;
            chan.Location = new Point(375, 295);
            chan.Font = new Font("幼圆", 15F, FontStyle.Regular, GraphicsUnit.Point);

            p1 = new Panel();
            p1.Width = 120; p1.Height = 40;
            p1.BackColor = Color.Transparent;
            p1.BackgroundImage = Resource1.button1;
            p1.Location = new Point(260, 360);
            p1.BorderStyle = BorderStyle.None;
            main_win.Controls.Add(p1);

            p2 = new Panel();
            p2.Width = 120; p2.Height = 40;
            p2.BackColor = Color.Transparent;
            p2.BackgroundImage = Resource1.button2;
            p2.Location = new Point(390, 360);
            p2.BorderStyle = BorderStyle.None;
            main_win.Controls.Add(p2);

            e1 = new Panel();
            e1.Width = 40; e1.Height = 40;
            e1.BackColor = Color.Transparent;
            e1.BackgroundImage = Resource1.叉叉;
            e1.Location = new Point(618, 483);
            e1.BorderStyle = BorderStyle.None;
            main_win.Controls.Add(e1);
            
            e2 = new Panel();
            e2.Width = 39; e2.Height = 39;
            e2.BackColor = Color.Transparent;
            e2.BackgroundImage = new Bitmap(Resource1.minimum, new Size(39, 39));
            e2.Location = new Point(512, 469);
            e2.BorderStyle = BorderStyle.None;
            main_win.Controls.Add(e2);

            e3 = new Panel();
            e3.Width = 40; e3.Height = 40;
            e3.BackColor = Color.Transparent;
            e3.BackgroundImage = Resource1.叉叉;
            e3.Location = new Point(618, 483);
            e3.BorderStyle = BorderStyle.None;
            setting_win.Controls.Add(e3);

            e4 = new Panel();
            e4.Width = 39; e4.Height = 39;
            e4.BackColor = Color.Transparent;
            e4.BackgroundImage = new Bitmap(Resource1.minimum, new Size(39, 39));
            e4.Location = new Point(512, 469);
            e4.BorderStyle = BorderStyle.None;
            setting_win.Controls.Add(e4);

            settingOK = new Panel();
            settingOK.Width = 120; settingOK.Height = 40;
            settingOK.BackColor = Color.Transparent;
            settingOK.BackgroundImage = Resource1.button_ok;
            settingOK.Location = new Point(330, 360);
            settingOK.BorderStyle = BorderStyle.None;
            setting_win.Controls.Add(settingOK);

            p1.Click += P1_Click;
            p1.MouseEnter += P1_MouseHover;
            p1.MouseLeave += P1_MouseLeave;
            p2.Click += P2_Click;
            p2.MouseEnter += P2_MouseHover;
            p2.MouseLeave += P2_MouseLeave;
            e1.Click += E1_Click;
            e2.Click += E2_Click;
            e3.Click += E1_Click;
            e4.Click += E2_Click;
            settingOK.Click += SettingOK_Click;
            settingOK.MouseEnter += SettingOK_MouseHover;
            settingOK.MouseLeave += SettingOK_MouseLeave;

            lb = new Label();
            lb.BackColor = Color.Transparent;
            lb.Text = "Channel:";
            lb.ForeColor = Color.White;
            lb.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            lb.Location = new Point(280, 295);
            main_win.Controls.Add(lb);

            alb = new Label();
            alb.BackColor = Color.Transparent;
            alb.Text = "FontSize:"; alb.Width = 100;
            alb.ForeColor = Color.White;
            alb.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            alb.Location = new Point(235, 290);
            setting_win.Controls.Add(alb);

            hsb1 = new HScrollBar();
            hsb1.Minimum = 15; hsb1.Maximum = 45;
            hsb1.SmallChange = 1; hsb1.LargeChange = 5;
            hsb1.Width = 180; hsb1.Value = (int)emSize;
            hsb1.Location = new Point(335, 295);
            setting_win.Controls.Add(hsb1);
            hsb1.ValueChanged += Hsb1_ValueChanged;
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            int winw = SystemInformation.VirtualScreen.Width;
            int winh = SystemInformation.VirtualScreen.Height;
            this.Location = new Point((winw - 800) / 2, (winh - 600) / 2);
            CanPenetrate(this, false);
            this.Show();
        }

        private void SettingOK_MouseLeave(object sender, EventArgs e)
        {
            settingOK.BackgroundImage = Resource1.button_ok;
        }

        private void SettingOK_MouseHover(object sender, EventArgs e)
        {
            settingOK.BackgroundImage = Resource1.button_okhv;
        }

        private void P2_MouseLeave(object sender, EventArgs e)
        {
            p2.BackgroundImage = Resource1.button2;
        }

        private void P2_MouseHover(object sender, EventArgs e)
        {
            p2.BackgroundImage = Resource1.button2hv;
        }

        private void P1_MouseLeave(object sender, EventArgs e)
        {
            p1.BackgroundImage = Resource1.button1;
        }

        private void P1_MouseHover(object sender, EventArgs e)
        {
            p1.BackgroundImage = Resource1.button1hv;
        }

        private void Ni_DoubleClick(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            CanPenetrate(this, false);
            this.Show();
            setting_win.Show();
            this.Activate();            
            ni.Visible = false;
        }

        private void SettingOK_Click(object sender, EventArgs e)
        {
            settingOK.BackgroundImage = Resource1.button_ok;
            if (!enterSuccess.Equals(status.success))
            {
                setting_win.Hide();
                main_win.Show();
            }
            else
            {
                this.Hide();
                this.ShowInTaskbar = false;
                ni.Visible = true;
            }
        }

        private void Hsb1_ValueChanged(object sender, EventArgs e)
        {
            emSize = hsb1.Value;
        }

        private void P1_Click(object sender, EventArgs e)
        {
            p1.BackgroundImage = Resource1.button1;
            main_win.Hide();
            setting_win.Show();
        }

        private void E2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void E1_Click(object sender, EventArgs e)
        {
            try
            {
                tryToDelete(chan.Text);
                client.Disconnect(3000);
            }
            catch(Exception) { }
            if (ni.Visible) ni.Visible = false;
            f.Close();
            this.Close();
        }

        private void P2_Click(object sender, EventArgs e)
        {
            if (chan.Text.Equals(""))
            {
                MessageBox.Show("Please input channel!");
                return;
            }
            string mychannel = chan.Text;
            p2.BackgroundImage = Resource1.button2;

            //新加的函数：
            tryToCreate(mychannel);
            reTry:
            if (enterSuccess.Equals(status.fail))
            {
                return;
            }
            else if (enterSuccess.Equals(status.noResponse))
            {
                tryTime--;
                Thread.Sleep(2000);
                if (tryTime <= 0)
                {
                    tryTime = MAX_TIME;
                    MessageBox.Show("connect to server fail...");
                    return;
                }
                goto reTry;
            }
            else
            {
                connect(mychannel);
                main_win.Hide();
                this.ShowInTaskbar = false;
                this.Hide();
                ni.Visible = true;
                f.ShowInTaskbar = false;
                f.BackColor = Color.FromArgb(0x78, 0x78, 0x78);
                f.FormBorderStyle = FormBorderStyle.None;
                f.Height = SystemInformation.VirtualScreen.Height;
                f.Width = SystemInformation.VirtualScreen.Width;
                f.TopMost = true;
                pb = new PictureBox();
                f.Controls.Add(pb);
                pb.Height = f.Height;
                pb.Width = f.Width;
                pb.BorderStyle = BorderStyle.None;
                pb.Location = new Point(0, 0);
                CanPenetrate(f, true);
                f.Show();

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 10;
                timer.Tick += Timer_Tick;
                timer.Start();

                //Thread thr = new Thread(new ThreadStart(getString));
                //thr.IsBackground = true;
                //thr.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            temp = Graphics.FromImage(bm);
            temp.Clear(Color.Transparent);
            temp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (userstring u in info_queue)
            {
                temp.DrawString(u.info, new Font(new FontFamily("幼圆"), emSize), new SolidBrush(u.style), u.x, u.y);
                u.x -= 2;
            }
            if (info_queue.TryPeek(out tempus) == false) tempus = new userstring("", 0, 0);
            if (tempus.x < SystemInformation.VirtualScreen.Width * (-1)) info_queue.TryDequeue(out tempus);
            pb.Image = bm;
        }
        /*int index = 0;
        private void getString()
        {
            while (index < 100)
            {
                Thread.Sleep(new Random().Next(2000));
                info_queue.Enqueue(new userstring("你好!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!",
                    f.Width, new Random().Next(f.Height),Color.Black));
                index++;
            }
        }*/

        private void OnMessageReceived(IClientSessionChannel channel, IMessage message, BayeuxClient client)
        {
            //Console.WriteLine(message.ToString());
            //Console.WriteLine(message.Channel);
            IDictionary<String, Object> data = message.DataAsDictionary;
            //Console.WriteLine(data["sender"]);
            //Console.WriteLine(data["msg"]);
            Color tempColor = Color.FromArgb(0xCC, 0xCC, 0xCC);
            switch (data["style"].ToString())
            {
                case "opinion":
                    tempColor = Color.FromArgb(0xF8, 0xC3, 0x01); break;
                case "question":
                    tempColor = Color.FromArgb(0xEF, 0x43, 0x40); break;
                case "answer":
                    tempColor = Color.FromArgb(0x6E, 0x3F, 0xCF); break;
                case "chatting":
                    tempColor = Color.FromArgb(0xA6, 0xD4, 0xF2); break;
                default:
                    break;
            }
            info_queue.Enqueue(new userstring(data["sender"] + ":" + data["msg"],
                f.Width, new Random().Next(f.Height), tempColor));
        }

        private void connect(String mychannel)
        {
            // Initializes a new BayeuxClient
            //string url = "http://112.74.22.182:8080/BubbleServer/cometd";
            string url = "http://112.74.22.182/cometd";
            var options = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            // CometD server socket timeout during connection: 2 minutes
            { ClientTransport.MaxNetworkDelayOption, 120000 }
        };

            using (client = new BayeuxClient(url, new LongPollingTransport(options)))
            {
                // Connects to the Bayeux server
                if (client.Handshake(null, 10000))  // Handshake timeout: 30 seconds
                {
                    // Subscribes to channels
                    IClientSessionChannel channel = client.GetChannel("/" + mychannel);
                    channel.Subscribe(new CallbackMessageListener<BayeuxClient>(OnMessageReceived, client));


                    /*
                    // Publishes to channels
                    var data = new Dictionary<string, string>()
                    {
                        { "sender", "PC" },
                        { "msg","pc text!"}
                    };
                    channel.Publish(data);
                    */
                }
                /*else {
                    MessageBox.Show("Connect failed!");
                    statics = 1;
                }*/
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        //以下为新加的函数
        private void ReplyForCreate(IClientSessionChannel channel, IMessage message, BayeuxClient client)
        {
            IDictionary<String, Object> data = message.DataAsDictionary;
            string state = (string)data["status"];
            string msg = (string)data["message"];
            if (state.Equals("OK"))
            {
                enterSuccess = status.success;
            }
            else
            {
                if (MessageBox.Show(msg + "\nwould you like to enter this channel", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    enterSuccess = status.success;
                else
                    enterSuccess = status.fail;
            }
        }

        private void ReplyForDelete(IClientSessionChannel channel, IMessage message, BayeuxClient client)
        {
            //IDictionary<String, Object> data = message.DataAsDictionary;
            //string state = (string)data["status"];
            //string msg = (string)data["message"];
            //if (state.Equals("OK"))
            //{
            //    enterSuccess = status.success;
            //}
            //else
            //{
            //    if (MessageBox.Show(msg + "\nwould you like to enter this channel", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        enterSuccess = status.success;
            //    else
            //        enterSuccess = status.fail;
            //}
        }

        private void tryToCreate(string mychannel)
        {
            //如果发哥的程序没有跟新就用下面这个调试
            //string url = "http://112.74.22.182:8080/BubbleServer/cometd";
            string url = "http://112.74.22.182/cometd";
            var options = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                // CometD server socket timeout during connection: 2 minutes
                { ClientTransport.MaxNetworkDelayOption, 120000 }
            };

            using (client = new BayeuxClient(url, new LongPollingTransport(options)))
            {
                if (client.Handshake(null, 30000))  // Handshake timeout: 30 seconds
                {
                    IClientSessionChannel channel = client.GetChannel("/service/createRoom");
                    channel.Subscribe(new CallbackMessageListener<BayeuxClient>(ReplyForCreate, client));
                    var data = new Dictionary<string, string>()
                    {
                        {"roomId", mychannel}
                    };
                    channel.Publish(data);
                }
            }
        }

        private void tryToDelete(string mychannel)
        {
            //如果发哥的程序没有跟新就用下面这个调试
            //string url = "http://112.74.22.182:8080/BubbleServer/cometd";
            string url = "http://112.74.22.182/cometd";
            var options = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                // CometD server socket timeout during connection: 2 minutes
                { ClientTransport.MaxNetworkDelayOption, 120000 }
            };

            using (client = new BayeuxClient(url, new LongPollingTransport(options)))
            {
                if (client.Handshake(null, 3000))  // Handshake timeout: 30 seconds
                {
                    IClientSessionChannel channel = client.GetChannel("/service/deleteRoom");
                    channel.Subscribe(new CallbackMessageListener<BayeuxClient>(ReplyForDelete, client));
                    var data = new Dictionary<string, string>()
                    {
                        {"roomId", mychannel}
                    };
                    channel.Publish(data);
                }
            }
        }
    }
}
