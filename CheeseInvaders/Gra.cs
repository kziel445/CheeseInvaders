using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using System.Diagnostics;

namespace SpaceCheese
{
    class Gra
    {
        internal int yRozmiar = 20, xRozmiar;
        internal int zycie, tempo;
        public static Stopwatch Timer = new Stopwatch();
        public static int time;
        public static ConsoleKeyInfo key;
        public static bool isKeyPressed = false;
        public List<PociskStatek> pociskiGracza = new List<PociskStatek>();
        public List<PociskObcy> pociskiObcych = new List<PociskObcy>();
        public List<Obcy> listaObcych = new List<Obcy>();
        Random kierunek = new Random();
        internal long punkty=0;
        Obiekty statek;
        public int numerEtapu = 1;
        private int liczbaObcych=0;
        public Gra(int hp = 10, int x = 30, int tempo = 300)//Domyslnie latwy poziom
        {
            this.xRozmiar = x;
            this.zycie = hp;
            this.tempo = tempo;
        }
            public void Wyswietl()
        {
            Mapa mapa = new Mapa(xRozmiar,yRozmiar);
            statek = new Statek();
            PasekStatystyk.Wyswietl(zycie, punkty);
            mapa.TworzenieMapy();
            statek.WyswietlObiekt();
        }
        public void Update()
        {
            Timer.Start();
            
            while (true)
            {
                time = (int)Timer.ElapsedMilliseconds;
                if (time > tempo)
                {
                    time = 0;
                    Timer.Restart();
                    //Rozpatrzenie lotu pociskowGracza
                    if (pociskiGracza.Count>0)   
                    {
                        for (int i= 0;i< pociskiGracza.Count; i++)              //rozbic na funkcje
                        {
                            if (pociskiGracza[i].y == statek.y && pociskiGracza[i].x == statek.x)   //kontakt ze statkiem == boom
                            {
                                zycie--;
                                if (zycie <= 0)//koniec gry i ponow gre
                                {
                                    PasekStatystyk.Wyswietl(zycie, punkty);
                                    return;
                                }   
                                PasekStatystyk.Wyswietl(zycie,punkty);
                                pociskiGracza[i].WymarzObiekt();
                                pociskiGracza.RemoveAt(i);
                                statek.WyswietlObiekt();
                            }
                            else if (pociskiGracza[i].y > 4) pociskiGracza[i].Lot(); //lot pocisku
                            else if (pociskiGracza[i].y == 4) //jezeli wyleca z planszy to znikaja
                            {
                                pociskiGracza[i].WymarzObiekt();
                                pociskiGracza.RemoveAt(i);
                            }
                            punkty = Kontakt.KontaktPociskObcy(pociskiGracza, listaObcych,punkty);
                            PasekStatystyk.Wyswietl(zycie, punkty);
                            //obcy moze zostac trafiony i znika jezeli zycie=0 w pociskach
                        }
                    }
                    //Rozpatrzenie ruchu Obcych
                    if(listaObcych.Count>0)
                    {
                        for(int i=0;i<listaObcych.Count;i++)
                        {
                            listaObcych[i].WyswietlObiekt();
                            int rnd = kierunek.Next(0, 1249);//zrobic metode
                            if (rnd > 0 && rnd < 249) Ruch.RuchLewo(listaObcych[i]);
                            if (rnd > 250 && rnd < 499) Ruch.RuchPrawo(listaObcych[i], xRozmiar);
                            if (rnd > 500 && rnd < 749) Ruch.RuchGora(listaObcych[i]);
                            if (rnd > 750 && rnd < 999) Ruch.RuchDol(listaObcych[i], yRozmiar);
                            if (rnd > 1000 && rnd < 1249)
                            {
                                if (listaObcych[i].y == yRozmiar + 1) return;
                                pociskiObcych.Add(new PociskObcy(listaObcych[i].x, listaObcych[i].y));

                                pociskiObcych[pociskiObcych.Count - 1].WyswietlObiekt();
                            }
                        }
                    }
                    if (pociskiObcych.Count > 0)
                    {
                        for (int i = 0; i < pociskiObcych.Count; i++)              
                        {
                            if (pociskiObcych[i].y == statek.y && pociskiObcych[i].x == statek.x)   //kontakt ze statkiem 
                            {
                                zycie--;
                                if (zycie <= 0)//koniec gry i ponow gre
                                {
                                    PasekStatystyk.Wyswietl(zycie, punkty);
                                    return;
                                }
                                PasekStatystyk.Wyswietl(zycie, punkty);
                                pociskiObcych[i].WymarzObiekt();
                                pociskiObcych.RemoveAt(i);
                                statek.WyswietlObiekt();
                            }
                            else if (pociskiObcych[i].y < yRozmiar + 1) pociskiObcych[i].Lot(); //lot pocisku
                            else if (pociskiObcych[i].y == yRozmiar+1) //jezeli wyleca z planszy to znikaja
                            {
                                pociskiObcych[i].WymarzObiekt();
                                pociskiObcych.RemoveAt(i);
                            }
                            punkty = Kontakt.KontaktPociskObcy(pociskiGracza, listaObcych, punkty);
                            PasekStatystyk.Wyswietl(zycie, punkty);
                            //obcy moze zostac trafiony i znika jezeli zycie=0 w pociskach
                        }
                    }
                    if (listaObcych.Count==0)
                    {
                        Etap NumerEtapu = new Etap();
                        liczbaObcych = (int)Math.Pow(2,numerEtapu - 1);
                        NumerEtapu.Spawn(listaObcych, liczbaObcych,xRozmiar);
                        numerEtapu++;
                    }
                }
                Odczytaj();
            }
        }
        private void Odczytaj()
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey();
                isKeyPressed = true;
            }
            else isKeyPressed = false;

