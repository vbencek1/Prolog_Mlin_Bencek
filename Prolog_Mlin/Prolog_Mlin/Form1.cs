using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SbsSW.SwiPlCs;

namespace Prolog_Mlin
{
    public partial class Form1 : Form
    {
        public Pravila pravila = new Pravila();

        public int stanje = 0; //0-ugaseno 1- postavljane zetona na plocu, 2- kretanje zetona po ploci 3- mlin kroz igru 4- mlin kod postavljanja 5- preraspodijela zetona
        private int PritisnutiGumb=0;

        public Form1()
        {
            InitializeComponent();
            Environment.SetEnvironmentVariable("SWI_HOME_DIR", @"C:\Program Files (x86)\swipl");
            Environment.SetEnvironmentVariable("Path", @"C:\Program Files (x86)\swipl");
            Environment.SetEnvironmentVariable("Path", @"C:\Program Files (x86)\swipl\bin");
            PlEngine.PlCleanup();
            pravila.OcitajPredikatePozicije();
            pravila.OcitajPredikateMlina();
            pravila.OcitajPredikateIgre();
           
        }
        
        
        public void PrikaziBrojZetona()
        {

            LabelaBijeliZetoni.Text = "Bijeli žetoni: " + pravila.IspisiBrojBijelihZetona().ToString();
            LabelaCrniZetoni.Text = "Crni žetoni: " + pravila.IspisiBrojCrnihZetona().ToString();
        }

        public void ZapocniDruguFazu()
        {
            if(pravila.IspisiBrojCrnihZetona() == 0 && pravila.IspisiBrojBijelihZetona() == 0 && stanje == 1)
            {
                stanje = 2;
                LabelaObavijesti.Text = "Započela je druga faza igre. Sada se možete kretati žetonima po ploči!";
                pravila.PostaviCrnogIgraca();
                LabelaFaze.Text ="Faza: "+ stanje.ToString();
                pravila.SmanjiBrojPocetnihBijelihZetonaZaMaknute();
                pravila.SmanjiBrojPocetnihCrnihZetonaZaMaknute();
                PrikaziBrojZetona();
            }
        }

        public void PromijeniIgraca() {
            LabelaPoteza.Text = pravila.PromijeniIgracaNaPotezu();
        }
        public void UnesiPozicijuZetona(int igrac, int pozicija)
        {
            pravila.UnosPozicijeZetona(igrac, pozicija);
        }

        public void AzurirajPozicijuZetona(int igrac, int trenutna, int nova)
        {
            pravila.AzuriranjePozicijeZetona(igrac, trenutna, nova);
        }

        public void PostaviPocetniZeton(Button odabraniGumb, int pozicija)
        {
            if (odabraniGumb.BackColor == Color.CornflowerBlue)
            {
                if (pravila.IspisiIgracaNaPotezu() == 1) { odabraniGumb.BackColor = Color.Black; pravila.OduzmiCrniZeton(); }
                else { odabraniGumb.BackColor = Color.White; pravila.OduzmiBijeliZeton(); }
                UnesiPozicijuZetona(pravila.IspisiIgracaNaPotezu(), pozicija);
                if (!ProvjeriMlin(pravila.IspisiIgracaNaPotezu(), pozicija))
                {
                    PromijeniIgraca();
                }
                PrikaziBrojZetona();
                ZapocniDruguFazu();
            }
        }

        public void PostaviZetonNaPoziciju(Button ovajGumb, Button prijasnjiGumb, int idOvogGumb, int idPrijasnjegGumba)
        {
            if (pravila.ProvjeraIgracaNaPotezu(pravila.IspisiIgracaNaPotezu(), idPrijasnjegGumba))
            {
                if (pravila.ProvjeraPozicije(PritisnutiGumb, idOvogGumb) && pravila.ProvjeriZauzetostPolja(idOvogGumb) && PritisnutiGumb == idPrijasnjegGumba)
                {
                    AzurirajPozicijuZetona(pravila.IspisiIgracaNaPotezu(), PritisnutiGumb, idOvogGumb);
                    if (pravila.IspisiIgracaNaPotezu() == 1) { ovajGumb.BackColor = Color.Black; }
                    else { ovajGumb.BackColor = Color.White; }
                    prijasnjiGumb.BackColor = Color.CornflowerBlue;
                    if (!ProvjeriMlin(pravila.IspisiIgracaNaPotezu(), idOvogGumb))
                    {
                        PromijeniIgraca();
                    }
                }
            }
        }

