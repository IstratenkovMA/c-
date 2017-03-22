using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brainstorm
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        static List<String> looseFrases = new List<String>();
        static int level = 1;
        Fild fild = new Fild(@"..\..\Levels\" + level.ToString() + ".txt");
        public Form1()
        {
            InitializeComponent();
        }
        
        public void ChangeLevel()
        {
            if(level == 6)
            {
                graphics.Clear(Color.LightBlue);
                fild = new Fild();
                graphics.DrawString("CONGRATULATIONS!\n   YOU BEAT\n   THE GAME!",
                new Font("Cooper Black", 50), new SolidBrush(Color.DarkGoldenrod), new PointF(0, 100));
                level = 1;
                return;
            }
            level++;
            fild = new Fild(@"..\..\Levels\" + level.ToString() + ".txt");
            fild.drawFild(graphics);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                case Keys.Enter:
                    if (fild.isBlocked())
                    {
                        if (fild.isWinner())
                        {
                            ChangeLevel();
                            break;
                        }
                    }
                    level--;
                    ChangeLevel();
                    break;

                case Keys.Down:
                    fild.MoveDown();
                    fild.drawFild(graphics);
                    break;
                case Keys.Right:
                    fild.MoveRight();
                    fild.drawFild(graphics);
                    break;
                case Keys.Up:
                    fild.MoveUp();
                    fild.drawFild(graphics);
                    break;
                case Keys.Left:
                    fild.MoveLeft();
                    fild.drawFild(graphics);
                    break;
            }
            if (fild.isBlocked())
            {
                if (fild.isWinner())
                    showWin();
                else
                    showLoose();
            }
        }

        public void showWin()
        {
            // Create string to draw.
            String drawString = level.ToString() + " LEVEL COMPLITE!";

            // Create font and brush.
            Font drawFont = new Font("Arial", 40);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(100.0F, 20);

            // Draw string to screen.
            graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
            graphics.DrawString("exit = esc\nnext level or restart\npress spacebar\nor enter",
                new Font("Arial", 28), new SolidBrush(Color.DarkGreen), new PointF(470.0F, 200));
        }

        public void showLoose()
        {
            // Create string to draw.
            Random rand = new Random();
            
            String drawString = looseFrases[rand.Next() % looseFrases.Count];

            // Create font and brush.
            Font drawFont = new Font("Arial", 40);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(100.0F, 20);

            // Draw string to screen.
            graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int x = e.Location.X;
            int y = e.Location.Y;
            fild = new Fild(@"..\..\Levels\" + level.ToString() + ".txt");
            fild.SetCursor(x, y, graphics);
            fild.drawFild(graphics);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            graphics = this.CreateGraphics();
            looseFrases.Add("Where did I go wrong?..");
            looseFrases.Add("Deadlock again?..");
            looseFrases.Add("It's so simple!");
            fild = new Fild(@"..\..\Levels\" + level.ToString() + ".txt");
            fild.drawFild(graphics);
        }
    }
}
