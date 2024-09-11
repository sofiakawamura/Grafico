using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Grafico
{
    class Retangulo : Ponto
    {
        public override void Desenhar(Color corDesenho, Graphics g)
        {
            Pen pen = new Pen(corDesenho);
            // g.DrawRectangle()
        }

        public override string ToString()
        {
            return transformaString("l", 5) +
                    transformaString(base.X, 5) +
                    transformaString(base.Y, 5) +
                    transformaString(Cor.R, 5) +
                    transformaString(Cor.G, 5) +
                    transformaString(Cor.B, 5) +
                    transformaString(pontoFinal.X, 5) +
                    transformaString(pontoFinal.Y, 5);
        }
    }
}
