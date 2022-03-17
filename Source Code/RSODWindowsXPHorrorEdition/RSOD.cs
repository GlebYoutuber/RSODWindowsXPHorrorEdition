using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Reflection;
using System.Threading;

namespace RSODWindowsXPHorrorEdition
{
    public partial class RSOD : Form
    {
        SoundPlayer rsod;
        public void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
        {
            //This is Very Important Code... DON'T CHANGE THIS!!! 

            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
                w.Write(r.ReadBytes((int)s.Length));
        }
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public RSOD()
        {
            Directory.CreateDirectory(@"C:\Temp");
            Extract("RSODWindowsXPHorrorEdition", @"C:\Temp", "Resources", "RSOD.wav");
            InitializeComponent();
            this.Text = RandomString(45);
            pictureBox1.Dock = DockStyle.Fill;
            this.Show();
            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height;
            pictureBox1.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
            pictureBox1.Top = Screen.PrimaryScreen.Bounds.Height - this.Height;
            rsod = new SoundPlayer(@"C:\Temp\RSOD.wav");
            rsod.PlayLooping();
            Cursor.Hide();
            this.BackColor = Color.DarkRed;
        }

        private void RSOD_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Thread.Sleep(6000);
            this.Hide();
            rsod.Stop();
            Environment.Exit(0);
            timer1.Start();
        }
    }
}
