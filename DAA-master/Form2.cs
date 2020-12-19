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

namespace DAA
{
   public partial class Form2 : Form
    {
        public static Graphics maze;
        public static int wallSize;

        Stack<Maze> steku = new Stack<Maze>();
 
        public static int mazeSize = 20;
        
        private Boolean perfundoj = false;

        Maze current;

        public static Maze[,] grid;
        


        public Form2()
        {
            InitializeComponent();
            
        }

        private void incializimi() {
            grid = new Maze[mazeSize, mazeSize];
            for (int i = 0; i < mazeSize; i++)
            {
                for (int j = 0; j < mazeSize; j++)
                {
                    grid[i, j] = new Maze(i, j);
                }
            }
            current = grid[0, 0];
            wallSize = Convert.ToInt32(label1.Width / mazeSize);
            Console.WriteLine(wallSize);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            maze = label1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                mazeSize = int.Parse(textBox1.Text);
            }
            else {
                textBox1.Text = mazeSize+"";
            }

            incializimi();

            for (int i = 0; i < mazeSize; i++)
            {
                for (int j = 0; j < mazeSize; j++)
                {
                    grid[i, j].DrawCell();
                }
            }

            current.visit();

            while (!perfundoj)
            {
                draw();
            }
        }


        public void draw() {
            Maze next = current.kojshit();
            if (next != null)
            {
                steku.Push(current);
                RemoveWall(current, next);
                current = next;
                current.visit();
            }
            else if (steku.Count > 0)
            {
                current.DrawCell();
                current = steku.Pop();
                current.visit2();
            }
            else {
                perfundoj = true;
            }
            Thread.Sleep(100);
        }

        public void RemoveWall(Maze a, Maze b) {
            int x = a.getI() - b.getI();

            if (x == 1)
            {
                a.muret[3] = false;
                b.muret[1] = false;
                a.DrawCell();
                b.DrawCell();
            }
            else if (x == -1) {
                a.muret[1] = false;
                b.muret[3] = false;
                a.DrawCell();
                b.DrawCell();
            }

            int y = a.getJ() - b.getJ();

            if (y == 1)
            {
                a.muret[0] = false;
                b.muret[2] = false;
                a.DrawCell();
                b.DrawCell();
            }
            else if (y == -1) {
                a.muret[2] = false;
                b.muret[0] = false;
                a.DrawCell();
                b.DrawCell();
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);   
        }
    }
}
