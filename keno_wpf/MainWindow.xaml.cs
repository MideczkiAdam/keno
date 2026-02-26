using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace keno_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> sorsoltSzamok;
        public MainWindow()
        {
            InitializeComponent();

            for (int i = 1; i <= 80; i++)
            {
                Border cella = new Border
                {
                    Background = Brushes.LightGreen,
                    BorderBrush = Brushes.DarkGreen,
                    BorderThickness = new Thickness(0.5),
                    Tag = i
                };

                TextBlock szam = new TextBlock
                {
                    Text = i.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                cella.Child = szam;
                gridSzamok.Children.Add(cella);
            }
        }

        private void btnBetoltes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(ofd.FileName);
                foreach (string line in lines)
                {
                    lbSzelvenyek.Items.Add(line);
                }

            }
        }

        private void btnSorsolas_Click(object sender, RoutedEventArgs e)
        {
            lbSorsolt.Items.Clear();

            Random rnd = new Random();
            sorsoltSzamok = new List<int>();

            while (sorsoltSzamok.Count < 20)
            {
                int szam = rnd.Next(1, 81);

                if (!sorsoltSzamok.Contains(szam))
                {
                    sorsoltSzamok.Add(szam);
                }
            }
            sorsoltSzamok.Sort();
            foreach (int szam in sorsoltSzamok)
            {
                lbSorsolt.Items.Add(szam);
            }
        }

        private void lbSzelvenyek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSzelvenyek.SelectedItem == null) return;

            string sor = lbSzelvenyek.SelectedItem.ToString();

            foreach ( Border cella in gridSzamok.Children)
            {
                cella.Background = Brushes.LightGreen;
            }

            string[] ketResz = sor.Split('!');

            string fej = ketResz[0];
            string szamResz = ketResz[1];

            string[] fejReszek = fej.Split(',');
            int tet = int.Parse(fejReszek[1]);

            List<int> szamok = szamResz.Split(',').Select(x => Convert.ToInt32(x)).ToList();

            foreach (Border cella in gridSzamok.Children)
            {
                int cellaSzam = (int)cella.Tag;

                if ( szamok.Contains(cellaSzam) )
                {
                    cella.Background = Brushes.Yellow;
                }
            }

            if (sorsoltSzamok == null)
            {
                MessageBox.Show("Nincs sorsolt szám!");
                return;
            }
            else
            {
                int szorzo = Szorzo(sorsoltSzamok, szamok);
                lblSzorzo.Content = szorzo;
                int nyeremeny = tet * 200 * szorzo;
                lblNyeremeny.Content = nyeremeny;
            }
        }

        private int Szorzo(List<int> kenoSzamai, List<int> tippek)
        {
            Dictionary<String, int> nyeroParok = new Dictionary<string, int>(){
                {"10-10",1000000}, {"10-9",8000}, {"10-8",350}, {"10-7",30}, {"10-6",3}, {"10-5",1}, {"10-0",2},
                {"9-9",100000}, {"9-8",1200}, {"9-7",100}, {"9-6",12}, {"9-5",3}, {"9-0",1},
                {"8-8",20000}, {"8-7",350}, {"8-6",25}, {"8-5",5}, {"8-0",1},
                {"7-7",5000}, {"7-6",60}, {"7-5",6}, {"7-4",1}, {"7-0",1},
                {"6-6",500}, {"6-5",20}, {"6-4",3}, {"6-0",1},
                {"5-5",200}, {"5-4",10}, {"5-3",2},
                {"4-4",100}, {"4-3",2},
                {"3-3",15}, {"3-2",1},
                {"2-2",6},
                {"1-1",2}
            };
            int jatekTipus = tippek.Count;
            int talalatokSzama = 0;
            foreach (int tipp in tippek)
            {
                if (kenoSzamai.Contains(tipp))
                {
                    talalatokSzama++;
                }
            }
            string kulcs = jatekTipus + "-" + talalatokSzama;

            if (nyeroParok.Keys.Contains(kulcs))
                return nyeroParok[kulcs];
            else
                return 0;
        }
    }
}