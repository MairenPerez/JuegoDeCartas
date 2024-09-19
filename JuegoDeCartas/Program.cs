using System;
using System.Collections.Generic;

namespace JuegoDeCartas
{
    internal class Program
    {
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

                List<List<Carta>> manosJugadores = RepartirCartas(numJugadores, baraja);

                IniciarBatalla(manosJugadores, baraja);

                Console.WriteLine("¿Quieres jugar otra partida? (s/n)");
                string respuesta = Console.ReadLine();
                jugarDeNuevo = respuesta.ToLower() == "s";
            }
        }

        /// <summary>
        /// Permite al usuario seleccionar el número de jugadores.
        /// </summary>
        /// <returns>Número de jugadores seleccionados.</returns>
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
        /// <param name="numJugadores">Número de jugadores.</param>
        /// <param name="baraja">Baraja de cartas.</param>
        /// <returns>Lista de manos de los jugadores.</returns>
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

            int cartasRestantes = 48 % numJugadores;
            if (cartasRestantes > 0)
                Console.WriteLine($"Han sobrado {cartasRestantes} cartas en la baraja.");

            return manosJugadores;
        }

        /// <summary>
        /// Inicia la batalla entre los jugadores.
        /// </summary>
        /// <param name="manosJugadores">Manos de los jugadores.</param>
        /// <param name="baraja">Baraja de cartas.</param>
        private static void IniciarBatalla(List<List<Carta>> manosJugadores, Baraja baraja)
        {
            bool juegoTerminado = false;
            int[] score = new int[manosJugadores.Count];

            while (!juegoTerminado)
            {
                List<Carta> cartasJugadas = new List<Carta>();
                Console.WriteLine("Ronda actual:");

                for (int i = 0; i < manosJugadores.Count; i++)
                {
                    List<Carta> manoActual = manosJugadores[i];
                    if (manoActual.Count == 0)
                    {
                        if (baraja.CartasRestantes() > 0)
                        {
                            manoActual.Add(baraja.RobarCarta());
                            Console.WriteLine($"Jugador {i + 1} ha robado una carta.");
                        }
                        else
                        {
                            Console.WriteLine($"Jugador {i + 1} se ha quedado sin cartas y pierde.");
                            continue;
                        }
                    }

                    Carta cartaJugada = SeleccionarCarta(manoActual, i);
                    cartasJugadas.Add(cartaJugada);
                }

                int jugadorGanador = DeterminarGanador(cartasJugadas, out bool empate);
                if (empate)
                {
                    Console.WriteLine("La ronda ha terminado en empate.");
                }
                else
                {
                    score[jugadorGanador]++;
                    Console.WriteLine($"La carta ganadora es: {cartasJugadas[jugadorGanador]} del Jugador {jugadorGanador + 1}");
                }

                juegoTerminado = VerificarJuegoTerminado(manosJugadores);
            }

            MostrarPuntajesFinales(score);
        }

        /// <summary>
        /// Permite al jugador seleccionar una carta para jugar.
        /// </summary>
        /// <param name="manoActual">Mano actual del jugador.</param>
        /// <param name="jugador">Índice del jugador.</param>
        /// <returns>Carta seleccionada.</returns>
        private static Carta SeleccionarCarta(List<Carta> manoActual, int jugador)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"Jugador {jugador + 1}, selecciona una carta para jugar (1-{manoActual.Count}):");
                    for (int j = 0; j < manoActual.Count; j++)
                        Console.WriteLine($"{j + 1}: {manoActual[j]}");

                    int seleccion = int.Parse(Console.ReadLine()) - 1;
                    if (seleccion < 0 || seleccion >= manoActual.Count)
                        throw new ArgumentOutOfRangeException();

                    Carta cartaJugada = manoActual[seleccion];
                    manoActual.RemoveAt(seleccion);
                    Console.WriteLine($"Jugador {jugador + 1} ha jugado: {cartaJugada}");
                    return cartaJugada;
                }
                catch (Exception)
                {
                    Console.WriteLine("Selección inválida. Por favor, intenta de nuevo.");
                }
            }
        }

        /// <summary>
        /// Determina el ganador de la ronda.
        /// </summary>
        /// <param name="cartasJugadas">Cartas jugadas en la ronda.</param>
        /// <param name="empate">Indica si hubo empate.</param>
        /// <returns>Índice del jugador ganador.</returns>
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
        /// <param name="manosJugadores">Manos de los jugadores.</param>
        /// <returns>Indica si el juego ha terminado.</returns>
        private static bool VerificarJuegoTerminado(List<List<Carta>> manosJugadores)
        {
            foreach (var mano in manosJugadores)
            {
                if (mano.Count > 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Muestra los puntajes finales de los jugadores.
        /// </summary>
        /// <param name="score">Puntajes de los jugadores.</param>
        private static void MostrarPuntajesFinales(int[] score)
        {
            Console.WriteLine("El juego ha terminado. Score final:");
            for (int i = 0; i < score.Length; i++)
                Console.WriteLine($"Jugador {i + 1}: {score[i]} puntos");

            Console.ReadLine();
        }
    }
}
