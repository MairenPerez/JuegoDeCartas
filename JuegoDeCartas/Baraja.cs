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
        private List<Carta> cartas;

        public Baraja()
        {
            cartas = new List<Carta>();

            foreach (Carta.EPalo epalo in Enum.GetValues(typeof(Carta.EPalo)))
            {
                for (int i = 1; i <= 13; i++)
                {
                    Carta carta = new Carta(i, epalo);
                    cartas.Add(carta);
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
        /// </summary>
        /// <returns>Carta en la primera posición</returns>
        public Carta RobarCarta()
        {
            return RobarEnPosicion(0);
        }

        /// <summary>
        /// Robamos una carta al azar
        /// </summary>
        /// <returns>Carta robada en una posición aleatoria</returns>
        public Carta RobarAlAzar()
        {
            Random rdm = new Random();
            int indiceRandom = rdm.Next(0, cartas.Count);

            return RobarEnPosicion(indiceRandom);
        }

        /// <summary>
        /// Robamos la carta en la posición especificada
        /// </summary>
        /// <param name="posicion">Posición de la carta a robar</param>
        /// <returns>Carta robada</returns>
        public Carta RobarEnPosicion(int posicion)
        {
            if (posicion < 0 || posicion >= cartas.Count)
                throw new Exception("Posición no válida");

            Carta cartaRobada = cartas[posicion];
            cartas.RemoveAt(posicion);
            return cartaRobada;
        }

        /// <summary>
        /// Devuelve el número de cartas disponibles en la baraja
        /// </summary>
        /// <returns>Total de cartas restantes</returns>
        public int CartasRestantes()
        {
            return cartas.Count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Carta carta in cartas)
                sb.AppendLine(carta.ToString());
            return sb.ToString();
        }
    }
}