        public bool ProvjeriMlin(int igrac, int pozicija)
        {
            if (pravila.ProvjeriMlin(pravila.IspisiIgracaNaPotezu(), pozicija))
            {
                if (pravila.IspisiIgracaNaPotezu() == 1)
                {
                    LabelaObavijesti.Text = "Crni je napravio mlin. Sad može maknuti jedan bijeli žeton sa ploče!";
                    if ( stanje== 2) stanje = 3;
                    if (stanje == 1) stanje = 4;
                    LabelaFaze.Text = "Faza: " + stanje.ToString();
                }
                if (pravila.IspisiIgracaNaPotezu() == 2)
                {
                    LabelaObavijesti.Text = "Bijeli je napravio mlin. Sad može maknuti jedan crni žeton sa ploče!";
                    if (stanje == 2) stanje = 3;
                    if (stanje == 1) stanje = 4;
                    LabelaFaze.Text = "Faza: " + stanje.ToString();
                }
                return true;
            }return false;
        }
        public void MakniZetonSaPloce(Button gumbPozicije,int pozicija)
        {
            if (pravila.ZastitaOdUklanjanjaVlastitogZetona(pravila.IspisiIgracaNaPotezu(), pozicija) && pravila.ZastitaOdUklanjanjaPraznogPolja(pozicija))
            {
                if (pravila.IspisiIgracaNaPotezu() == 1)
                {
                    pravila.MakniZetonSaPloce(2, pozicija);
                    pravila.OduzmiBijeliZeton();
                    gumbPozicije.BackColor = Color.CornflowerBlue;
                }
                if (pravila.IspisiIgracaNaPotezu() == 2)
                {
                    pravila.MakniZetonSaPloce(1, pozicija);
                    pravila.OduzmiCrniZeton();
                    gumbPozicije.BackColor = Color.CornflowerBlue;
                }
                stanje = 2;
                LabelaFaze.Text = "Faza: " + stanje.ToString();
                PrikaziBrojZetona();
                LabelaObavijesti.Text = "";
                PromijeniIgraca();
                ProglasiPobjednika();
                
            }
        }
        public void MakniZetonSaPocetnePloce(Button gumbPozicije, int pozicija)
        {
            if (pravila.ZastitaOdUklanjanjaVlastitogZetona(pravila.IspisiIgracaNaPotezu(), pozicija) && pravila.ZastitaOdUklanjanjaPraznogPolja(pozicija))
            {
                if (pravila.IspisiIgracaNaPotezu() == 1)
                {
                    pravila.MakniZetonSaPloce(2, pozicija);
                    pravila.PovecajBrojMaknutihBijelihZetona();
                    gumbPozicije.BackColor = Color.CornflowerBlue;
                }
                if (pravila.IspisiIgracaNaPotezu() == 2)
                {
                    pravila.MakniZetonSaPloce(1, pozicija);
                    pravila.PovecajBrojMaknutihCrnihZetona();
                    gumbPozicije.BackColor = Color.CornflowerBlue;
                }
                stanje = 1;
                LabelaFaze.Text = "Faza: " + stanje.ToString();
                PrikaziBrojZetona();
                LabelaObavijesti.Text = "";
                PromijeniIgraca();
                ZapocniDruguFazu();
            }
        }

