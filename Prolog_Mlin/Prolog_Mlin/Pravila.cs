using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SbsSW.SwiPlCs;

namespace Prolog_Mlin
{
    public class Pravila
    {
        public void InicijalizirajProlog()
        {
            if (!PlEngine.IsInitialized)
            {
                String[] param = { "-q" };
                PlEngine.Initialize(param);
            }
        }

        //funkcija koja očitava predikate koji služe sa definiranje mogućnosti kretanja po ploči
        //npr. pozicija(1,2) - znači da se žeton može pomaknuti sa pozicije 1 na poziciju 2.
        public void OcitajPredikatePozicije()
        {
            InicijalizirajProlog();
            PlQuery.PlCall("assert(pozicija(1,2))");
            PlQuery.PlCall("assert(pozicija(1,8))");
            PlQuery.PlCall("assert(pozicija(2,1))");
            PlQuery.PlCall("assert(pozicija(2,3))");
            PlQuery.PlCall("assert(pozicija(2,10))");
            PlQuery.PlCall("assert(pozicija(3,2))");
            PlQuery.PlCall("assert(pozicija(3,4))");
            PlQuery.PlCall("assert(pozicija(4,3))");
            PlQuery.PlCall("assert(pozicija(4,5))");
            PlQuery.PlCall("assert(pozicija(4,12))");
            PlQuery.PlCall("assert(pozicija(5,4))");
            PlQuery.PlCall("assert(pozicija(5,6))");
            PlQuery.PlCall("assert(pozicija(6,5))");
            PlQuery.PlCall("assert(pozicija(6,7))");
            PlQuery.PlCall("assert(pozicija(6,14))");
            PlQuery.PlCall("assert(pozicija(7,6))");
            PlQuery.PlCall("assert(pozicija(7,8))");
            PlQuery.PlCall("assert(pozicija(8,7))");
            PlQuery.PlCall("assert(pozicija(8,1))");
            PlQuery.PlCall("assert(pozicija(8,16))");
            PlQuery.PlCall("assert(pozicija(9,10))");
            PlQuery.PlCall("assert(pozicija(9,16))");
            PlQuery.PlCall("assert(pozicija(10,2))");
            PlQuery.PlCall("assert(pozicija(10,9))");
            PlQuery.PlCall("assert(pozicija(10,11))");
            PlQuery.PlCall("assert(pozicija(10,18))");
            PlQuery.PlCall("assert(pozicija(11,10))");
            PlQuery.PlCall("assert(pozicija(11,12))");
            PlQuery.PlCall("assert(pozicija(12,4))");
            PlQuery.PlCall("assert(pozicija(12,11))");
            PlQuery.PlCall("assert(pozicija(12,13))");
            PlQuery.PlCall("assert(pozicija(12,20))");
            PlQuery.PlCall("assert(pozicija(13,12))");
            PlQuery.PlCall("assert(pozicija(13,14))");
            PlQuery.PlCall("assert(pozicija(14,6))");
            PlQuery.PlCall("assert(pozicija(14,13))");
            PlQuery.PlCall("assert(pozicija(14,15))");
            PlQuery.PlCall("assert(pozicija(14,22))");
            PlQuery.PlCall("assert(pozicija(15,14))");
            PlQuery.PlCall("assert(pozicija(15,16))");
            PlQuery.PlCall("assert(pozicija(16,8))");
            PlQuery.PlCall("assert(pozicija(16,9))");
            PlQuery.PlCall("assert(pozicija(16,15))");
            PlQuery.PlCall("assert(pozicija(16,24))");
            PlQuery.PlCall("assert(pozicija(17,18))");
            PlQuery.PlCall("assert(pozicija(17,24))");
            PlQuery.PlCall("assert(pozicija(18,10))");
            PlQuery.PlCall("assert(pozicija(18,17))");
            PlQuery.PlCall("assert(pozicija(18,19))");
            PlQuery.PlCall("assert(pozicija(19,18))");
            PlQuery.PlCall("assert(pozicija(19,20))");
            PlQuery.PlCall("assert(pozicija(20,12))");
            PlQuery.PlCall("assert(pozicija(20,19))");
            PlQuery.PlCall("assert(pozicija(20,21))");
            PlQuery.PlCall("assert(pozicija(21,20))");
            PlQuery.PlCall("assert(pozicija(21,22))");
            PlQuery.PlCall("assert(pozicija(22,14))");
            PlQuery.PlCall("assert(pozicija(22,21))");
            PlQuery.PlCall("assert(pozicija(22,23))");
            PlQuery.PlCall("assert(pozicija(23,22))");
            PlQuery.PlCall("assert(pozicija(23,24))");
            PlQuery.PlCall("assert(pozicija(24,16))");
            PlQuery.PlCall("assert(pozicija(24,17))");
            PlQuery.PlCall("assert(pozicija(24,23))");

        }


