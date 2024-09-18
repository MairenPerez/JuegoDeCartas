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
            Console.WriteLine();

            int numJugadores = SeleccionarNumJugadores();

            Baraja baraja = new Baraja();

            Console.WriteLine("El juego ha comenzado con " + numJugadores + " jugadores.");

            Console.WriteLine();

            List<List<Carta>> manosJugadores = RepartirCartas(numJugadores, baraja);

            IniciarBatalla(manosJugadores);
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
            if (cartasRestantes > 0)
                Console.WriteLine($"Han sobrado {cartasRestantes} cartas en la baraja.");

            return manosJugadores;
        }

        private static void IniciarBatalla(List<List<Carta>> manosJugadores)
        {
            bool juegoTerminado = false;
            int turnoActual = 0;
            int[] score = new int[manosJugadores.Count]; // Controlamos las rondas de cada jugador

            while (!juegoTerminado)
            {
                List<Carta> cartasJugadas = new List<Carta>();
                Console.WriteLine("Ronda Actual: " + turnoActual);

                // Cada jugador juega sus cartas
                for (int i = 0; i < manosJugadores.Count; i++)
                {
                    List<Carta> manoActual = manosJugadores[i];
                    Console.WriteLine($"Jugador {i + 1}, selecciona una carta para jugar (1-{manoActual.Count}):");
                    for (int j = 0; j < manoActual.Count; j++)
                        Console.WriteLine($"{j + 1}. {manoActual[j]}");

                    int seleccion = int.Parse(Console.ReadLine()) - 1;
                    Carta cartaJugada = manoActual[seleccion];
                    manoActual.RemoveAt(seleccion); // Eliminamos la carta de la mano
                    cartasJugadas.Add(cartaJugada); // Añadimos la carta jugada
                    Console.WriteLine($"Jugador {i + 1} ha jugado: {cartaJugada}");
                }

                // Miramos cual es la carta más alta 
                Carta cartaGanadora = cartasJugadas[0];
                int jugadorGanador = 0;
                for (int i = 1; i < cartasJugadas.Count; i++)
                {
                    if (cartasJugadas[i].NumeroCarta > cartaGanadora.NumeroCarta)
                    {
                        cartaGanadora = cartasJugadas[i];
                        jugadorGanador = i;
                    }
                }

                // Incrementar el puntaje del jugador ganador
                score[jugadorGanador]++;
                Console.WriteLine($"Jugador {jugadorGanador + 1} gana la ronda con la carta {cartaGanadora}.");

                // Verificar si el juego ha terminado
                juegoTerminado = manosJugadores.All(mano => mano.Count == 0);
                turnoActual++;

            }

            // Mostrar el puntaje final
            Console.WriteLine("El juego ha terminado. Puntajes finales:");
            for (int i = 0; i < score.Length; i++)
            {
                Console.WriteLine($"Jugador {i + 1}: {score[i]} puntos");
            }
        }
    }
}
