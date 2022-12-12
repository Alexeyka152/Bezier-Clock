using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Часы_безье
{
    public partial class BasierClockControl : UserControl
    {
        DateTime dt;
        
        public BasierClockControl()
        {
            ResizeRedraw = true;
            Enabled = false;   
        }

        public DateTime Time
        {
            get
            {
                return dt;
            }
            set
            {
                Graphics grfx = CreateGraphics();
                InitializeCoordinates(grfx);
                Pen pen = new Pen(BackColor);
                if (dt.Hour != value.Hour)
                {
                    DrawHourHand(grfx, pen);
                }
                if (dt.Minute != value.Minute)
                {
                    DrawHourHand(grfx, pen);
                    DrawMinuteHand(grfx, pen);
                }
                if (dt.Second != value.Second)
                {
                    DrawMinuteHand(grfx, pen);
                    DrawSecondHand(grfx, pen);
                }
                if (dt.Millisecond != value.Millisecond)
                {
                    DrawSecondHand(grfx, pen);
                }
                dt = value;
                pen = new Pen(ForeColor);
                DrawHourHand(grfx, pen);
                DrawMinuteHand(grfx, pen);
                DrawSecondHand(grfx, pen);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics grfx = e.Graphics;
            Pen pen = new Pen(ForeColor);
            Brush brush = new SolidBrush(ForeColor);
            InitializeCoordinates(grfx);
            DrawDots(grfx, brush);
            DrawHourHand(grfx, pen);
            DrawMinuteHand(grfx, pen);
            DrawSecondHand(grfx, pen);
        }

        void InitializeCoordinates(Graphics grfx)
        {
            if (Width == 0 || Height == 0)
                return;
            grfx.TranslateTransform(Width / 2, Height / 2);
            float fInches = Math.Min(Width / grfx.DpiX, Height / grfx.DpiY);
            grfx.ScaleTransform(fInches * grfx.DpiX / 2000, fInches * grfx.DpiY / 2000);
        }

        void DrawDots(Graphics grfx, Brush brush)
        {
            for (int i = 0; i < 60; i++)
            {
                int iSize = i % 5 == 0 ? 100 : 30;
                grfx.FillEllipse(brush, 0 - iSize / 2, -900 - iSize / 2, iSize, iSize);
                grfx.RotateTransform(6);
            }
        }

        protected void DrawHourHand(Graphics grfx, Pen pen)
        {
            GraphicsState gs = grfx.Save();
            grfx.RotateTransform(360f * dt.Hour / 12 + 30f * dt.Minute / 60);
            grfx.DrawBeziers(pen, new Point[]
            {
                new Point(0, -600), new Point(0, -300),
                new Point(200, -300), new Point(50, -200),
                new Point(50, -200), new Point(50, 0),
                new Point(50, 0), new Point(50, 75),
                new Point(-50, 75), new Point(-50, 0),
                new Point(-50, 0), new Point(-50, -200),
                new Point(-50, -200), new Point(-200, -300),
                new Point(0, -300), new Point(0, -600),
            });
            grfx.Restore(gs);
        }

        protected void DrawMinuteHand(Graphics grfx, Pen pen)
        {
            GraphicsState gs = grfx.Save();
            grfx.RotateTransform(360f * dt.Minute / 60 + 6f * dt.Second / 60);
            grfx.DrawBeziers(pen, new Point[]
            {
                new Point(0, -800), new Point(0, -750),
                new Point(0, -700), new Point(25, -600),
                new Point(25, -600), new Point(25, 0),
                new Point(25, 0), new Point(25, 50),
                new Point(-25, 50), new Point(-25, 0),
                new Point(-25, 0), new Point(-25, -600),
                new Point(-25, -600), new Point(0, -700),
                new Point(0, -750), new Point(0, -800),
            });
            grfx.Restore(gs);
        }

        protected void DrawSecondHand(Graphics grfx, Pen pen)
        {
            GraphicsState gs = grfx.Save();
            grfx.RotateTransform(360f * dt.Second / 60 + 6f * dt.Millisecond / 1000);
            grfx.DrawLine(pen, 0, 0, 0, -800);
            grfx.Restore(gs);
        }
    }
}