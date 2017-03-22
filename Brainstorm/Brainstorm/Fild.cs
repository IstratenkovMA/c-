using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstorm
{
    class Fild
    {
        List<List<Cell>> fild = new List<List<Cell>>();
        int x = -1; // position of our cursor
        int y = -1; // sets up by player

        int slide = 100;
        int size = 70;
        int splitter = 5;

        Bitmap leftArrow = new Bitmap(@"..\..\Resources\leftArrow.png");
        Bitmap rightArrow = new Bitmap(@"..\..\Resources\rightArrow.png");
        Bitmap upArrow = new Bitmap(@"..\..\Resources\upArrow.png");
        Bitmap downArrow = new Bitmap(@"..\..\Resources\downArrow.png");

        Bitmap empty = new Bitmap(@"..\..\Resources\empty.png");
        Bitmap block = new Bitmap(@"..\..\Resources\block.png");
        Bitmap painted = new Bitmap(@"..\..\Resources\painted.png");

        public Fild()
        {
            for(int i = 0; i < 5; i++)
            {
                List<Cell> a = new List<Cell>();
                for (int j = 0; j < 5; j++)
                    a.Add(new Cell());
                fild.Add(a);
            }
        }

        public Fild(String filePath)
        {
            for (int i = 0; i < 5; i++)
            {
                List<Cell> a = new List<Cell>();
                for (int j = 0; j < 5; j++)
                    a.Add(new Cell());
                fild.Add(a);
            }
            //SimpleGraph graph = new SimpleGraph(@"..\..\Simple.txt", true);
            string[] lines = File.ReadAllLines(filePath);
            for(int i = 0; i < lines.Length; i++)
            {
                fild[int.Parse(lines[i].Split(' ')[0])]
                    [int.Parse(lines[i].Split(' ')[1])].blockCell();
            }
        }

        public void drawFild(Graphics graphics)
        {
            graphics.Clear(Color.LightBlue);
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    Rectangle area = new Rectangle( slide + i * (size + splitter), slide + j * (size + splitter), size, size);
                    if (fild[j][i].getType().Equals("empty"))
                    {
                        graphics.DrawImage(empty, area);
                    }
                    else if (fild[j][i].getType().Equals("painted"))
                    {
                        graphics.DrawImage(painted, area);
                    }
                    else if (fild[j][i].getType().Equals("rightArrow"))
                    {
                        graphics.DrawImage(rightArrow, area);
                    }
                    else if (fild[j][i].getType().Equals("leftArrow"))
                    {
                        graphics.DrawImage(leftArrow, area);
                    }
                    else if (fild[j][i].getType().Equals("upArrow"))
                    {
                        graphics.DrawImage(upArrow, area);
                    }
                    else if (fild[j][i].getType().Equals("downArrow"))
                    {
                        graphics.DrawImage(downArrow, area);
                    }
                    else if (fild[j][i].getType().Equals("block"))
                    {
                        graphics.DrawImage(block, area);
                    }
                }
        }

        /// <summary>
        /// Shows the game result: is player
        /// Win, Loose when game is over
        /// </summary>
        /// <returns>true when player win. otherwise false</returns>
        public bool isWinner()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (!fild[i][j].isBlocker())
                        return false;
                }
            return true;
        }

        public bool isBlocked()
        {
            if (CanMoveDown() || CanMoveUp() || CanMoveLeft() || CanMoveRight())
                return false;
            return true;
        }

        public void SetCursor(int x, int y, Graphics graphics)
        {
            x = x - slide;
            y = y - slide;

            x = x / (size + splitter);
            y = y / (size + splitter);
            
            if(y >= 0 && y < 5 && x >= 0 && x < 5)
            {
                if(!fild[y][x].getType().Equals("block"))
                {
                    this.x = y;
                    this.y = x;
                    fild[y][x].paintCell();
                }
            }
            drawArrows();
        }
        
        private bool CanMoveUp()
        {
            if (x < 0 || y < 0)
                return true;
            if (x - 1 < 0)
                return false;
            return !fild[x - 1][y].isBlocker();
        }

        private bool CanMoveLeft()
        {
            if (x < 0 || y < 0)
                return true;
            if (y - 1 < 0)
                return false;
            return !fild[x][y - 1].isBlocker();
        }

        private bool CanMoveRight()
        {
            if (x < 0 || y < 0)
                return true;
            if (y + 1 >= 5)
                return false;
            return !fild[x][y + 1].isBlocker();
        }

        private bool CanMoveDown()
        {
            if (x < 0 || y < 0)
                return true;
            if (x + 1 >= 5)
                return false;
            return !fild[x + 1][y].isBlocker();
        }

        private void deleteArrows()
        {
            if (x < 0 || y < 0)
                return;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (fild[i][j].isArrow())
                    {
                        fild[i][j].emptyCell();
                    }
        }

        private void drawArrows()
        {
            if (x < 0 || y < 0)
                return;
            if (CanMoveDown())
            {
                fild[x + 1][y].arrowCell("down");
            }
            if (CanMoveUp())
            {
                fild[x - 1][y].arrowCell("up");
            }
            if (CanMoveLeft())
            {
                fild[x][y - 1].arrowCell("left");
            }
            if (CanMoveRight())
            {
                fild[x][y + 1].arrowCell("right");
            }
        }

        public void MoveRight()
        {
            if (x < 0 || y < 0)
                return;
            fild[x][y].paintCell();
            if (CanMoveRight())
            {
                y++;
                MoveRight();
            }
            else
            {
                deleteArrows();
                drawArrows();
            }
        }

        public void MoveLeft()
        {
            if (x < 0 || y < 0)
                return;
            fild[x][y].paintCell();
            if (CanMoveLeft())
            {
                y--;
                MoveLeft();
            }
            else
            {
                deleteArrows();
                drawArrows();
            }
        }

        public void MoveUp()
        {
            if (x < 0 || y < 0)
                return;
            fild[x][y].paintCell();
            if (CanMoveUp())
            {
                x--;
                MoveUp();
            }
            else
            {
                deleteArrows();
                drawArrows();
            }
        }

        public void MoveDown()
        {
            if (x < 0 || y < 0)
                return;
            fild[x][y].paintCell();
            if (CanMoveDown())
            {
                x++;
                MoveDown();
            }
            else
            {
                deleteArrows();
                drawArrows();
            }
        }
    }
}
