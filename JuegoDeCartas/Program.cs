using System;
using System.Collections.Generic;

namespace JuegoDeCartas
{
    internal class Program
    {
        /// <summary>
        /// Método principal que inicia el juego.
        /// </summary>
        static void Main(string[] args)
        {
            bool jugarDeNuevo = true;

            while (jugarDeNuevo)
            {
                Console.WriteLine("Bienvenido al Juego de Cartas!");
                Console.WriteLine();

                int numJugadores = SeleccionarNumJugadores();

                Baraja baraja = new Baraja();
                baraja.Barajar();

                Console.WriteLine("El juego ha comenzado con " + numJugadores + " jugadores.");
                Console.WriteLine();

                List<Queue<Carta>> manosJugadores = RepartirCartas(numJugadores, baraja);

                IniciarBatalla(manosJugadores, baraja);

                Console.WriteLine("¿Quieres jugar otra partida? (s/n)");
                string respuesta = Console.ReadLine();
                jugarDeNuevo = respuesta.ToLower() == "s";
            }
        }

        /// <summary>
        /// Selecciona el número de jugadores para el juego.
        /// </summary>
        /// <returns>Número de jugadores</returns>
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
        /// Reparte las cartas entre los jugadores.
        /// </summary>
        /// <param name="numJugadores">Número de jugadores</param>
        /// <param name="baraja">Baraja de cartas</param>
        /// <returns>Lista de manos de jugadores</returns>
        private static List<Queue<Carta>> RepartirCartas(int numJugadores, Baraja baraja)
        {
            int numCartaPorJug = 48 / numJugadores;
            List<Queue<Carta>> manosJugadores = new List<Queue<Carta>>();

            for (int i = 0; i < numJugadores; i++)
            {
                Queue<Carta> mano = new Queue<Carta>();
                for (int j = 0; j < numCartaPorJug; j++)
                {
                    Carta carta = baraja.RobarCarta();
                    mano.Enqueue(carta);
                }
                manosJugadores.Add(mano);
            }

            int cartasRestantes = 48 % numJugadores;
            for (int i = 0; i < cartasRestantes; i++)
            {
                Carta carta = baraja.RobarCarta();
                manosJugadores[i].Enqueue(carta);
            }

            return manosJugadores;
        }

        /// <summary>
        /// Inicia la batalla entre los jugadores.
        /// </summary>
        /// <param name="manosJugadores">Manos de los jugadores</param>
        /// <param name="baraja">Baraja de cartas</param>
        private static void IniciarBatalla(List<Queue<Carta>> manosJugadores, Baraja baraja)
        {
            bool juegoTerminado = false;
            int[] score = new int[manosJugadores.Count];
           
            while (!juegoTerminado)
            {
                List<Carta> cartasJugadas = new List<Carta>();
                Console.WriteLine("Ronda actual:");

                for (int i = 0; i < manosJugadores.Count; i++)
                {
                    Queue<Carta> manoActual = manosJugadores[i];
                    if (manoActual.Count == 0)
                    {
                        Console.WriteLine($"Jugador {i + 1} se ha quedado sin cartas y pierde.");
                        juegoTerminado = true;
                        break;
                    }

                    Console.WriteLine($"Jugador {i + 1}, elige una opción:");
                    Console.WriteLine("1. Jugar carta");
                    Console.WriteLine("2. Robar una carta");
                    string opcion = Console.ReadLine();

                    if (opcion == "2")
                    {
                        if (baraja.CartasRestantes() == 0)
                        {
                            Console.WriteLine("No hay cartas disponibles para robar.");
                            continue;
                        }

                        Carta cartaRobada = baraja.RobarCarta();
                        manoActual.Enqueue(cartaRobada);
                        Console.WriteLine($"Has robado: {cartaRobada}");
                    }

                    Carta cartaJugada = manoActual.Dequeue();
                    cartasJugadas.Add(cartaJugada);
                    Console.WriteLine($"Jugador {i + 1} ha jugado: {cartaJugada}");
                }

                if (juegoTerminado) break;

                int jugadorGanador = DeterminarGanador(cartasJugadas, out bool empate);
                if (empate)
                    Console.WriteLine("La ronda ha terminado en empate.");
                else
                {
                    score[jugadorGanador]++;
                    Console.WriteLine($"La carta ganadora es: {cartasJugadas[jugadorGanador]} del Jugador {jugadorGanador + 1}");
                    foreach (var carta in cartasJugadas)
                        manosJugadores[jugadorGanador].Enqueue(carta);
                }
                juegoTerminado = VerificarJuegoTerminado(manosJugadores);
            }

            MostrarPuntajesFinales(score);
        }

        /// <summary>
        /// Determina el ganador de la ronda.
        /// </summary>
        /// <param name="cartasJugadas">Cartas jugadas en la ronda</param>
        /// <param name="empate">Indica si hubo empate</param>
        /// <returns>Índice del jugador ganador</returns>
        private static int DeterminarGanador(List<Carta> cartasJugadas, out bool empate)
        {
            Carta cartaGanadora = cartasJugadas[0];
            int jugadorGanador = 0;
            empate = false;

            for (int i = 1; i < cartasJugadas.Count; i++)
            {
                int comparacion = cartasJugadas[i].CompareTo(cartaGanadora);
                if (comparacion > 0)
                {
                    cartaGanadora = cartasJugadas[i];
                    jugadorGanador = i;
                    empate = false;
                }
                else if (comparacion == 0)
                {
                    empate = true;
                }
            }

            return jugadorGanador;
        }

        /// <summary>
        /// Verifica si el juego ha terminado.
        /// </summary>
        /// <param name="manosJugadores">Manos de los jugadores</param>
        /// <returns>True si el juego ha terminado, de lo contrario False</returns>
        private static bool VerificarJuegoTerminado(List<Queue<Carta>> manosJugadores)
        {
            int jugadoresConCartas = 0;

            foreach (var mano in manosJugadores)
            {
                if (mano.Count > 0)
                    jugadoresConCartas++;
            }

            return jugadoresConCartas <= 1;
        }

        /// <summary>
        /// Muestra los puntajes finales de los jugadores.
        /// </summary>
        /// <param name="score">Array de puntajes</param>
        private static void MostrarPuntajesFinales(int[] score)
        {
            Console.WriteLine("El juego ha terminado. Score final:");
            for (int i = 0; i < score.Length; i++)
                Console.WriteLine($"Jugador {i + 1}: {score[i]} puntos");

            Console.ReadLine();
        }
    }
}
