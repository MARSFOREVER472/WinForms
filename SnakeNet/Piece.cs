using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // Agregar una librería para los componentes de la interfaz.

namespace SnakeNet
{
    class Piece: Label // Clase Piece mediante un componente de texto.
    {
        public Piece(int x, int y) // La serpiente tiene dos dimensiones (ancho y altura).
        {
            Location = new System.Drawing.Point(x, y); // Se ubicará la posición en donde se encuentra la serpiente.
            Size = new System.Drawing.Size(20, 20); // El tamaño de la comida será la misma que la original.
            BackColor = Color.Orange; // El color de la serpiente es naranja.
            Enabled = false; // Por el momento no se activará la visualización de la serpiente.
        }
    }
}
