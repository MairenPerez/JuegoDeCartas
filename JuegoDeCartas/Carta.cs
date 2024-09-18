using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeCartas
{
    public class Carta : IComparable<Carta>
    {
        public int NumeroCarta { get; set; }
        public EPalo Palo { get; set; }

        public enum EPalo { Oros, Copas, Espadas, Bastos }

        public Carta() { }

        public Carta(int numeroCarta, EPalo palo)
        {
            NumeroCarta = numeroCarta;
            Palo = palo;
        }

        public override string ToString()
        {
            return NumeroCarta + " de " + Palo;
        }

        public int CompareTo(Carta other)
        {
            return NumeroCarta.CompareTo(other.NumeroCarta);
        }
    }
}
