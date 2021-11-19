using DLLInjection;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loader
{
    public partial class Encryptedname : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        FontFamily font_never;

        private void Font_never()
        {
            PrivateFontCollection new_Font = new PrivateFontCollection();
            int long_font = Properties.Resources.MuseoSansCyrl_900.Length;
            byte[] fontdata = Properties.Resources.MuseoSansCyrl_900;
            IntPtr replace = Marshal.AllocCoTaskMem(long_font);
            Marshal.Copy(fontdata, 0, replace, long_font);
            uint cFonts = 0;
            AddFontMemResourceEx(replace, (uint)fontdata.Length, IntPtr.Zero, ref cFonts);
            new_Font.AddMemoryFont(replace, long_font);
            Marshal.FreeCoTaskMem(replace);
            font_never = new_Font.Families[0];
        }

        Point lastPoint;

        public Encryptedname()
        {
            InitializeComponent();
            // change form name to randomly generated string
            Random rnd = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            this.Text = new string(Enumerable.Repeat(chars, rnd.Next(10, 20)).Select(s => s[rnd.Next(s.Length)]).ToArray());
            // create rounded form borders
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 2, 2));
            // activate and change font to custom one from project resources
            Font_never();
            label1.Font = new Font(font_never, 10, FontStyle.Bold);
            label2.Font = new Font(font_never, 10, FontStyle.Bold);
            // welcome message
            label2.Text = $"Welcome, {Environment.UserName}!";
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            // this will kill our loader process
            Process.GetCurrentProcess().Kill();
        }

        private void Encryptedname_Load(object sender, EventArgs e)
        {
            // create webclient
            WebClient client = new WebClient();
            // current status = online cause your hack works - if no then you should change it to offline
            label3.Text = $"Status: {client.DownloadString("https://pastebin.com/raw/iQuqytCu")}";
            // you have to set up expiry date as you need
            label4.Text = "Expires: Never";
            // last update (this is date when i started to do this loader so change it to yours aswell)
            label5.Text = $"Last update: {client.DownloadString("https://pastebin.com/raw/AmspuJT3")}";
        }

        private void load_Click(object sender, EventArgs e)
        {
            // if you want add more functions go ahead
            // i just show you how you can inject
            /*
            string hack = "enter dll path here";
             */
            /*
            string choosenprocess = "enter process name here";
             */
            // 1. if you want loadlibrarya use this
            /*
            Inject.InjectMethod.Inject(hack, choosenprocess);
             */
            // 2. if you want manualmap use this
            /*
            var processes = Process.GetProcessesByName(choosenprocess);
            foreach (var process in processes)
            {
                int pid = process.Id;
                var injectionMethod = InjectionMethod.NT_CREATE_THREAD_EX;
                var injector = new DLLInjector(injectionMethod);
                injector.Inject(pid, hack);
            }
             */
            // 3. if you want native use this
            /*
            var name = choosenprocess;
            var target = Process.GetProcessesByName(name).FirstOrDefault();
            ManualMapInjector manualmap = new ManualMapInjector(target);
            manualmap.AsyncInjection = true;
            {
                manualmap.Inject(File.ReadAllBytes(hack));
            }
             */
            // 4. if you want reflective use this
            /*
            Reflective.Run(hack, choosenprocess);
             */
            // dll injection requires admin permissions so just run application as admin - or i do recommend to create manifest file
        }
    }
}
