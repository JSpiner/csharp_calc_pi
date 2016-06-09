using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace csharp_pi
{
    public partial class Form1 : Form
    {

        int R = 100;
        Pen pen = new Pen(Brushes.DeepSkyBlue, 5);
        List<Pos> posList;
        Random rnd = new Random();

        class Pos{
            public int x;
            public int y;

            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Bitmap canvas = new Bitmap(500, 500);
            pictureBox1.Image = canvas;

            Graphics g = Graphics.FromImage(canvas);

            g.DrawEllipse(Pens.Black, new Rectangle(0, 0, 200, 200));
            g.DrawLine(Pens.Black, new Point(100, 0), new Point(100, 200));
            g.DrawLine(Pens.Black, new Point(0, 100), new Point(200, 100));

            posList = new List<Pos>();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Pos pos = new Pos(
                rnd.Next(0, R * 2), 
                rnd.Next(0, R * 2));

            drawPos(pos);

            posList.Add(pos);

            calcPI();
        }

        void calcPI()
        {

            int count = 0;
            int i;
            for (i = 0; i < posList.Count;i++ )
            {
                double distance =
                    Math.Sqrt(
                        Math.Pow(posList[i].x - R, 2) +
                        Math.Pow(posList[i].y - R, 2)
                    );
                if (distance <= R) count++;
            }
            label1.Text = "PI : " + ((double) count / posList.Count * 4); 
            /*
             원 : 사각형 = PI * r * r : 2r * 2r = PI : 4
             PI = 4 * 원안의 갯수 / 전체 갯수 
              
             */ 
            label2.Text = "원 안의 점 수 : " + count + "\n"
                            + "사각형 안의 점 수 : " + posList.Count;

        }

        void drawPos(Pos pos)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);

            g.DrawRectangle(Pens.Black, new Rectangle(pos.x / (R / 100) ,pos.y / (R / 100), 2,2));

            pictureBox1.Invalidate();
        }
    }
}
