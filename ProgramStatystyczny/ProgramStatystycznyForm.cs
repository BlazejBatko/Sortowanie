using System;
using System.Linq;
using System.Windows.Forms;

namespace ProgramStatystyczny
{
    public partial class ProgramStatystycznyForm : Form
    {
        int[] tablicaLiczb;
        
        public ProgramStatystycznyForm()
        {
            InitializeComponent();
        }

        private void btnGeneruj_Click(object sender, EventArgs e)
        {
            int min = int.Parse(txtMin.Text);
            int max = int.Parse(txtMax.Text);
            int ilosc = int.Parse(txtIlosc.Text);

            int[] wylosowaneLiczby = GenerujCiagLiczb(min, max, ilosc);

            tablicaLiczb = wylosowaneLiczby;

            txtCiagN.Text = string.Join(" ", wylosowaneLiczby);

            cbxCiagN.DataSource = wylosowaneLiczby;
        }
        int[] GenerujCiagLiczb(
            int minimalnaLiczba,
            int maksymalnaLiczba,
            int iloscLiczb)
        {
            int[] tablica = new int[iloscLiczb];
            Random generator = new Random();

            for (int i = 0; i < tablica.Length; i++)
            {
                tablica[i] = generator.Next(minimalnaLiczba, maksymalnaLiczba);
            }

            return tablica;
        }

        private void btnSortowanieB_Click_1(object sender, EventArgs e)
        {
            txtCiagB.Clear();

            int[] kopiaDlaSortowaniaBabelkowego = (int[])tablicaLiczb.Clone();

            sortujBabelkowo(kopiaDlaSortowaniaBabelkowego);

            txtCiagB.Text = string.Join(" ", kopiaDlaSortowaniaBabelkowego);

        }

        void sortujBabelkowo(int[] tablicaLiczb)
        {
            for (int i = 0; i < tablicaLiczb.Length; i++)
            {
                for (int j = i + 1; j < tablicaLiczb.Length; j++)
                {
                    if (tablicaLiczb[i] > tablicaLiczb[j])
                    {
                        int temp = tablicaLiczb[i];

                        tablicaLiczb[i] = tablicaLiczb[j];

                        tablicaLiczb[j] = temp;
                    }
                }
            }
        }

        void sortujPrzezWstawianie(int[] tablicaLiczb)
        {
            for (int i = 0; i < tablicaLiczb.Length; i++)
            {
                int j = i;
                var temp = tablicaLiczb[j];
                while (j > 0 && tablicaLiczb[j - 1] > temp)
                {
                    tablicaLiczb[j] = tablicaLiczb[j - 1];
                    j--;
                }
                tablicaLiczb[j] = temp;
            }
        }

        private void btnSortowanieW_Click_1(object sender, EventArgs e)
        {
            txtCiagW.Clear();

            int[] kopiaDlaSortowania = (int[])tablicaLiczb.Clone();

            sortujPrzezWstawianie(kopiaDlaSortowania);

            txtCiagW.Text = string.Join(" ", kopiaDlaSortowania);
        }

        void sortujQuicksort(int[] tablicaLiczb, int L, int R)
        {
            int i = L;
            int j = R;
            int pv = tablicaLiczb[(L + R) / 2];

            while (i <= j)
            {
                while (tablicaLiczb[i] < pv)
                    i++;
                while (tablicaLiczb[j] > pv)
                    j--;

                if (i <= j)
                {
                    int tmp = tablicaLiczb[i];
                    tablicaLiczb[i++] = tablicaLiczb[j];
                    tablicaLiczb[j--] = tmp;
                }
            }
            if (j > L)
            {
                sortujQuicksort(tablicaLiczb, L, j);
            }
            if (i < R)
            {
                sortujQuicksort(tablicaLiczb, i, R);
            }
        }

        private void btnSortowanieQ_Click_1(object sender, EventArgs e)
        {
            txtCiagQ.Clear();

            int[] kopiaDlaSortowania = (int[])tablicaLiczb.Clone();

            sortujQuicksort(kopiaDlaSortowania, 0, kopiaDlaSortowania.Length - 1);

            txtCiagQ.Text = string.Join(" ", kopiaDlaSortowania);
        }

        private void txtWylicz_Click(object sender, EventArgs e)
        {
            txtSrednia.Text = Convert.ToString(tablicaLiczb.Average());
            txtMediana.Text = Convert.ToString(Mediana(tablicaLiczb));
            txtDominanta.Text = Convert.ToString(Dominanta());
        }

        // nie potrafię przekonwerterować tego na double aby wynik mediany mógł być po przecinku :(
        int Mediana(int[] tablicaLiczb)
        {
            Array.Sort(tablicaLiczb);
            return tablicaLiczb[tablicaLiczb.Length / 2];
        }

        int Dominanta()
        {
            var mode = tablicaLiczb.GroupBy(n => n).
                OrderByDescending(g => g.Count()).
                Select(g => g.Key).FirstOrDefault();
            return mode;
        }
        
        int szukana()
        {
            
            int n = int.Parse(txtSzukana.Text);
            int[] kopiaDlaSzukania = tablicaLiczb;
            sortujPrzezWstawianie(kopiaDlaSzukania);
            int pozycja =Array.IndexOf(kopiaDlaSzukania, n );
          

          
            return pozycja + 1;
        }

       
        private void btnSzukanie_Click(object sender, EventArgs e)
        {
           txtPozycja.Text=Convert.ToString(szukana());
        }

       
    }
}