        //funkcija koja očitava predikate koji služe za definiranje mlina (pokazuje koje 3 pozicije formiraju mlin)
        //npr. mlin(1,2,3) - znači da mlin nastaje kada se tri žetona nalaze na pozicijama 1,2 i 3.
        public void OcitajPredikateMlina()
        {
            InicijalizirajProlog();
            PlQuery.PlCall("assert(mlin(1,2,3))");
            PlQuery.PlCall("assert(mlin(2,1,3))");
            PlQuery.PlCall("assert(mlin(3,1,2))");
            PlQuery.PlCall("assert(mlin(9,10,11))");
            PlQuery.PlCall("assert(mlin(10,9,11))");
            PlQuery.PlCall("assert(mlin(11,9,10))");
            PlQuery.PlCall("assert(mlin(17,18,19))");
            PlQuery.PlCall("assert(mlin(18,17,19))");
            PlQuery.PlCall("assert(mlin(19,17,18))");
            PlQuery.PlCall("assert(mlin(8,16,24))");
            PlQuery.PlCall("assert(mlin(16,8,24))");
            PlQuery.PlCall("assert(mlin(24,8,16))");
            PlQuery.PlCall("assert(mlin(4,12,20))");
            PlQuery.PlCall("assert(mlin(12,4,20))");
            PlQuery.PlCall("assert(mlin(20,4,12))");
            PlQuery.PlCall("assert(mlin(21,22,23))");
            PlQuery.PlCall("assert(mlin(22,21,23))");
            PlQuery.PlCall("assert(mlin(23,21,22))");
            PlQuery.PlCall("assert(mlin(13,14,15))");
            PlQuery.PlCall("assert(mlin(14,13,15))");
            PlQuery.PlCall("assert(mlin(15,13,14))");
            PlQuery.PlCall("assert(mlin(5,6,7))");
            PlQuery.PlCall("assert(mlin(6,5,7))");
            PlQuery.PlCall("assert(mlin(7,5,6))");

            PlQuery.PlCall("assert(mlin(1,7,8))");
            PlQuery.PlCall("assert(mlin(7,1,8))");
            PlQuery.PlCall("assert(mlin(8,1,7))");
            PlQuery.PlCall("assert(mlin(9,15,16))");
            PlQuery.PlCall("assert(mlin(15,9,16))");
            PlQuery.PlCall("assert(mlin(16,9,15))");
            PlQuery.PlCall("assert(mlin(17,23,24))");
            PlQuery.PlCall("assert(mlin(23,17,24))");
            PlQuery.PlCall("assert(mlin(24,17,23))");
            PlQuery.PlCall("assert(mlin(6,14,22))");
            PlQuery.PlCall("assert(mlin(14,6,22))");
            PlQuery.PlCall("assert(mlin(22,6,14))");
            PlQuery.PlCall("assert(mlin(2,10,18))");
            PlQuery.PlCall("assert(mlin(10,2,18))");
            PlQuery.PlCall("assert(mlin(18,2,10))");
            PlQuery.PlCall("assert(mlin(19,20,21))");
            PlQuery.PlCall("assert(mlin(20,19,21))");
            PlQuery.PlCall("assert(mlin(21,19,20))");
            PlQuery.PlCall("assert(mlin(11,12,13))");
            PlQuery.PlCall("assert(mlin(12,11,13))");
            PlQuery.PlCall("assert(mlin(13,11,12))");
            PlQuery.PlCall("assert(mlin(3,4,5))");
            PlQuery.PlCall("assert(mlin(4,3,5))");
            PlQuery.PlCall("assert(mlin(5,3,4))");


        }


