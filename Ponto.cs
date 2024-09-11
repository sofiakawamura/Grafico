using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace Grafico
{
    class Ponto
    {
        private int x, y;
        private Color cor;

        public Ponto(int cX, int cY, Color qualCor)
        {
            X = cX;
            Y = cY;
            Cor = qualCor;
        }

        public int X
        {
            get { return x; }
            set 
            {
                if (value < 0)
                    throw new Exception("Coordenadas não podem ser negativas!");
                x = value; 
            }
        }

        public int Y
        {
            get { return y; }
            set 
            {
                if (value < 0)
                    throw new Exception("Coordenadas não podem ser negativas!");
                y = value;
            }
        }

        public Color Cor
        {
            get { return cor; }
            set { cor = value; }
        }

        public virtual void Desenhar(Color cor, Graphics g)
        {
            Pen pen = new Pen(cor);
            g.DrawLine(pen, x, y, x, y);
        }

        public int CompareTo(Ponto other)
        {
            int diferencaX = X - other.X;
            if (diferencaX == 0)
                return Y - other.Y;
            return diferencaX;
        }
    }
}