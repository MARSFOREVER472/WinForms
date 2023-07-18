using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeNet
{
    public partial class Snake : Form
    {
        // A la serpiente le pasamos varios parámetros de acción.

        int cols = 50, rows = 25, score = 0, dx = 0, dy = 0, front = 0, back = 0;
        Piece[] snake = new Piece[1250]; // La serpiente tendrá el tamaño de sus partes por defecto.
        List<int> available = new List<int>(); // Se encontrará disponible mediante lista las partes de la serpiente.
        bool[,] visit; // Estará o no la serpiente.

        Random rand = new Random(); // Variable Aleatoria.
        Timer temporizador = new Timer(); // Contador de tiempo.

        // EN INSTANTES...
        public Snake()
        {
            InitializeComponent();
            // EN INSTANTES...
        }

        private void Snake_Load(object sender, EventArgs e)
        {

        }
    }
}
