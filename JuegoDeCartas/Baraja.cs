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
                for (int i = 1; i <= 13; i++)
                {
                    cartas.Add(new Carta(i, palo));
                }
            }

            Barajar();
        }

        /// <summary>
        /// Aleatoriamente barajamos las cartas
        /// Usamos el algoritmo Fisher-Yates Shuffle.
        /// </summary>
        public void Barajar()
        {
            Random rdm = new Random();

            int n = cartas.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rdm.Next(i + 1);
                Carta temp = cartas[i];
                cartas[i] = cartas[j];
                cartas[j] = temp;
            }
        }

        /// <summary>
        /// Robamos la primera carta de la baraja
        /// 
        /// Si no hay ninguna carta, lanzamos
        /// una excepción
        /// </summary>
        /// <returns>Carta Robada</returns>
        /// <exception cref="Exception"></exception>
        public Carta RobarCarta()
        {
            if (cartas.Count == 0)
                throw new Exception("No hay cartas en la baraja");

            Carta cartaRobada = cartas[0];
            cartas.RemoveAt(0);
            return cartaRobada;
        }

        /// <summary>
        /// Robamos una carta al azar
        /// 
        /// Si no hay ninguna carta a robar,
        /// lanzamos una excepción
        /// </summary>
        /// <returns>Carta Robada</returns>
        /// <exception cref="Exception"></exception>
        public Carta RobarAlAzar()
        {
            if (cartas.Count == 0)
                throw new Exception("No hay cartas en la baraja");

            Random rdm = new Random();
            int indiceRandom = rdm.Next(0, cartas.Count);
            Carta cartaRobada = cartas[indiceRandom];
            cartas.RemoveAt(indiceRandom);

            return cartaRobada;
        }

        /// <summary>
        /// Robamos la carta según la posicion
        /// del jugador.
        /// 
        /// Si la posición no es válida o directamente
        /// no hay más cartas, lanzamos una excepción.
        /// </summary>
        /// <param name="posicion"></param>
        /// <returns>Carta Robada</returns>
        /// <exception cref="Exception"></exception>
        public Carta RobarEnPosicionN(int posicion)
        {
            if (cartas.Count == 0)
                throw new Exception("No hay cartas en la baraja");

            if (posicion < 0 || posicion >= cartas.Count)
                throw new Exception("Posición no válida");

            Carta cartaRobada = cartas[posicion];

            cartas.RemoveAt(posicion);

            return cartaRobada;
        }
    }
}
