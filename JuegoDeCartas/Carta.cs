using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeCartas
{
    public class Carta
    {
        int numeroCarta;
        public enum Palo { Oros, Copas, Espadas, Bastos }; // Changed to public

        public Carta() { }

        public Carta(int numeroCarta)
        {
            this.numeroCarta = numeroCarta;
        }

    }
}