            if (key.Key == ConsoleKey.LeftArrow && isKeyPressed && Kontakt.KontaktStatekObcy(listaObcych, statek.x-1, statek.y))
            {
                Ruch.RuchLewo(statek);
            }
            else if (key.Key == ConsoleKey.RightArrow && isKeyPressed && Kontakt.KontaktStatekObcy(listaObcych, statek.x + 1, statek.y))
            {
                Ruch.RuchPrawo(statek, xRozmiar);
            }
            else if(key.Key == ConsoleKey.UpArrow && isKeyPressed && Kontakt.KontaktStatekObcy(listaObcych, statek.x, statek.y-1))
            {
                Ruch.RuchGora(statek);
            }
            else if(key.Key == ConsoleKey.DownArrow && isKeyPressed && Kontakt.KontaktStatekObcy(listaObcych, statek.x, statek.y-1))
            {
                Ruch.RuchDol(statek, yRozmiar);
            }
            else if(key.Key == ConsoleKey.Spacebar && isKeyPressed)
            {
                if (statek.y == 4) return;
                pociskiGracza.Add(new PociskStatek(statek.x, statek.y)); //PociskStatek(statek.x,statek.y);
                pociskiGracza[pociskiGracza.Count-1].WyswietlObiekt();
            }
        }
    }
    class Mapa
    {
        protected string[,] wspolrzedne;//do zmiany
        public Mapa(int x,int y)// tylko x i y potrzebny
        {
            wspolrzedne = new string[x,y];
        }
        public void TworzenieMapy()
        {
            for (int i=0;i< wspolrzedne.GetLength(0); i++)
            {
                for (int j = 0; j < wspolrzedne.GetLength(1);j++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow ;
                    if (j==0)//&& i % 2 == 0
                    {
                        Console.SetCursorPosition(i, j + 3);
                        Console.Write("▄");
                    }
                    else if(j == wspolrzedne.GetLength(1) - 1)
                    {
                        Console.SetCursorPosition(i, j + 3);
                        Console.Write("▀");
                    }
                    else if ((i== 0 || i== wspolrzedne.GetLength(0)-1))
                    {
                        Console.SetCursorPosition(i, j + 3);
                        Console.Write("█");
                    }
                    else wspolrzedne[i, j] = " ";
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }
        public void Wyswietl()//nie uzywane
        {
            for (int i = 0; i < wspolrzedne.GetLength(0); i++)
            {
                for (int j = 0; j < wspolrzedne.GetLength(1); j++)
                {
                    Console.Write(wspolrzedne[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
    class Kontakt:Gra
    {
        public static long KontaktPociskObcy(List<PociskStatek> pociski,List<Obcy> obcy,long punkty)
        {
            for(int i=0;i<pociski.Count;i++)
            {
                for (int j = 0; j < obcy.Count; j++)
                {
                    if (pociski[i].y == obcy[j].y && pociski[i].x == obcy[j].x)
                    {
                        pociski[i].WymarzObiekt();
                        obcy[j].WymarzObiekt();
                        pociski.RemoveAt(i);
                        obcy.RemoveAt(j);
                        punkty += 100;
                        KontaktPociskObcy(pociski, obcy,punkty);
                        return punkty;
                    }

                }
            }
            return punkty;
        }
        public static bool KontaktStatekObcy(List<Obcy> obcy,int x,int y)       //obcy nie moze latac na pole zajete przez statek
        {
            for (int j = 0; j < obcy.Count; j++)
            {
                if (y == obcy[j].y && x == obcy[j].x)
                {
                    return false;
                }
            }
            return true;
        }
    }
    class Ruch:Gra
    {
        public static void RuchLewo(Obiekty obiekt)
        {
            if (obiekt.x == 2) return;
            obiekt.WymarzObiekt();
            obiekt.x--;
            obiekt.WyswietlObiekt();
        }
        public static void RuchPrawo(Obiekty obiekt,int x)
        {

            if (obiekt.x == x-3) return;
            obiekt.WymarzObiekt();
            obiekt.x++;
            obiekt.WyswietlObiekt();
        }
        public static void RuchGora(Obiekty obiekt)
        {
            if (obiekt.y == 4) return;
            obiekt.WymarzObiekt();
            obiekt.y--;
            obiekt.WyswietlObiekt();
        }
        public static void RuchDol(Obiekty obiekt,int y)
        {
            if (obiekt.y == y+1) return;
            obiekt.WymarzObiekt();
            obiekt.y++;
            obiekt.WyswietlObiekt();
        }
    }
    class PasekStatystyk
    {
        public static void Wyswietl(int zycie,long punkty)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("---------------------------");
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  Zycie: {0}     " , zycie);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Punkty: {0}   ", punkty);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("---------------------------");
            Console.SetCursorPosition(27,0);
        }
    }
}
