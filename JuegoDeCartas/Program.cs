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

            List<List<Carta>> manosJugadores = RepartirCartas(numJugadores, baraja);

            // Iniciamos el juego una vez se hayan repartido las cartas

            IniciarBatalla();
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

        /// <summary>
        /// Según las cartas que disponga el
        /// jugador, se reparten las cartas
        /// </summary>
        /// <param name="numJugadores"></param>
        /// <param name="baraja"></param>
        private static List<List<Carta>> RepartirCartas(int numJugadores, Baraja baraja)
        {
            int numCartaPorJug = 48 / numJugadores;
            List<List<Carta>> manosJugadores = new List<List<Carta>>();

            for (int i = 0; i < numJugadores; i++)
            {
                List<Carta> mano = new List<Carta>();
                for (int j = 0; j < numCartaPorJug; j++)
                {
                    Carta carta = baraja.RobarCarta();
                    mano.Add(carta);
                }
                manosJugadores.Add(mano);
            }

            // Si han sobrado cartas, las dejamos en la bara
            int cartasRestantes = 48 % numJugadores;
            if ( cartasRestantes > 0)
                Console.WriteLine($"Han sobrado {cartasRestantes} cartas en la baraja.");

            return manosJugadores;
        }



        private static void IniciarBatalla()
        {
            bool juegoTerminado = false;
            int turnoActual = 0;

            while (!juegoTerminado)
            {
                Console.WriteLine($"Turno del jugador {turnoActual + 1}");



            }
        }
    }
}