        public void OcistiSve()
        {
            stanje = 0;
            pravila.ResetirajPredikateIgre();

            LabelaBijeliZetoni.Text = "---------------";
            LabelaCrniZetoni.Text = "---------------";
            LabelaFaze.Text = "---------------";
            LabelaObavijesti.Text = "";
            LabelaPoteza.Text = "---------------";

            GumbPozicije1.BackColor = Color.CornflowerBlue;
            GumbPozicije2.BackColor = Color.CornflowerBlue;
            GumbPozicije3.BackColor = Color.CornflowerBlue;
            GumbPozicije4.BackColor = Color.CornflowerBlue;
            GumbPozicije5.BackColor = Color.CornflowerBlue;
            GumbPozicije6.BackColor = Color.CornflowerBlue;
            GumbPozicije7.BackColor = Color.CornflowerBlue;
            GumbPozicije8.BackColor = Color.CornflowerBlue;
            GumbPozicije9.BackColor = Color.CornflowerBlue;
            GumbPozicije10.BackColor = Color.CornflowerBlue;
            GumbPozicije11.BackColor = Color.CornflowerBlue;
            GumbPozicije12.BackColor = Color.CornflowerBlue;
            GumbPozicije13.BackColor = Color.CornflowerBlue;
            GumbPozicije14.BackColor = Color.CornflowerBlue;
            GumbPozicija15.BackColor = Color.CornflowerBlue;
            GumbPozicije16.BackColor = Color.CornflowerBlue;
            GumbPozicije17.BackColor = Color.CornflowerBlue;
            GumbPozicije18.BackColor = Color.CornflowerBlue;
            GumbPozicije19.BackColor = Color.CornflowerBlue;
            GumbPozicije20.BackColor = Color.CornflowerBlue;
            GumbPozicije21.BackColor = Color.CornflowerBlue;
            GumbPozicije22.BackColor = Color.CornflowerBlue;
            GumbPozicije23.BackColor = Color.CornflowerBlue;
            GumbPozicije24.BackColor = Color.CornflowerBlue;
            GumbPokreni.Enabled = true;
        }

        public void ProglasiPobjednika()
        {
            if (pravila.ProvjeriAkoJePobijedioCrni()) MessageBox.Show("Pobijedio je CRNI");
            if (pravila.ProvjeriAkoJePobijedioBijeli()) MessageBox.Show("Pobijedio je BIJELI");
            if (pravila.ProvjeriAkoJePobijedioCrni() || pravila.ProvjeriAkoJePobijedioBijeli()) OcistiSve();
        }

        private void GumbPokreni_Click(object sender, EventArgs e)
        {
            stanje = 1;
            LabelaObavijesti.Text= "Započela je prva faza igre. Postavite žetone na ploču. Prvi kreće CRNI!";
            LabelaPoteza.Text = "Na potezu je: CRNI";
            LabelaFaze.Text ="Faza: "+ stanje.ToString();
            PrikaziBrojZetona();
            GumbPokreni.Enabled = false;
        }

        private void GumbPredaje_Click(object sender, EventArgs e)
        {
            if (pravila.IspisiIgracaNaPotezu() == 2) MessageBox.Show("Pobijedio je CRNI");
            if (pravila.IspisiIgracaNaPotezu() == 1) MessageBox.Show("Pobijedio je BIJELI");
            OcistiSve();
        }

        private void GumbUgasi_Click(object sender, EventArgs e)
        {
            OcistiSve();
        }

