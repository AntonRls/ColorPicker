using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ColorPicker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics colorFillGr;
        async void UpdatePictureBox()
        {

            Rectangle bounds = Screen.GetBounds(Point.Empty);
            float Scalling = GetScalling.GetScalingFactor();
         

            Bitmap mainBitMap = new Bitmap(300, 300);
            Graphics mainGr = Graphics.FromImage(mainBitMap);
         
            while (isDowned)
            {
                int kef = int.Parse(comboBox2.SelectedItem.ToString().Replace("x", ""));
                int Width = 150 / kef;
                int Height = 150 / kef;
                Bitmap bmp = new Bitmap(Width, Height);
   
                Graphics graphics = Graphics.FromImage(bmp);
                graphics.Clear(Color.Gray);
              
                graphics.CopyFromScreen(new Point((int)Math.Round(MousePosition.X*Scalling) - Width/2, (int)Math.Round(MousePosition.Y*Scalling) -Height/2), new Point(0,0), new Size(Width, Height));
                var color =  bmp.GetPixel(Width / 2, Height / 2);
                CurrentColor = color;
                colorFillGr.Clear(color);
                mainGr.DrawImage(new Bitmap(bmp, mainBitMap.Width, mainBitMap.Height), 0, 0);
                mainGr.DrawRectangle(new Pen(Brushes.Red), new Rectangle(mainBitMap.Width/2, mainBitMap.Height/2-5, 1, 10));
                mainGr.DrawRectangle(new Pen(Brushes.Red), new Rectangle(mainBitMap.Width / 2-5, mainBitMap.Height/2, 10, 1));
                pictureBox1.Image = mainBitMap;
                pictureBox2.Image = colorFill;

                bmp.Dispose();
                graphics.Dispose();
                
                await Task.Delay(10);       
            }
        }
        Bitmap colorFill;
        private void Form1_Load(object sender, EventArgs e)
        {

            colorFill = new Bitmap(Width, Height);
            colorFillGr = Graphics.FromImage(colorFill);
            pictureBox2.Image = colorFill;

            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            panel1.BorderStyle = BorderStyle.FixedSingle; 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.SelectedIndex = 0;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        bool isDowned = false;
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            isDowned = true;
            UpdatePictureBox();
        }
        Color _CurrentColor = Color.White;
        Color CurrentColor
        {
            get
            {
                return _CurrentColor;
            }
            set
            {
                _CurrentColor = value;
                trackBar1.Value = value.R;
                trackBar2.Value = value.G;
                trackBar3.Value = value.B;

                UpdateTextFromColor();
            }
        }
       
        void UpdateTextFromColor()
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label1.Text = ColorConverter.RGBConvert(CurrentColor);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                label1.Text = ColorConverter.HexConvert(CurrentColor);
            }
        }
        private void button1_MouseUp(object sender, MouseEventArgs e)
        {

            isDowned = false;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentColor = CurrentColor;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label1.Text);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label_info_1.Text = trackBar1.Value.ToString();
            UpdateColorFromTrackBar();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label_info_2.Text = trackBar2.Value.ToString();
            UpdateColorFromTrackBar();
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            label_info_3.Text = trackBar3.Value.ToString();
            UpdateColorFromTrackBar();
        }

        private void UpdateColorFromTrackBar()
        {
            _CurrentColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            colorFillGr.Clear(Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value));
            pictureBox2.Image = colorFill;
            UpdateTextFromColor();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
