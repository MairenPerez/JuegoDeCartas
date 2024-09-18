using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeCartas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido al Juego de Cartas!");
            int numJugadores = SeleccionarNumJugadores();

            Baraja baraja = new Baraja();

            Console.WriteLine("El juego ha comenzado con " + numJugadores + " jugadores.");

            Console.WriteLine();

            RepartirCartas(numJugadores, baraja);

        }

        /// <summary>
        /// Selección de jugadores
        /// que intervendrán en la 
        /// partida
        /// </summary>
        /// <returns>numJugadores</returns>
        private static int SeleccionarNumJugadores()
        {
            int numJugadores = 0;

            while (numJugadores < 2 || numJugadores > 5)
            {
                Console.WriteLine("Selecciona el número de jugadores (2-5)");
                string input = Console.ReadLine();
                if (int.TryParse(input, out numJugadores) && numJugadores >= 2 && numJugadores <= 5)
                    return numJugadores;
                else
                    Console.WriteLine("Por favor, introduce un número válido de jugadores.");
            }
            return numJugadores;
        }

        // Método para repartir las cartas a los jugadores 
        private static void RepartirCartas(int numJugadores, Baraja baraja)
        {
            // 48 cartas para repartir según el número de jugadores
            int numCartasPorJugador = 48 / numJugadores;

            for (int i = 0; i < numJugadores; i++)
            {
                Console.WriteLine("Jugador " + (i + 1) + " tiene las siguientes cartas:");
                for (int j = 0; j < numCartasPorJugador; j++)
                {
                    Carta carta = baraja.RobarCarta();
                    Console.WriteLine(carta);
                }
                Console.ReadLine();
            }
        }
    }
}
