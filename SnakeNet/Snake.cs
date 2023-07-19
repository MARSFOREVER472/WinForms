using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeNet // Espacio de nombres para el proyecto.
{
    public partial class Snake : Form // Clase de la interfaz por defecto.
    {
        // A la serpiente le pasamos varios parámetros de acción.

        int cols = 50, rows = 25, score = 0, dx = 0, dy = 0, front = 0, back = 0;
        Piece[] snake = new Piece[1250]; // La serpiente tendrá el tamaño de sus partes por defecto.
        List<int> available = new List<int>(); // Se encontrará disponible mediante lista las partes de la serpiente.
        bool[,] visit; // Estará o no la serpiente.

        Random rand = new Random(); // Variable Aleatoria.
        Timer temporizador = new Timer(); // Contador de tiempo.

        // Método para la serpiente.
        public Snake()
        {
            InitializeComponent(); // Llamado del método principal.
            initial(); // Llamado del método de inicialización.
        }

        // Método inicial.
        private void initial()
        {
            visit = new bool[rows, cols]; // La serpiente verificará si estará mediante recorrido de filas y columnas.
            Piece head =
                new Piece((rand.Next() % cols) * 20, (rand.Next() % rows) * 20); // Cabeza de la serpiente mediante filas y columnas.
            lblComida.Location = new Point((rand.Next() % cols) * 20, (rand.Next() % rows) * 20); // Comida generada mediante cadena de texto se posicionará en cualquier punto.

            // Crearemos 2 ciclos "for" mediante filas y columnas la presencia de la serpiente con su comida.

            for (int i = 0; i < rows; i++) // Para las filas.
            {
                for (int j = 0; j < cols; j++) // Para las columnas.
                {
                    visit[i, j] = false; // Por el momento no se solicitará su presencia pero en la siguiente línea de código...
                    available.Add(i * cols + j); // Va añadiendo mediante este algoritmo.
                }
                visit[head.Location.Y / 20, head.Location.X / 20] = true; // Mediante algoritmo por filas la serpiente estará presente en la consola.
                available.Remove(head.Location.Y / 20 * cols + head.Location.X / 20); // Se eliminará la serpiente desde su cabeza.
                Controls.Add(head); snake[front] = head; // Añade controles para la cabeza de la serpiente desde lo frontal.
            }
        }
    }
}
