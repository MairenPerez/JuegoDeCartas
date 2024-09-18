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

            foreach (Carta.EPalo epalo in Enum.GetValues(typeof(Carta.EPalo)))
            {
                for (int i = 1; i <= 13; i++)
                    cartas.Add(new Carta(i, epalo));
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
        /// </summary>
        /// <returns>Carta Primera Posición</returns>
        public Carta RobarCarta()
        {
            return RobarEnPosicionN(0); 
        }

        /// <summary>
        /// Robamos una carta al azar
        /// </summary>
        /// <returns> Carta Robada en x Posición</returns>
        public Carta RobarAlAzar()
        {
            Random rdm = new Random();
            int indiceRandom = rdm.Next(0, cartas.Count);
            
            return RobarEnPosicionN(indiceRandom);
        }

        /// <summary>
        /// Robamos la carta según la posicion
        /// del jugador.
        /// </summary>
        /// <param name="posicion"></param>
        /// <returns>Carta Robada</returns>
        public Carta RobarEnPosicionN(int posicion)
        {
            return RobarEnPosicion(posicion);
        }

        /// <summary>
        /// Miramos la posición de la carta
        /// </summary>
        /// <param name="posicion"></param>
        /// <returns>Carta Robada</returns>
        /// <exception cref="Exception"></exception>
        private Carta RobarEnPosicion(int posicion)
        {
            if (posicion < 0 || posicion >= cartas.Count)
                throw new Exception("Posición no válida");

            Carta cartaRobada = cartas[posicion];
            cartas.RemoveAt(posicion);
            return cartaRobada;
        }
    }
}
