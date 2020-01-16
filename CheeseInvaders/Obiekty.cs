using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SpaceCheese
{
    abstract class Obiekty:Gra
    {
        internal int x;
        internal int y;
        internal char symbol;
        public ConsoleColor kolor = ConsoleColor.White;
        public void WymarzObiekt()
        {
            //kolor = ConsoleColor.White;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(' ');
        }
        public void WyswietlObiekt()
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = kolor;
            Console.Write(symbol);
        }
    }
    class Statek:Obiekty//Statek jako gracz
    {
        public Statek()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            symbol = '\u25B2';
            x = xRozmiar/2;
            y = yRozmiar+1;
            kolor = ConsoleColor.Cyan;
        }
    }
    class Obcy:Obiekty 
    {
        //int hpObcy=1;
        
        public Obcy(int x, int y)
        {
            symbol = '¤';
            this.x = x;
            this.y = y;
            kolor = ConsoleColor.Green;
        }
    }
    class PociskObcy : Obiekty
    {
        
        public PociskObcy(int x, int y)
        {
            symbol = '║';//▒
            this.x = x;
            this.y = y+1;
            kolor = ConsoleColor.Magenta;
        }
        public void Lot()
        {
            WymarzObiekt();
            y++;
            WyswietlObiekt();
        }
    }
    class PociskStatek : Obiekty
    {
        public PociskStatek(int x,int y)
        {
            symbol = '│';
            this.x = x;
            this.y = y-1;
            kolor = ConsoleColor.Red;
        }
        public void Lot()
        {
            WymarzObiekt();
            y--;
            WyswietlObiekt();
        }
    }

}
    

