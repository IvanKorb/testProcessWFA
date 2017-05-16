using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace testProcWFA
{
    public partial class Form1 : Form
    {
        Thread thread1;
        public Form1()
        {
            //Process[] proc = Process.GetProcesses();

            InitializeComponent();
            Text = Thread.CurrentThread.Name + "/ id = " + Thread.CurrentThread.ManagedThreadId.ToString() + "/ background = "
                + Thread.CurrentThread.IsBackground.ToString();
            btnStart.Tag = 0;
            thread1 = null;
        }

        
        private void btnStart_Click(object sender, EventArgs e)
        {
            
            if ((int)btnStart.Tag == 0)
            {
                GraphParam gParam = new GraphParam();
                gParam.Canvas = CreateGraphics();
                gParam.x = 10;
                gParam.y = 10;
                gParam.width = 200;
                gParam.heith = 200;
                thread1 = new Thread(ThreadGraphics);
                thread1.IsBackground = true;
                thread1.Start(gParam);
                btnStart.Text = "Pause";
                btnStart.Tag = 1;
            }
            else if ((int)btnStart.Tag == 1) {
                thread1.Suspend();
                btnStart.Text = "Continue";
                btnStart.Tag = 2;
            }
            else
            {
                thread1.Resume();
                btnStart.Text = "Pause";
                btnStart.Tag = 1;
            }
               
        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            
            GraphParam gParam = new GraphParam();
            gParam.Canvas = CreateGraphics();
            gParam.x = 10 + 250;
            gParam.y = 10;
            gParam.width = 200;
            gParam.heith = 200;
            Thread thread1 = new Thread(ThreadGraphics);
            thread1.IsBackground = true;
            thread1.Start(gParam);
        }
        void ThreadRoutine()
        {
            Application.Run(new Form1());
        }
        void ThreadGraphics(object param)
        {
            GraphParam gParam = (GraphParam)param;

            gParam.Canvas.DrawRectangle(new Pen(Color.Black), gParam.x, gParam.y, gParam.width, gParam.heith);

            Random rnd = new Random();
            int x1, x2 = 0;
            int y1, y2 = 0;

            x1 = gParam.x;
            y1 = rnd.Next(gParam.y, gParam.y + gParam.heith);
            int iStep = 0;


            while (true)
            {
                switch (iStep % 4)
                {
                    case 0:
                        x2 = rnd.Next(gParam.x, gParam.x + gParam.width);
                        y2 = gParam.y;
                        break;
                    case 1:
                        x2 = gParam.x + gParam.width;
                        y2 = rnd.Next(gParam.y, gParam.y + gParam.heith);
                        break;
                    case 2:
                        x2 = rnd.Next(gParam.x, gParam.x + gParam.width);
                        y2 = gParam.y + gParam.heith;
                        break;
                    case 3:
                        x2 = gParam.x;
                        y2 = rnd.Next(gParam.y, gParam.y + gParam.heith);
                        break;


                }

                Color col = Color.FromArgb(
                    rnd.Next(0, 255),
                    rnd.Next(0, 255),
                    rnd.Next(0, 255));

                Pen pen = new Pen(col);
                pen.Width = rnd.Next(1, 5);
                gParam.Canvas.DrawLine(pen, x1, y1, x2, y2);
                x1 = x2;
                y1 = y2;

                iStep++;
                Thread.Sleep(10);
                

            }

        }
        class GraphParam
        {
            public Graphics Canvas;
            public int x;
            public int y;
            public int width;
            public int heith;

        }


    }
}
