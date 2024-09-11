using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Grafico
{
    class Circulo : Ponto
    {
        // herda o ponto central (x, y) da classe Ponto
        int raio;

        public int Raio
        {
            get { return raio; }
            set 
            {
                if (value < 0)
                    throw new Exception("Raio não pode ser negativo");
                raio = value; 
            }
        }

        public Circulo(int xCentro, int yCentro, int novoRaio, Color novaCor) :
            base(xCentro, yCentro, novaCor)
        {
            Raio = novoRaio;
        }

        public override void Desenhar(Color corDesenho, Graphics g)
        {
            Pen pen = new Pen(corDesenho);
            g.DrawEllipse(pen, base.X - raio, base.Y - raio,    // centro - raio
                                2 * raio, 2 * raio);            // centro + raio
        }

        public override String ToString()
        {
            return transformaString("c", 5) +
                    transformaString(X, 5) +
                    transformaString(Y, 5) +
                    transformaString(Cor.R, 5) +
                    transformaString(Cor.G, 5) +
                    transformaString(Cor.B, 5) +
                    transformaString(raio, 5);
        }
    }
}