        //funkcija koja očitava predikate koji služe za samo izvođenje aktivne igre
        public void OcitajPredikateIgre()
        {
            InicijalizirajProlog();
            PlQuery.PlCall("assert(igracNaPotezu(1))");  //predikat koji sadrži trenutnog igrača na potezu (1- crni, 2- bijeli)
            PlQuery.PlCall("assert(brojCrnihZetona(9))");  //predikat koji sadrži ukupan broj crnih žetona
            PlQuery.PlCall("assert(brojBijelihZetona(9))");  //predikat koji sadrži ukupan broj bijelih žetona
            PlQuery.PlCall("assert(brojMaknutihCrnihZetona(0))");  //predikat koji sadrži ukupan broj maknutih crnih žetona (iz prve faze postavljanja žetona na ploču)
            PlQuery.PlCall("assert(brojMaknutihBijelihZetona(0))"); //predikat koji sadrži ukupan broj maknutih bijelih žetona (iz prve faze postavljanja žetona na ploču)
        }

        //funkcija koja mijenja igrača na potezu pomoću assert i retract-a
        public string PromijeniIgracaNaPotezu()
        {
            InicijalizirajProlog();
            if (IspisiIgracaNaPotezu() == 1)
            {
                PlQuery.PlCall("retract(igracNaPotezu(1))");
                PlQuery.PlCall("assert(igracNaPotezu(2))");
                return "Na potezu je: BIJELI";
            }
            else
            {
                PlQuery.PlCall("retract(igracNaPotezu(2))");
                PlQuery.PlCall("assert(igracNaPotezu(1))");
                return "Na potezu je: CRNI";
            }
            
        }

