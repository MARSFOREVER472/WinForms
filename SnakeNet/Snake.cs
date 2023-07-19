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
            lanzarTemporizador(); // Llamado del siguiente método para el temporizador.
        }

        // Método para las funciones del tiempo mediante temporizador.
        private void lanzarTemporizador()
        {
            temporizador.Interval = 50; // Intervalo de tiempo de entre 0 a 50 segundos.
            temporizador.Tick += move; // Se mueve cada unidad de tiempo el temporizador.
            temporizador.Start(); // Inicia el temporizador.
        }

        // Método extra al presionar la tecla de abajo.
        private void Snake_KeyDown(object sender, KeyEventArgs e)
        {
            // Nueva variable para las distancias (x e y).

            dx = dy = 0;

            // Usaremos la estructura de un "Switch" mediante algunos "cases" al presionar las teclas de flecha:

            switch(e.KeyCode)
            {
                case Keys.Right: // Cuando se presiona la tecla derecha.
                    dx = 20;
                    break;

                case Keys.Left: // Cuando se presiona la tecla izquierda.
                    dx = -20;
                    break;

                case Keys.Up: // Cuando se presiona la tecla de arriba.
                    dy = -20;
                    break;

                case Keys.Down: // Cuando se presiona la tecla de abajo.
                    dy = 20;
                    break;
            }
        }

        // Método para mover el contador de tiempo mediante un temporizador.
        private void move(object sender, EventArgs e)
        {
            // Declaramos su posición para la serpiente en x e y.

            int x = snake[front].Location.X, y = snake[front].Location.Y;
            if (dx == 0 && dy == 0) return; // Retorna el valor de la posición si ambas distancias son 0.
            if (gameOver(x + dx, y + dy)) // Mediante distancias se realizarán acciones al finalizar la partida.
            {
                temporizador.Stop(); // Paraliza el contador de tiempo.
                MessageBox.Show(" JUEGO TERMINADO !!! "); // Juego Terminado.
                return;
            }

            // Al momento de colisionar con la comida, utilizaremos la condición "if".

            if (colisionarComida(x + dx, y + dy))
            {
                // Incrementa la puntuación.
                score += 1;
                lblScore.Text = " Puntuación: " + score.ToString(); // Puntuación: 1.
                if (choque((y + dy) / 20, (x + dx) / 20)) return; // Cuando choca con su cuerpo.
                Piece head = new Piece(x + dx, y + dy); // Definimos su cabeza para la serpiente.
                front = (front - 1 + 1250) % 1250; // Vista frontal de la serpiente al realizar esta acción.
                snake[front] = head; // La parte frontal de la serpiente será su cabeza.
                visit[head.Location.Y / 20, head.Location.X / 20] = true; // Aparecerá su cabeza.
                Controls.Add(head); // Añade controles para su cabeza.
                comidaAleatoria(); // Vamos a añadir este nuevo método en relación a la comida.
            } // En caso contrario...
            else
            {
                if (choque((y + dy) / 20, (x + dx) / 20)) return; // Cuando choca con su cuerpo.
                visit[snake[back].Location.Y / 20, snake[back].Location.X / 20] =
                    false; // No se presenciará su vista trasera de su cuerpo.
                front = (front - 1 + 1250) % 1250; // Vista frontal de la serpiente al realizar esta acción.
                snake[front] = snake[back]; // Es el cuerpo completo de la serpiente.
                snake[front].Location = new Point(x + dx, y + dy); // Posición de su vista frontal para la serpiente.
                back = (back - 1 + 1250) % 1250; // Vista trasera de la serpiente al realizar esta acción.
                visit[(y + dy) / 20, (x + dx) / 20] = true;

            }
        }

        // Método que genera una comida en cualquier posición del juego.
        private void comidaAleatoria()
        {
            // Realizaremos el siguiente algoritmo cuando se posiciona su comida en cualquier parte del juego.
            available.Clear();
            for (int i = 0; i < rows; i++) // Filas.
                for (int j = 0; j < cols; j++) // Columnas.
                    if (!visit[i, j]) available.Add(i * cols + j); // Algoritmo más complejo mediante filas y columnas.
            int idx = rand.Next(available.Count) % available.Count; // Variable más avanzada para un index.
            lblComida.Left = (available[idx] * 20) % Width; // Ancho o en x.
            lblComida.Top = (available[idx] * 20) / Width * 20; // Altura o en y.
        }

        // Método booleano cuando choca con un objeto como la comida.
        private bool choque(int x, int y)
        {
            // Se realizará un algoritmo al chocar con su cuerpo y finaliza la partida.

            if (visit[x, y])
            {
                temporizador.Stop(); // Para el contador de tiempo.
                MessageBox.Show(" LA SERPIENTE CHOCÓ CON SU CUERPO! "); // La serpiente chocó con su cuerpo.
                return true; // Si es que se cumple con esta condición.
            }
            return false; // Si es que no se cumple con la condición anterior.
        }

        // Método que permite a la serpiente al colisionar con la comida.

        private bool colisionarComida(int x, int y)
        {
            // Mediante las posiciones en x e y devuelven un valor por defecto al colisionar con la comida.
            return x == lblComida.Location.X && y == lblComida.Location.Y;
        }

        // Método que finaliza la partida mediante posiciones (x e y).

        private bool gameOver(int x, int y)
        {
            return x < 0 || y < 0 || x > 980 || y > 480; // Retorna un valor definitivo de las posiciones al finalizar la partida.
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
