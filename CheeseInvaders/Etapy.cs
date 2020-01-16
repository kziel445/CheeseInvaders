using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCheese
{
    class Etap
    {
        Random kierunek = new Random();
        public void Spawn(List<Obcy>lista, int liczbaObcych,int y)
        {
            if(liczbaObcych<=y/2)
            {
                for (int i = 0; i < liczbaObcych; i++)
                {
                    lista.Add(new Obcy((y / (liczbaObcych + 1)) * (i + 1), 4));
                }
            }
            else
            {
                for (int i = 0; i < y-4; i++)
                {
                    lista.Add(new Obcy((y / (liczbaObcych + 1)) * (i + 1), 4));
                }
            }
        }
    }
}