        private void GumbPozicije1_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije1, 1);
            }
            
            if (stanje == 2) {
                PostaviZetonNaPoziciju(GumbPozicije1,GumbPozicije2,1,2);
                PostaviZetonNaPoziciju(GumbPozicije1, GumbPozicije8, 1, 8);
                PritisnutiGumb = 1;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije1, 1);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije1, 1);
            }
        }

        private void GumbPozicije2_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije2, 2);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije2, GumbPozicije1, 2, 1);
                PostaviZetonNaPoziciju(GumbPozicije2, GumbPozicije3, 2, 3);
                PostaviZetonNaPoziciju(GumbPozicije2, GumbPozicije10, 2, 10);
                PritisnutiGumb = 2;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije2, 2);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije2, 2);
            }
        }

        private void GumbPozicije3_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije3, 3);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije3, GumbPozicije2, 3, 2);
                PostaviZetonNaPoziciju(GumbPozicije3, GumbPozicije4, 3, 4);
                PritisnutiGumb = 3;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije3, 3);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije3, 3);
            }
        }

        private void GumbPozicije4_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije4, 4);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije4, GumbPozicije3, 4, 3);
                PostaviZetonNaPoziciju(GumbPozicije4, GumbPozicije5, 4, 5);
                PostaviZetonNaPoziciju(GumbPozicije4, GumbPozicije12, 4, 12);
                PritisnutiGumb = 4;
            }
            if (stanje == 1)
            {
                if (stanje == 1)
                {
                    PostaviPocetniZeton(GumbPozicije4, 4);
                }
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije4, 4);
            }
        }

        private void GumbPozicije5_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije5, 5);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije5, GumbPozicije4, 5, 4);
                PostaviZetonNaPoziciju(GumbPozicije5, GumbPozicije6, 5, 6);
                PritisnutiGumb = 5;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije5, 5);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije5, 5);
            }
        }

        private void GumbPozicije6_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije6, 6);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije6, GumbPozicije5, 6, 5);
                PostaviZetonNaPoziciju(GumbPozicije6, GumbPozicije7, 6, 7);
                PostaviZetonNaPoziciju(GumbPozicije6, GumbPozicije14, 6, 14);
                PritisnutiGumb = 6;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije6, 6);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije6, 6);
            }
        }

        private void GumbPozicije7_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije7, 7);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije7, GumbPozicije6, 7, 6);
                PostaviZetonNaPoziciju(GumbPozicije7, GumbPozicije8, 7, 8);
                PritisnutiGumb = 7;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije7, 7);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije7, 7);
            }
        }

        private void GumbPozicije8_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije8, 8);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije8, GumbPozicije1, 8, 1);
                PostaviZetonNaPoziciju(GumbPozicije8, GumbPozicije7, 8, 7);
                PostaviZetonNaPoziciju(GumbPozicije8, GumbPozicije16, 8, 16);
                PritisnutiGumb = 8;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije8, 8);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije8, 8);
            }
        }

        private void GumbPozicije9_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije9, 9);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije9, GumbPozicije10, 9, 10);
                PostaviZetonNaPoziciju(GumbPozicije9, GumbPozicije16, 9, 16);
                PritisnutiGumb = 9;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije9, 9);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije9, 9);
            }
        }

        private void GumbPozicije10_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije10, 10);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije10, GumbPozicije2, 10, 2);
                PostaviZetonNaPoziciju(GumbPozicije10, GumbPozicije9, 10, 9);
                PostaviZetonNaPoziciju(GumbPozicije10, GumbPozicije11, 10, 11);
                PostaviZetonNaPoziciju(GumbPozicije10, GumbPozicije18, 10, 18);
                PritisnutiGumb = 10;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije10, 10);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije10, 10);
            }
        }

        private void GumbPozicije11_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije11, 11);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije11, GumbPozicije10, 11, 10);
                PostaviZetonNaPoziciju(GumbPozicije11, GumbPozicije12, 11, 12);
                PritisnutiGumb = 11;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije11, 11);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije11, 11);
            }
        }

        private void GumbPozicije12_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije12, 12);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije12, GumbPozicije4, 12, 4);
                PostaviZetonNaPoziciju(GumbPozicije12, GumbPozicije11, 12, 11);
                PostaviZetonNaPoziciju(GumbPozicije12, GumbPozicije13, 12, 13);
                PostaviZetonNaPoziciju(GumbPozicije12, GumbPozicije20, 12, 20);
                PritisnutiGumb = 12;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije12, 12);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije12, 12);
            }
        }

        private void GumbPozicije13_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije13, 13);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije13, GumbPozicije12, 13, 12);
                PostaviZetonNaPoziciju(GumbPozicije13, GumbPozicije14, 13, 14);
                PritisnutiGumb = 13;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije13, 13);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije13, 13);
            }
        }

        private void GumbPozicije14_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije14, 14);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije14, GumbPozicije6, 14, 6);
                PostaviZetonNaPoziciju(GumbPozicije14, GumbPozicije13, 14, 13);
                PostaviZetonNaPoziciju(GumbPozicije14, GumbPozicija15, 14, 15);
                PostaviZetonNaPoziciju(GumbPozicije14, GumbPozicije22, 14, 22);
                PritisnutiGumb = 14;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije14, 14);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije14, 14);
            }
        }

        private void GumbPozicija15_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicija15, 15);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicija15, GumbPozicije14, 15, 14);
                PostaviZetonNaPoziciju(GumbPozicija15, GumbPozicije16, 15, 16);
                PritisnutiGumb = 15;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicija15, 15);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicija15, 15);
            }
        }

        private void GumbPozicije16_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije16, 16);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije16, GumbPozicije8, 16, 8);
                PostaviZetonNaPoziciju(GumbPozicije16, GumbPozicije9, 16, 9);
                PostaviZetonNaPoziciju(GumbPozicije16, GumbPozicija15, 16, 15);
                PostaviZetonNaPoziciju(GumbPozicije16, GumbPozicije24, 16, 24);
                PritisnutiGumb = 16;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije16, 16);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije16, 16);
            }
        }

        private void GumbPozicije17_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije17, 17);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije17, GumbPozicije18, 17, 18);
                PostaviZetonNaPoziciju(GumbPozicije17, GumbPozicije24, 17, 24);
                PritisnutiGumb = 17;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije17, 17);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije17, 17);
            }
        }

        private void GumbPozicije18_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije18, 18);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije18, GumbPozicije10, 18, 10);
                PostaviZetonNaPoziciju(GumbPozicije18, GumbPozicije17, 18, 17);
                PostaviZetonNaPoziciju(GumbPozicije18, GumbPozicije19, 18, 19);
                PritisnutiGumb = 18;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije18, 18);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije18, 18);
            }
        }

        private void GumbPozicije19_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije19, 19);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije19, GumbPozicije18, 19, 18);
                PostaviZetonNaPoziciju(GumbPozicije19, GumbPozicije20, 19, 20);
                PritisnutiGumb = 19;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije19, 19);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije19, 19);
            }
        }

        private void GumbPozicije20_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije20, 20);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije20, GumbPozicije12, 20, 12);
                PostaviZetonNaPoziciju(GumbPozicije20, GumbPozicije19, 20, 19);
                PostaviZetonNaPoziciju(GumbPozicije20, GumbPozicije21, 20, 21);
                PritisnutiGumb = 20;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije20, 20);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije20, 20);
            }
        }

        private void GumbPozicije21_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije21, 21);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije21, GumbPozicije20, 21, 20);
                PostaviZetonNaPoziciju(GumbPozicije21, GumbPozicije22, 21, 22);
                PritisnutiGumb = 21;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije21, 21);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije21, 21);
            }
        }

        private void GumbPozicije22_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije22, 22);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije22, GumbPozicije14, 22, 14);
                PostaviZetonNaPoziciju(GumbPozicije22, GumbPozicije21, 22, 21);
                PostaviZetonNaPoziciju(GumbPozicije22, GumbPozicije23, 22, 23);
                PritisnutiGumb = 22;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije22, 22);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije22, 22);
            }
        }

        private void GumbPozicije23_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije23, 23);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije23, GumbPozicije22, 23, 22);
                PostaviZetonNaPoziciju(GumbPozicije23, GumbPozicije24, 23, 24);
                PritisnutiGumb = 23;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije23, 23);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije23, 23);
            }
        }

        private void GumbPozicije24_Click(object sender, EventArgs e)
        {
            if (stanje == 3)
            {
                MakniZetonSaPloce(GumbPozicije24, 24);
            }
            if (stanje == 2)
            {
                PostaviZetonNaPoziciju(GumbPozicije24, GumbPozicije16, 24, 16);
                PostaviZetonNaPoziciju(GumbPozicije24, GumbPozicije17, 24, 17);
                PostaviZetonNaPoziciju(GumbPozicije24, GumbPozicije23, 24, 23);
                PritisnutiGumb = 24;
            }
            if (stanje == 1)
            {
                PostaviPocetniZeton(GumbPozicije24, 24);
            }
            if (stanje == 4)
            {
                MakniZetonSaPocetnePloce(GumbPozicije24, 24);
            }
        }


        private void SlikaGumbUgasi_Click(object sender, EventArgs e)
        {
            this.Close();
            PlEngine.PlCleanup();
        }

        private void label1_Click(object sender, EventArgs e)
        { }
    }
}
