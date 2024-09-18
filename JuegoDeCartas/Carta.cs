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
        private EPalo epalo;

        public int NumeroCarta
        {
            get { return numeroCarta; }
            set { numeroCarta = value; }
        }

        public enum EPalo { Oros, Copas, Espadas, Bastos }

        public Carta() { }

        public Carta(int numeroCarta, EPalo epalo)
        {
            this.numeroCarta = numeroCarta;
            this.epalo = epalo;
        }

        public override string ToString()
        {
            return numeroCarta + " de " + epalo;
        }
    }
}
