using JuegoDeCartas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeCartas
{
    public class Baraja
    {
        private List<Carta> cartas = new List<Carta>();

        public Baraja()
        {
            cartas = new List<Carta>();

            foreach (Carta.Palo palo in Enum.GetValues(typeof(Carta.Palo)))
            {
                for (int i = 1; i  < 13; i++)
                {
                    cartas.Add(new Carta(i, palo));
                }
            }

            Barajar();

        }

        public void Barajar()
        {
            Random rdm = new Random();
            cartas = cartas.OrderBy(c => rdm.Next(rdm.Next())).ToList(); // Ordenamos la lista de cartas de forma aleatoria


        }

        public Carta RobarCarta()
        {
            // Implementación para robar la primera carta
            return null;
        }

        public Carta RobarAlAzar()
        {
            // Implementación para robar una carta al azar
            return null;
        }

        public Carta RobarEnCualquierPos(int posicion)
        {
            // Implementación para robar una carta en una posición específica
            return null;
        }
    }
}
