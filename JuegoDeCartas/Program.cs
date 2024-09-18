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
                Console.WriteLine("Selecciona el número de jugadores (2-5) ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out numJugadores) && numJugadores >= 2 && numJugadores <= 5)
                    return numJugadores;
                else
                    Console.WriteLine("Por favor, introduce un número válido de jugadores.");
            }
            return numJugadores;
        }
    }
}
