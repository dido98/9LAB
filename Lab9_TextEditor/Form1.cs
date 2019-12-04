using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab9_TextEditor
{
    public partial class Form1 : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        Stack<string> undoList = new Stack<string>();
        bool t = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t = false;
            try
            {
                richTextBox1.Text = undoList.Pop().ToString();
            }
            catch { }
            t = true;
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (t)
            {
                undoList.Push(richTextBox1.Text);
            }
        }
        
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "text Files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFile.ShowDialog() == DialogResult.OK)
                richTextBox1.LoadFile(openFile.FileName, RichTextBoxStreamType.PlainText);
        }
        
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string str = richTextBox1.Text;

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "All Files (*.*)|*.*";
            saveFile.Title = "Save";
            if (saveFile.ShowDialog() == DialogResult.OK && saveFile.FileName.Length > 0)
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                StreamWriter sw = new StreamWriter(saveFile.FileName, false, utf8);
                sw.Write(str);
                sw.Close();
            }
        }
        
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
            richTextBox1.Clear();
        }
        
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }
        
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = Clipboard.GetText();
        }
        
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }
        
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();

            colorDialog1.Color = richTextBox1.SelectionColor;

            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               colorDialog1.Color != richTextBox1.SelectionColor)
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }
        
        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }
        
        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }
        
        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }
        
        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK & !String.IsNullOrEmpty(richTextBox1.Text))
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }
        
        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();

            colorDialog1.Color = richTextBox1.SelectionColor;

            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               colorDialog1.Color != richTextBox1.BackColor)
            {
                richTextBox1.BackColor = colorDialog1.Color;
            }
        }

        private void alignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
