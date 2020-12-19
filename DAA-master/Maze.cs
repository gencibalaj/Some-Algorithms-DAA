using System;
using System.Collections.Generic;
using System.Drawing;

namespace DAA
{
    public class Maze
    {
        int i;
        int j;
        public Boolean[] muret = { true, true, true, true }; //lart, djathtas, posht, majtas
        Boolean visited = false;

        Pen blackPen = new Pen(Color.Black, 1);

        public Maze(int i, int j) {
            this.i = i;
            this.j = j;
        }

        public int getI() {
            return i;
        }

        public int getJ() {
            return j;
        }
        public Maze kojshit() {
            List<Maze> Kojshit = new List<Maze>();
            Maze top = null;
            Maze right = null;
            Maze bottom = null;
            Maze left = null;

            if (j - 1 >= 0)
            {
                top = Form2.grid[i, j - 1];
            }
            if (i + 1 < Form2.mazeSize)
            {
                right = Form2.grid[i + 1, j];
            }
            if(j+1 < Form2.mazeSize) { 
                bottom = Form2.grid[i, j + 1];
            }if (i - 1 >= 0)
            {
                left = Form2.grid[i - 1, j];
            }

            if (top != null && !top.visited) {
                Kojshit.Add(top);  
            }
            if (right != null && !right.visited)
            {
                Kojshit.Add(right);
            }
            if (bottom != null && !bottom.visited) {
                Kojshit.Add(bottom);
            }
            if (left  != null && !left.visited)
            {
                Kojshit.Add(left);
            }

            if (Kojshit.Count > 0)
            {
                var random = new Random();
                int index = random.Next(Kojshit.Count);
                return Kojshit[index];
            }
            else {
                return null;
            }

        }
        public void visit() {
            this.visited = true;
            int w = Form2.wallSize;
            int x = this.i * w;
            int y = this.j * w;
            Rectangle rect = new Rectangle(x+1, y+1, w-1, w-1);
            Form2.maze.FillRectangle(new SolidBrush(Color.Blue), rect);
        }

        public void visit2()
        {
            int w = Form2.wallSize;
            int x = this.i * w;
            int y = this.j * w;
            Rectangle rect = new Rectangle(x + 1, y + 1, w - 1, w - 1);
            Form2.maze.FillRectangle(new SolidBrush(Color.Green), rect);
        }

        public void DrawCell() {

            int w = Form2.wallSize;
            int x = this.i * w;
            int y = this.j * w;
            Rectangle rect = new Rectangle(x, y, w, w);
            Form2.maze.FillRectangle(new SolidBrush(Color.White), rect);

            if (muret[0])
            {
                DrawLine(new Point(x, y), new Point(x + w, y)); //nalt
            }
            if (muret[1]) { 
                DrawLine(new Point(x+w, y), new Point(x+w, y+w)); //djathtas
            }
            if (muret[2]) { 
                DrawLine(new Point(x + w, y+w), new Point(x, y + w)); //posht
            }
            if (muret[3]) { 
                DrawLine(new Point(x, y + w), new Point(x, y)); //majtas
            }

        }

        private void DrawLine(Point Point1, Point Point2)
        {
            Form2.maze.DrawLine(blackPen, Point1.X, Point1.Y, Point2.X, Point2.Y);
        }

    }
}
