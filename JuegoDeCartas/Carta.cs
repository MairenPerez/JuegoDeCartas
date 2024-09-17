using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeCartas
{
    public class Carta
    {
        private int numeroCarta;
        private Palo palo;

        public enum Palo { Oros, Copas, Espadas, Bastos }

        public Carta() { }

        public Carta(int numeroCarta, Palo palo)
        {
            this.numeroCarta = numeroCarta;
            this.palo = palo;
        }

        public int NumeroCarta
        {
            get { return numeroCarta; }
            set { numeroCarta = value; }
        }
    }
}
