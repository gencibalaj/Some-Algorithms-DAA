using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DAA
{
    public partial class Form1 : Form
    {
        Point _coordinates;
        Graphics g;
        Boolean mode = true;
        int NrNyje = 0;
        //int selected = 0
        List<Point> selectedNyjet = new List<Point>();
        List<int> selectedNyjet2 = new List<int>();
        List<Point> nyjet = new List<Point>();
        List<int[]> lidhjet = new List<int[]>();
        Grafi grafi = new Grafi();
        public Form1()
        {
            _coordinates.X = 0;
            _coordinates.Y = 0;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = label1.CreateGraphics();
        }




        private void label1_Click(object sender, EventArgs e)
        {
            Point x = label1.PointToClient(Cursor.Position);
            if (mode)
            {

                if (ShouldIAdd(x))
                {
                    Console.WriteLine(x.X + "  " + x.Y);
                    nyjet.Add(x);
                    Pen blackPen = new Pen(Color.Black, 1);
                    Rectangle rect = new Rectangle(x.X - 25, x.Y - 25, 50, 50);
                    g.DrawEllipse(blackPen, rect);
                    g.DrawString((NrNyje + 1) + "", new System.Drawing.Font("Arial", 16),
                        new System.Drawing.SolidBrush(System.Drawing.Color.Black), new Point(x.X - 10, x.Y - 10));
                    grafi.ShtoNyje();
                    NrNyje++;
                    

                }
                else {
                    Console.WriteLine("EKZISTON");
                }
            }
            else {
                //Console.WriteLine(whichCircle(x));
                
                int NyjaId = whichCircle(x);
                if (NyjaId != -1) {
                    Point Nyja = nyjet[whichCircle(x)];
                    selectedNyjet.Add(Nyja);
                    selectedNyjet2.Add(NyjaId);
                    DrawCircle(Color.Red, Nyja, NyjaId);

                    if (selectedNyjet.Count == 2)
                    {
                        int[] lidhja = { selectedNyjet2[0], selectedNyjet2[1] };
                        lidhjet.Add(lidhja);

                        grafi.shtoDege(selectedNyjet2[0], selectedNyjet2[1]);

                        DrawLine(Color.Black, selectedNyjet[0], selectedNyjet[1]);
                        selectedNyjet.Clear();
                        selectedNyjet2.Clear();
                        DrawAll();
                    }
                }
            }
        }

        private void DrawAll(){
            g.Clear(Color.White);
            for(int i = 0; i < nyjet.Count; i++)
            {
                DrawCircle(Color.Black, nyjet[i], i);
            }
            for (int j = 0; j < lidhjet.Count; j++) {
                DrawLine(Color.Black, nyjet[lidhjet[j][0]], nyjet[lidhjet[j][1]]);
            }
        }

        private void DrawAll(List<Point> nyjet, List<int []> lidhjet)
        {
            g.Clear(Color.White);
            for (int i = 0; i < nyjet.Count; i++)
            {
                DrawCircle(Color.Green, nyjet[i], i);
            }
            for (int j = 0; j < lidhjet.Count; j++)
            {
                DrawLine(Color.Green, nyjet[lidhjet[j][0]], nyjet[lidhjet[j][1]]);
            }
        }


        private Boolean ShouldIAdd(Point x) {
            for (int i = 0; i < nyjet.Count; i++) {
                if (InsideCircle2(nyjet[i], x) || OutsideForm(x)) {
                    return false;
                }
            }
            return true;
        }


        private Point[] newPoints(Point P1, Point P2) {
            double m = (P2.Y - P1.Y) / (P2.X - P1.X+0.001);
            double angle = Math.Atan(m);
            double deltaX = Math.Abs(25 * Math.Cos(angle));
            Console.WriteLine("DELTA X:"+deltaX);
            double deltaY = Math.Abs(25 * Math.Sin(angle));
            Point NP1 = P1;
            Point NP2 = P2;

            if (P1.X < P2.X && P1.Y < P2.Y)
            {
                NP1 = new Point(Convert.ToInt32(P1.X + deltaX), Convert.ToInt32(P1.Y + deltaY));
                NP2 = new Point(Convert.ToInt32(P2.X - deltaX), Convert.ToInt32(P2.Y - deltaY));
            }
            else if (P1.X < P2.X && P1.Y > P2.Y) {
                NP1 = new Point(Convert.ToInt32(P1.X + deltaX), Convert.ToInt32(P1.Y - deltaY));
                NP2 = new Point(Convert.ToInt32(P2.X - deltaX), Convert.ToInt32(P2.Y + deltaY));
            }
            else if (P1.X > P2.X && P1.Y < P2.Y)
            {
                NP1 = new Point(Convert.ToInt32(P1.X - deltaX), Convert.ToInt32(P1.Y + deltaY));
                NP2 = new Point(Convert.ToInt32(P2.X + deltaX), Convert.ToInt32(P2.Y - deltaY));
            }
            else if (P1.X > P2.X && P1.Y > P2.Y)
            {
                NP1 = new Point(Convert.ToInt32(P1.X - deltaX), Convert.ToInt32(P1.Y - deltaY));
                NP2 = new Point(Convert.ToInt32(P2.X + deltaX), Convert.ToInt32(P2.Y + deltaY));
            }

            Point[] Result = { NP1, NP2 };
            return Result;
        }

        private void DrawLine(Color c, Point Point1, Point Point2) {
            Point[] newP = newPoints(Point1, Point2);
            Pen blackPen = new Pen(c, 1);
            g.DrawLine(blackPen, newP[0].X, newP[0].Y, newP[1].X, newP[1].Y);
        }
        private void DrawCircle(Color c, Point location, int NyjeId) {
            Pen blackPen = new Pen(c, 1);
            Rectangle rect = new Rectangle(location.X - 25, location.Y - 25, 50, 50);
            g.DrawEllipse(blackPen, rect);
            g.DrawString((NyjeId + 1) + "", new System.Drawing.Font("Arial", 16),
                        new System.Drawing.SolidBrush(System.Drawing.Color.Black), new Point(location.X - 10, location.Y - 10));
        }

        private int whichCircle(Point clickedPoint) {
            for (int i = 0; i < nyjet.Count; i++) {
                if (InsideCircle(nyjet[i], clickedPoint)) {
                    return i;
                }
            }
            return -1;
        }

        

        private Boolean OutsideForm(Point p) {
            int x = label1.Width;
            int y = label1.Height;
            int x_max = p.X + 25;
            int x_min = p.X - 25;
            int y_max = p.Y + 25;
            int y_min = p.Y - 25;
            return (x_max > x || x_min < 0 || y_max > y || y_min < 0);
        }
        private bool InsideCircle(Point h, Point m)
        {
            return (Math.Sqrt(Math.Pow((h.X - m.X), 2) + Math.Pow((h.Y - m.Y), 2)) < 25);
        }

        private bool InsideCircle2(Point h, Point m)
        {
            return (Math.Sqrt(Math.Pow((h.X - m.X), 2) + Math.Pow((h.Y - m.Y), 2)) < 60);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mode)
            {
                button1.Text = "Shto Nyje";
            }
            else {
                button1.Text = "Lidh Nyjet";
            }
            mode = !mode;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrawAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = grafi.ListaFqinjesise();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine("FILLOJ DFS");
            GrafAlgoritmet ga = new GrafAlgoritmet(grafi);
            DrawNewGraph(ga.DFS(0));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Console.WriteLine("FILLOJ BFS");
            GrafAlgoritmet ga = new GrafAlgoritmet(grafi);
            DrawNewGraph(ga.BFS(0));
        }

        private void DrawNewGraph(Grafi newGrafi) {
            DrawAll(nyjet,lidhjetFromAdjList(newGrafi.G));
        }

        private List<int []> lidhjetFromAdjList(List<List<int>> adjList) {
            List<int[]> result = new List<int[]>();
            for (int i = 0; i < adjList.Count; i++) {
                for (int j = 0; j < adjList[i].Count; j++) {
                    int[] e = { i, adjList[i][j] };
                    int[] b = { adjList[i][j], i };
                    if (!(result.Contains(e) || result.Contains(b))) {
                        result.Add(e);
                    }
                }
            }
            return result;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2();
            form2.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Console.WriteLine("FILLOJ EULERI");


            GrafAlgoritmet ga = new GrafAlgoritmet( new Grafi(grafi.G));
            List<int> Qarku = ga.GrafiEulerit();
            if (Qarku != null)
            {
                if (Qarku[0] == Qarku[Qarku.Count - 1])
                {
                    textBox1.Text = "Graf i Eulerit. Qarku: ";   
                }
                else {
                    textBox1.Text = "Semi-Graf i Eulerit. Rruga: ";
                }
                textBox1.Text += listToString(Qarku);
            }
            else {
                textBox1.Text = "Nuk eshte Graf i Eulerit";
            }

            
        }


        private String listToString(List<int> list) {
            String s = "";
            for ( int i = 0; i < list.Count; i++ ) {

                if (i != list.Count - 1)
                {
                    s += list[i] + " - ";
                }
                else {
                    s += list[i];
                }

            }
            return s;
        }


        private String ArrayToString(int[] list)
        {
            String s = "";
            for (int i = 0; i < list.Length; i++)
            {

                if (i != list.Length - 1)
                {
                    s += list[i] + ", ";
                }
                else
                {
                    s += list[i];
                }

            }
            return s;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            NrNyje = 0;
            //int selected = 0
            selectedNyjet = new List<Point>();
            selectedNyjet2 = new List<int>();
            nyjet = new List<Point>();
            lidhjet = new List<int[]>();
            grafi = new Grafi();
            g.Clear(Color.White);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GrafAlgoritmet ga = new GrafAlgoritmet(grafi);
            ga.NaiveHamiltonian();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GrafAlgoritmet ga = new GrafAlgoritmet(grafi);
            ga.BackTrackHamiltonian();
        }
    }
}
