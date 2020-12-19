using System;
using System.Collections.Generic;

public class Grafi
{

	public List<List<int>> G = new List<List<int>>();
	public Grafi()
	{

	}
	public Grafi(int nr)
	{
		for (int i = 0; i < nr; i++) {
			ShtoNyje();
		}
	}


	public Grafi(List<List<int>> G) :this(G.Count)
	{
		for (int i = 0; i < G.Count; i++) {
			for (int j = 0; j < G[i].Count; j++) {
				this.G[i].Add(G[i][j]);
			}
		}
	}


	public List<List<int>> getListaFqinjesise() {
		return G;
	}

	public int[] getNyjet() {
		int[] Nyjet = new int[getNrNyjeve()];
		for (int i = 0; i < getNrNyjeve(); i++) {
			Nyjet[i] = i;
		}
		return Nyjet;
	}

	public Boolean egzistonDega(int n1, int n2) {
		return G[n1].Contains(n2);
	}

	public void ShtoNyje() {
		G.Add(new List<int>());
	}

	public void LargoDegen(int n1, int n2) {
		G[n1].Remove(n2);
		G[n2].Remove(n1);
	}
	public int getNrNyjeve() {
		return G.Count;
	}
	public void shtoDege(int n1, int n2) {
		G[n1].Add(n2);
		G[n2].Add(n1);
	}

	public String ListaFqinjesise() {
		String s = "";
		for (int i = 0; i < G.Count; i++) {
			s += "Nyja " + (i + 1);
			for (int j = 0; j < G[i].Count; j++) {
				s += " -> " + (G[i][j]+1);
			}
			s += "\r\n";
		}
		return s;
	}
}
