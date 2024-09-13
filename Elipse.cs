using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Grafico
{
    class Elipse : Ponto
    {
        // herda o ponto central (x, y) da classe Ponto
        int raio1;
        int raio2;

        public int Raio1
        {
            get { return raio1; }
            set
            {
                if (value < 0)
                    throw new Exception("Raio não pode ser negativo");
                raio1 = value;
            }
        }

        public int Raio2
        {
            get { return raio2; }
            set
            {
                if (value < 0)
                    throw new Exception("Raio não pode ser negativo");
                raio2 = value;
            }
        }

        public Elipse(int xCentro, int yCentro, int novoRaio1, int novoRaio2, Color novaCor) :
            base(xCentro, yCentro, novaCor)
        {
            Raio1 = novoRaio1;
            Raio2 = novoRaio2;
        }

        public override void Desenhar(Color corDesenho, Graphics g)
        {
            Pen pen = new Pen(corDesenho);
            g.DrawEllipse(pen, base.X - raio1, base.Y - raio2,    // centro - raio
                                2 * raio1, 2 * raio2);
        }

        public override String ToString()
        {
            return transformaString("e", 5) +
                    transformaString(X, 5) +
                    transformaString(Y, 5) +
                    transformaString(Cor.R, 5) +
                    transformaString(Cor.G, 5) +
                    transformaString(Cor.B, 5) +
                    transformaString(raio1, 5) +
                    transformaString(raio2, 5);
        }

        public int CompareTo(Elipse other)
        {
            if (base.CompareTo(other) != 0)
                return base.CompareTo(other);
            if (raio1 == other.Raio1)
                return raio2 - other.Raio2;
            return raio1 - other.Raio1; 
        }
    }
}