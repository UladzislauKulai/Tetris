using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris.Controllers
{
    public static class MapController
    {
        public static Shape currentShape;
        public static int[,] map = new int[20, 10];
        public static int size;
        public static int lines;
        public static int score;
        public static int interval;
        public static void ShowNextShape(Graphics e)
        {
            for (int i = 0; i < currentShape.sizeNextMatrix; i++)
            {
                for (int a = 0; a < currentShape.sizeNextMatrix; a++)
                {
                    if (currentShape.nextMatrix[i, a] == 1)
                    {
                        e.FillRectangle(Brushes.Green, new Rectangle(360 + a*(size) + 1, 50 + i*(size) + 1, size - 1, size - 1));
                    }
                    if (currentShape.nextMatrix[i, a] == 2)
                    {
                        e.FillRectangle(Brushes.Yellow, new Rectangle(360 + a*(size) + 1, 50 + i*(size) + 1, size - 1, size - 1));
                    }
                    if (currentShape.nextMatrix[i, a] == 3)
                    {
                        e.FillRectangle(Brushes.Orange, new Rectangle(360 + a*(size) + 1, 50 + i*(size) + 1, size - 1, size - 1));
                    }
                    if (currentShape.nextMatrix[i, a] == 4)
                    {
                        e.FillRectangle(Brushes.Blue, new Rectangle(360 + a*(size) + 1, 50 + i*(size) + 1, size - 1, size - 1));
                    }
                    if (currentShape.nextMatrix[i, a] == 5)
                    {
                        e.FillRectangle(Brushes.Brown, new Rectangle(360 + a*(size) + 1, 50 + i*(size) + 1, size - 1, size - 1));
                    }
                }
            }
        }

        public static void ClearMap()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int a = 0; a < 10; a++)
                {
                    map[i, a] = 0;
                }
            }
        }

        public static void DrawMap(Graphics e)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int a = 0; a < 10; a++)
                {
                    if (map[i, a] == 1)
                    {
                        e.FillRectangle(Brushes.Green, new Rectangle(50 + a*(size) + 1, 50 + i*(size) + 1, size - 1, size - 1));
                    }
                    if (map[i, a] == 2)
                    {
                        e.FillRectangle(Brushes.Yellow, new Rectangle(50 + a*(size) + 1, 50 + i*(size) + 1, size - 1, size - 1));
                    }
                    if (map[i, a] == 3)
                    {
                        e.FillRectangle(Brushes.Orange, new Rectangle(50 + a*(size) + 1, 50 + i*(size) + 1, size - 1, size - 1));
                    }
                    if (map[i, a] == 4)
                    {
                        e.FillRectangle(Brushes.Blue, new Rectangle(50 + a*(size) + 1, 50 + i*(size) + 1, size - 1, size - 1));
                    }
                    if (map[i, a] == 5)
                    {
                        e.FillRectangle(Brushes.Brown, new Rectangle(50 + a*(size)+ 1, 50 + i*(size) + 1, size - 1, size - 1));
                    }
                }
            }
        }

        public static void DrawGrid(Graphics g)
        {
            for (int i = 0; i <= 20; i++)
            {
                g.DrawLine(Pens.Black, new Point(50, 50 + i * size), new Point(50 + 10 * size, 50 + i * size));
            }
            for (int i = 0; i <= 10; i++)
            {
                g.DrawLine(Pens.Black, new Point(50 + i * size, 50), new Point(50 + i * size, 50 + 20* size));
            }
        }

        public static void SliceMap(Label label1,Label label2)
        {
            int count = 0;
            int curlines = 0;
            for (int i = 0; i < 20; i++)
            {
                count = 0;
                for (int a = 0; a < 10; a++)
                {
                    if (map[i, a] != 0)
                        count++;
                }
                if (count == 10)
                {
                    curlines++;
                    for (int k = i; k >= 1; k--)
                    {
                        for (int o = 0; o < 10; o++)
                        {
                            map[k, o] = map[k - 1, o];
                        }
                    }
                }
            }
            for (int i = 0; i < curlines; i++)
            {
                score += 10 * (i + 1);
            }
            lines += curlines;

            if (lines % 5 == 0)
            {
                if (interval > 60)
                    interval -= 10;
            }

            label1.Text = "Score: " + score;
            label2.Text = "Lines: " + lines;
        }

        public static bool IsIntersects()
        {
            for (int i = currentShape.y; i < currentShape.y + currentShape.sizeMatrix; i++)
            {
                for (int a = currentShape.x; a < currentShape.x + currentShape.sizeMatrix; a++)
                {
                    if (a >= 0 && a <= 9)
                    {
                        if (map[i, a] != 0 && currentShape.matrix[i - currentShape.y, a - currentShape.x] == 0)
                            return true;
                    }
                }
            }
            return false;
        }

        public static void Merge()
        {
            for (int i = currentShape.y; i < currentShape.y + currentShape.sizeMatrix; i++)
            {
                for (int a = currentShape.x; a < currentShape.x + currentShape.sizeMatrix; a++)
                {
                    if (currentShape.matrix[i - currentShape.y, a - currentShape.x] != 0)
                        map[i, a] = currentShape.matrix[i - currentShape.y, a - currentShape.x];
                }
            }
        }

        public static bool Collide()
        {
            for (int i = currentShape.y + currentShape.sizeMatrix - 1; i >= currentShape.y; i--)
            {
                for (int a = currentShape.x; a < currentShape.x + currentShape.sizeMatrix; a++)
                {
                    if (currentShape.matrix[i - currentShape.y, a - currentShape.x] != 0)
                    {
                        if (i + 1 == 20)
                            return true;
                        if (map[i + 1, a] != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool CollideHor(int dir)
        {
            for (int i = currentShape.y; i < currentShape.y + currentShape.sizeMatrix; i++)
            {
                for (int a = currentShape.x; a < currentShape.x + currentShape.sizeMatrix; a++)
                {
                    if (currentShape.matrix[i - currentShape.y, a - currentShape.x] != 0)
                    {
                        if (a + 1 * dir > 9 || a + 1 * dir < 0)
                            return true;

                        if (map[i, a + 1 * dir] != 0)
                        {
                            if (a - currentShape.x + 1 * dir >= currentShape.sizeMatrix || a - currentShape.x + 1 * dir < 0)
                            {
                                return true;
                            }
                            if (currentShape.matrix[i - currentShape.y, a - currentShape.x + 1 * dir] == 0)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void ResetArea()
        {
            for (int i = currentShape.y; i < currentShape.y + currentShape.sizeMatrix; i++)
            {
                for (int a = currentShape.x; a < currentShape.x + currentShape.sizeMatrix; a++)
                {
                    if (i >= 0 && a >= 0 && i < 20 && a < 10)
                    {
                        if (currentShape.matrix[i - currentShape.y, a - currentShape.x] != 0)
                        {
                            map[i, a] = 0;
                        }
                    }
                }
            }
        }

    }
}
