using Binarysharp.MemoryManagement;
using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineClicker
{
    public partial class Form1 : Form
    {
        MemorySharp sharp = null;
        public Process process = null;
        public Form1()
        {
            InitializeComponent();
        }
        private IKeyboardMouseEvents m_GlobalHook;
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        static public System.Drawing.Color GetPixelColor(IntPtr hwnd, int x, int y)
        {
            IntPtr hdc = GetDC(hwnd);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(hwnd, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                            (int)(pixel & 0x0000FF00) >> 8,
                            (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in Enum.GetValues(typeof(Keys)))
            {
                comboBox1.Items.Add(item);
                comboBox2.Items.Add(item);
            }
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.KeyDown += KeyDown;
            m_GlobalHook.KeyUp += KeyUp;
            m_GlobalHook.MouseDown += MouseDown;
            m_GlobalHook.MouseUp += MouseUp;
            new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    if (sharp != null)
                    {
                        if (sharp.IsRunning)
                        {
                            var window = sharp.Windows.MainWindow;
                            if (window != null)
                            {
                                if (LMBon)
                                {
                                    if (window.IsActivated)
                                    {

                                        window.PostMessage(Binarysharp.MemoryManagement.Native.WindowsMessages.LButtonDown, UIntPtr.Zero, UIntPtr.Zero);
                                        window.PostMessage(Binarysharp.MemoryManagement.Native.WindowsMessages.LButtonUp, UIntPtr.Zero, UIntPtr.Zero);
                                        Thread.Sleep(1000 / int.Parse(textBox2.Text));
                                    }
                                }
                            }
                        }
                    }
                }
            })).Start();
            new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    if (sharp != null)
                    {
                        if (sharp.IsRunning)
                        {
                            var window = sharp.Windows.MainWindow;
                            if (window != null)
                            {
                                if (RMBon)
                                {
                                    if (window.IsActivated)
                                    {
                                        window.PostMessage(Binarysharp.MemoryManagement.Native.WindowsMessages.RButtonDown, UIntPtr.Zero, UIntPtr.Zero);
                                        window.PostMessage(Binarysharp.MemoryManagement.Native.WindowsMessages.RButtonUp, UIntPtr.Zero, UIntPtr.Zero);
                                        Thread.Sleep(1000 / int.Parse(textBox1.Text));
                                    }
                                }
                            }
                        }
                    }
                }
            })).Start();
            new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    if (sharp != null)
                    {
                        if (sharp.IsRunning)
                        {
                            var window = sharp.Windows.MainWindow;
                            if (window != null)
                            {
                                if (AutoSprint)
                                {
                                    if (window.IsActivated)
                                    {
                                        window.Keyboard.Press(Binarysharp.MemoryManagement.Native.Keys.LControlKey);
                                        Thread.Sleep(15);
                                        window.Keyboard.Release(Binarysharp.MemoryManagement.Native.Keys.LControlKey);
                                    }
                                }
                            }
                        }
                    }
                }
            })).Start();
        }
        bool AutoSprint = false;
        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Keys key = Keys.None;
                Enum.TryParse(comboBox1.Text, out key);
                if (e.KeyCode == key)
                {
                    LMBon = true;
                }
            }
            if (checkBox2.Checked == true)
            {
                Keys key = Keys.None;
                Enum.TryParse(comboBox1.Text, out key);
                if (e.KeyCode == key)
                {
                    RMBon = true;
                }
            }
            if (checkBox3.Checked == true)
            {
                if (e.KeyCode == Keys.W)
                {
                    AutoSprint = true;
                }
            }
        }
        private void KeyUp(object sender, KeyEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Keys key = Keys.None;
                Enum.TryParse(comboBox1.Text, out key);
                if (e.KeyCode == key)
                {
                    LMBon = false;
                }
            }
            if (checkBox2.Checked == true)
            {
                Keys key = Keys.None;
                Enum.TryParse(comboBox2.Text, out key);
                if (e.KeyCode == key)
                {
                    RMBon = false;
                }
            }
            if (checkBox3.Checked == true)
            {
                if (e.KeyCode == Keys.W)
                {
                    AutoSprint = false;
                }
            }
        }
        private void MouseDown(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                MouseButtons key = MouseButtons.None;
                Enum.TryParse(comboBox1.Text, out key);
                if (e.Button == key)
                {
                    LMBon = true;
                }
            }
            if (checkBox2.Checked == true)
            {
                MouseButtons key = MouseButtons.None;
                Enum.TryParse(comboBox2.Text, out key);
                if (e.Button == key)
                {
                    RMBon = true;
                }
            }
        }
        private void MouseUp(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                MouseButtons key = MouseButtons.None;
                Enum.TryParse(comboBox1.Text, out key);
                if (e.Button == key)
                {
                    LMBon = false;
                }
            }
            if (checkBox2.Checked == true)
            {
                MouseButtons key = MouseButtons.None;
                Enum.TryParse(comboBox2.Text, out key);
                if (e.Button == key)
                {
                    RMBon = false;
                }
            }
        }
        bool LMBon = false;
        bool RMBon = false;

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sharp != null)
            {
                sharp.Dispose();
            }
            if (m_GlobalHook != null)
            {
                m_GlobalHook.Dispose();
            }
            Environment.Exit(0);
        }

        private void acaciaButton1_Click(object sender, EventArgs e)
        {
            process = Process.GetProcessesByName(procc.Text).FirstOrDefault();
            if (process != null)
            {
                sharp = new MemorySharp(process);
                process.Exited += Process_Exited;
                label6.Text = "Window: " + sharp.Windows.MainWindow.Title;
            }
            else
            {
                label6.Text = "Window: null";
            }
        }
        private void Process_Exited(object sender, EventArgs e)
        {
            label6.Text = "Window: null";
        }

        private void acaciaButton2_Click(object sender, EventArgs e)
        {
            if (process != null)
            {
                process.Kill();
                label6.Text = "Window: null";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox3.Checked) { AutoSprint = false;  }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void acaciaSkin1_Click(object sender, EventArgs e)
        {

        }
    }
}