        //Upit koji ispisuje trenutnog igrača na potezu
        public int IspisiIgracaNaPotezu()
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("igracNaPotezu(I)"))
            {
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    return int.Parse(v["I"].ToString());
                }
                return 0;
            }
        }

        //funkcija koja resetira igrača na potezu na početnu vrijednost (Na 1- crni)
        public void PostaviCrnogIgraca()
        {
            InicijalizirajProlog();
            PlQuery.PlCall("abolish(igracNaPotezu/1)");
            PlQuery.PlCall("assert(igracNaPotezu(1))");
        }

        //upit koji vrača trenutni broj crnih žetona
        public int IspisiBrojCrnihZetona()
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("brojCrnihZetona(B)"))
            {
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    return int.Parse(v["B"].ToString());
                }
                return 0;
            }
        }

        //upit koji vrača trenutni broj bijelih žetona
        public int IspisiBrojBijelihZetona()
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("brojBijelihZetona(B)"))
            {
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    return int.Parse(v["B"].ToString());
                }
                return 0;
            }
        }

        //funkcija koja smanjuje broj crnih žetona za jedan
        public void OduzmiCrniZeton()
        {
            InicijalizirajProlog();
            int trenutniBroj = IspisiBrojCrnihZetona();
            int noviBroj = IspisiBrojCrnihZetona() - 1;
            PlQuery.PlCall("retract(brojCrnihZetona("+trenutniBroj+"))");
            PlQuery.PlCall("assert(brojCrnihZetona("+noviBroj+"))");
        }

      //funkcija koja smanjuje broj bijelih žetona za jedan
        public void OduzmiBijeliZeton()
        {
            InicijalizirajProlog();
            int trenutniBroj = IspisiBrojBijelihZetona();
            int noviBroj = IspisiBrojBijelihZetona() - 1;
            PlQuery.PlCall("retract(brojBijelihZetona(" + trenutniBroj + "))");
            PlQuery.PlCall("assert(brojBijelihZetona(" + noviBroj + "))");
        }

        //upit koji vrača trenutni broj maknutih crnih
        public int IspisiBrojMaknutihCrnihZetona()
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("brojMaknutihCrnihZetona(B)"))
            {
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    return int.Parse(v["B"].ToString());
                }
                return 0;
            }
        }
        
        //upit koji vrača trenutni broj maknutih bijelih
        public int IspisiBrojMaknutihBijelihZetona()
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("brojMaknutihBijelihZetona(B)"))
            {
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    return int.Parse(v["B"].ToString());
                }
                return 0;
            }
        }

        //funkcija koja povečava broj maknutih crnih žetona za jedan
        public void PovecajBrojMaknutihCrnihZetona()
        {
            InicijalizirajProlog();
            int trenutniBroj = IspisiBrojMaknutihCrnihZetona();
            int noviBroj = IspisiBrojMaknutihCrnihZetona() + 1;
            PlQuery.PlCall("retract(brojMaknutihCrnihZetona(" + trenutniBroj + "))");
            PlQuery.PlCall("assert(brojMaknutihCrnihZetona(" + noviBroj + "))");
        }

        //funkcija koja povečava broj maknutih bijelih žetona za jedan
        public void PovecajBrojMaknutihBijelihZetona()
        {
            InicijalizirajProlog();
            int trenutniBroj = IspisiBrojMaknutihBijelihZetona();
            int noviBroj = IspisiBrojMaknutihBijelihZetona() + 1;
            PlQuery.PlCall("retract(brojMaknutihBijelihZetona(" + trenutniBroj + "))");
            PlQuery.PlCall("assert(brojMaknutihBijelihZetona(" + noviBroj + "))");
        }

        //funkcija koja smanjuje broj početnih crnih žetona za maknute žetone iz prve faze
        public void SmanjiBrojPocetnihCrnihZetonaZaMaknute()
        {
            InicijalizirajProlog();
            int trenutniBroj = IspisiBrojCrnihZetona();
            int noviBroj = 9 - IspisiBrojMaknutihCrnihZetona();
            PlQuery.PlCall("retract(brojCrnihZetona(" + trenutniBroj + "))");
            PlQuery.PlCall("assert(brojCrnihZetona(" + noviBroj + "))");
        }

        //funkcija koja smanjuje broj početnih bijelih žetona za maknute žetone iz prve faze
        public void SmanjiBrojPocetnihBijelihZetonaZaMaknute()
        {
            InicijalizirajProlog();
            int trenutniBroj = IspisiBrojBijelihZetona();
            int noviBroj = 9 - IspisiBrojMaknutihBijelihZetona();
            PlQuery.PlCall("retract(brojBijelihZetona(" + trenutniBroj + "))");
            PlQuery.PlCall("assert(brojBijelihZetona(" + noviBroj + "))");
        }

        //funkcija koja vraća sve predikate u početno stanje
        public void ResetirajPredikateIgre()
        {
            PlQuery.PlCall("abolish(pocetnaPozicija/2)");
            PlQuery.PlCall("abolish(igracNaPotezu/1)");
            PlQuery.PlCall("abolish(brojCrnihZetona/1)");
            PlQuery.PlCall("abolish(brojBijelihZetona/1)");
            PlQuery.PlCall("abolish(brojMaknutihCrnihZetona/1)");
            PlQuery.PlCall("abolish(brojMaknutihBijelihZetona/1)");
            OcitajPredikateIgre();
        }

        
        //funkcija kreira predikat pocetnaPozicija(Igrac, Pozicija).
        //predikat sadrži igrača i poziciju na kojoj se nalazi njegov žeton
        public void UnosPozicijeZetona(int igrac, int pozicija)
        {
            InicijalizirajProlog();
            PlQuery.PlCall("assert(pocetnaPozicija(" + igrac.ToString() + ", " + pozicija.ToString() + "))");
        }

        //funkcija koja mijenja poziciju žetona igrača, a poziva se kad igrač postavi žeton na neku drugu poziciju
        public void AzuriranjePozicijeZetona(int igrac, int trenutna, int nova)
        {
            InicijalizirajProlog();
            PlQuery.PlCall("retract(pocetnaPozicija(" + igrac.ToString() + ", " + trenutna.ToString() + "))");
            PlQuery.PlCall("assert(pocetnaPozicija(" + igrac.ToString() + ", " + nova.ToString() + "))");
        }

        //funkcija koja briše predikat pocetnaPozicija(Igrac, Pozicija) pomoću retract- a
        //poziva se kad igrač formira mlin
        public void MakniZetonSaPloce(int igrac, int pozicija)
        {
            InicijalizirajProlog();
            PlQuery.PlCall("retract(pocetnaPozicija(" + igrac.ToString() + ", " + pozicija.ToString() + "))");
        }

        //upit koji provjerava da li se žeton može pomaknuti sa jedne pozicije na drugu
        //npr. ProvjeraPozicije(1,2). vraća true jer postoji pozicija(1,2).
        //npr. ProvjeraPozicije(1,20). vraća false jer nr postoji pozicija(1,20).
        public bool ProvjeraPozicije(int od, int prema)
        {
            InicijalizirajProlog();
                using (PlQuery q = new PlQuery("pozicija(O, P)"))
                {
                    foreach (PlQueryVariables v in q.SolutionVariables)
                    {
                        if(v["O"].ToString()==od.ToString() && v["P"].ToString() == prema.ToString())
                        {
                            return true;
                        }
                    }
                }
            return false;
        }

        //upit koji provjerava da li je neka pozicija na ploči već zauzeta (dakle na nju se tada ne može staviti žeton)
        //npr. neka postoji pocetnaPozicija(1,2). 
        //npr. ProvjeriZauzetostPolja(2). vraća false jer polje nije slobodno
        //npr. ProvjeriZauzetostPolja(3). vraća true jer je polje slobodno
        public bool ProvjeriZauzetostPolja(int prema)
        {

            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("pocetnaPozicija(I, P)"))
            {
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    if (v["P"].ToString() == prema.ToString())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //upit koji služi kao zaštita da igrač može pomaknuti samo onaj žeton koji je njegov
        //npr. neka je igracNaPotezu(1) i pocetnaPozicija(1,2).
        //npr. ProvjeraIgracaNaPotezu(1,2). tada bi vratila true
        //npr. ProvjeraIgracaNaPotezu(1,3). i ProvjeraIgracaNaPotezu(2,2). vraćaju false za gornji slučaj.
        public bool ProvjeraIgracaNaPotezu(int igrac, int pozicija)
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("pocetnaPozicija(I, P)"))
            {
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    if (v["P"].ToString() == pozicija.ToString() && v["I"].ToString()==igrac.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //upit koji provjerava da li je igrač formirao mlin
        //klauzula upita izgleda ovako: ProvjeriMlin(Igrac, Pozicija):- mlin(Pozicija, X, Y), pocetnaPozicija(Igrac, Pozicija), pocetnaPozicija(Igrac, X), pocetnaPozicija(Igrac, Y).
        //kako bi se formirao mlin isti igrač mora imati zauzete sve tri pozicije (pocetnaPozicija) koje odgovaraju predikatu: mlin
        public bool ProvjeriMlin(int igrac, int pozicija)
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("mlin(P,X,Y),pocetnaPozicija(I,P), pocetnaPozicija(I,X),pocetnaPozicija(I,Y)"))
            {
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    if (v["P"].ToString() == pozicija.ToString() && v["I"].ToString() == igrac.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //upit koji osigurava da igrač prilikom formiranja mlina ne ukloni vlastiti žeton
        //upit vraća false ako se pronađe pocetnaPozicija(I,P) gdje je I igrac na potezu, a P pozicija koju želi ukloniti
        public bool ZastitaOdUklanjanjaVlastitogZetona(int igrac, int pozicija)
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("pocetnaPozicija(I, P)"))
            {
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    if (v["P"].ToString() == pozicija.ToString() && v["I"].ToString()==igrac.ToString())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //upit koji osigurava da igrač ne ukloni "prazno polje"
        //upit provjerava da li postoji navedena pozicija u predikatu pocetnaPozicija.
        public bool ZastitaOdUklanjanjaPraznogPolja(int pozicija)
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("pocetnaPozicija(I, P)"))
            {
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    if (v["P"].ToString() == pozicija.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Upit koji provjerava ako je pobijedio igrač sa crnim žetonima
        public bool ProvjeriAkoJePobijedioCrni()
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("brojBijelihZetona(X), X < 3"))
            {
                if (q.NextSolution()) return true;
            }
            return false;
        }


        //Upit koji provjerava ako je pobijedio igrač sa bijelim žetonima
        public bool ProvjeriAkoJePobijedioBijeli()
        {
            InicijalizirajProlog();
            using (PlQuery q = new PlQuery("brojCrnihZetona(X), X < 3"))
            {
                if (q.NextSolution()) return true;
            }
            return false;
        }
    }
}
