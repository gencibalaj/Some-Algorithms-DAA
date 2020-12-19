using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAA
{
    class GrafAlgoritmet {

        Grafi grafi;
        Boolean[] visited;
        Boolean[] discoverd;
        Grafi DFSGrafi;
        Grafi BFSGrafi;
        List<int> QarkuIEulerit = new List<int>();

        public GrafAlgoritmet(Grafi grafi) {
            this.grafi = grafi;
            DFSGrafi = new Grafi(grafi.getNrNyjeve());
            BFSGrafi = new Grafi(grafi.getNrNyjeve());
            visited = new Boolean[grafi.getNrNyjeve()];
            discoverd = new Boolean[grafi.getNrNyjeve()];
        }

        public Grafi DFS(int nyja) {
            visited[nyja] = true;
            List<int> a = grafi.G[nyja];
            for (int i = 0; i < a.Count; i++) {
                int adj = a[i];
                if (!visited[adj]) {
                    DFSGrafi.shtoDege(nyja, adj);
                    DFS(adj);
                }
            }
            return DFSGrafi;
        }

        public Grafi BFS(int nyja) {
            Queue<int> Q = new Queue<int>();
            discoverd[nyja] = true;
            Q.Enqueue(nyja);
            while (Q.Count > 0) {
                int v = Q.Dequeue();
                BFSGrafi.shtoDege(nyja, v);
                Console.WriteLine(v);
                for (int i = 0; i < grafi.G[v].Count; i++) {
                    int w = grafi.G[v][i];
                    if (!discoverd[w]) {
                        discoverd[w] = true;
                        Q.Enqueue(w);
                    }
                }
            }
            return BFSGrafi;
        }

        public List<int> GrafiEulerit (){
            grafi.ListaFqinjesise();
            int nyja = isGrafIEulerit();
            if (nyja != -1)
            {
                QarkuIEulerit.Add(nyja+1);
                QarkuEulerit(nyja);
                return QarkuIEulerit;
            }
            else {
                return null;
            }

        }

        //-1 - jo
        //0 - po
        //tjeter - Semi-Graf i Eulerit
        public int isGrafIEulerit() {
            int nyje_me_shkall_teke = 0;
            int nyja_startuse = 0;
            for (int i = 0; i < grafi.G.Count; i++) {
                int shkalla_nyjes = grafi.G[i].Count;
                if (shkalla_nyjes % 2 == 1) {
                    nyja_startuse = i;
                    nyje_me_shkall_teke++;
                }
            }
            if (nyje_me_shkall_teke == 0)
            {
                return nyja_startuse; // Nese eshte Grafi i Eulerit fillo nga nyja 0 
            }
            else if (nyje_me_shkall_teke == 2)
            {
                return nyja_startuse; //Nese eshte Semi - Grafi i Eulerit fillo nga nyja teke 
            }
            else {
                return -1; //Nuk eshte grafi i Eulerit
            }

        }


        public void QarkuEulerit(int nyja) {
            for (int i = 0; i < grafi.G[nyja].Count; i++) {
                int nyja_fqinje = grafi.G[nyja][i];
                if (DegeValide(nyja, nyja_fqinje)) {
                    
                    QarkuIEulerit.Add(nyja_fqinje+1);
                    
                    //Console.WriteLine(nyja + "-" + nyja_fqinje + " ");
                    grafi.LargoDegen(nyja, nyja_fqinje);
                    QarkuEulerit(nyja_fqinje);
                }
            }
        }

        public Boolean DegeValide(int nyja1, int nyja2) {

            if (grafi.G[nyja1].Count == 1) {
                return true;
            }

            visited = new Boolean[grafi.G.Count];
            DFSGrafi = new Grafi(grafi.getNrNyjeve());
            int c1 = DFS(nyja1).G.Count;

            grafi.LargoDegen(nyja1, nyja2);

            visited = new Boolean[grafi.G.Count];
            DFSGrafi = new Grafi(grafi.getNrNyjeve());
            int c2 = DFS(nyja1).G.Count;

            grafi.shtoDege(nyja1, nyja2);

            return !(c1 > c2);
        }

        public void NaiveHamiltonian() {
            permutation(grafi.getNrNyjeve(), grafi.getNyjet());
        }


        int[] rrugaHamiltonian;
        public void BackTrackHamiltonian() {
            rrugaHamiltonian = new int[grafi.getNrNyjeve()];
            for (int i = 0; i < grafi.getNrNyjeve(); i++) { 
                rrugaHamiltonian[i] = -1;
            }
            rrugaHamiltonian[0] = 0;
            if (QarkuHamiltonian(1) == false)
            {
                Console.WriteLine("Nuk ka qark te Hameltonit");
                Form1.textBox1.Text = "Nuk ka qark te Hameltonit\r\n";
            }
            else {
                Console.WriteLine(ArrayToString(rrugaHamiltonian));
                Form1.textBox1.Text = ArrayToString(rrugaHamiltonian)+"\r\n";
            }
        }

        Boolean PjeseHamiltonit(int nyja, int pos) {

            if (!grafi.egzistonDega(rrugaHamiltonian[pos - 1], nyja)) {
                return false;
            }

            for (int i = 0; i < rrugaHamiltonian.Length; i++) {
                if (rrugaHamiltonian[i] == nyja) {
                    return false;
                }
            }

            return true;
        }

        Boolean QarkuHamiltonian(int pos) {
            if (pos == grafi.getNrNyjeve()) {
                if (grafi.egzistonDega(rrugaHamiltonian[pos - 1], rrugaHamiltonian[0]))
                {
                    return true;
                }
                else {
                    return false;
                }
            }

            for (int v = 1; v < grafi.getNrNyjeve(); v++)
            {
                if (PjeseHamiltonit(v, pos))
                {
                    rrugaHamiltonian[pos] = v;

                    if (QarkuHamiltonian(pos + 1) == true) {
                        return true;
                    }
                    rrugaHamiltonian[pos] = -1;
                }

            }
            return false; 
        }






        public void permutation(int k , int[] A) {
            if (k == 1) {

                if (checkIfHamilton(A))
                {
                    Form1.textBox1.Text += "Konifgurimi: " + ArrayToString(A) + ", " + (A[0]+1) + " paraqet Qark\r\n";
                }
            }
            for (int i = 0; i < k; i++) {
                permutation(k-1, A);

                if (k % 2 == 1)
                {
                    int temp = A[0];
                    A[0] = A[k - 1];
                    A[k - 1] = temp;
                }
                else {
                    int temp = A[i];
                    A[i] = A[k - 1];
                    A[k - 1] = temp;
                }
            }
        }


        public Boolean checkIfHamilton(int[] A) {
            if (A.Length == 1) {
                return true;
            }

            for (int i = 0; i < A.Length; i++) {
                if (i != A.Length - 1)
                {
                    if (!grafi.egzistonDega(A[i], A[i + 1]))
                    {
                        //Console.WriteLine("Nuk Ekzison lidhja, " + i + " " + (i + 1));
                        return false;
                    }
                }else {
                    if (!grafi.egzistonDega(A[i], A[0])) {
                        return false;                    
                    }
                }    
            }
            return true;
        }

        private String ArrayToString(int[] list)
        {
            String s = "";
            for (int i = 0; i < list.Length; i++)
            {

                if (i != list.Length - 1)
                {
                    s += (list[i]+1) + ", ";
                }
                else
                {
                    s += (list[i]+1);
                }

            }
            return s;
        }
    }
}
