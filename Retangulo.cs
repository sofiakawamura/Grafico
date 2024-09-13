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
        int altura, largura;

        public Retangulo(int x, int y, int largura, int altura, Color cor) : 
            base(x, y, cor)
        {
            Largura = largura;
            Altura = altura;
        }

        public int Largura
        {
            get { return largura; }
            set
            {
                if (value < 0)
                    largura = largura * (-1);
                largura = value;
            }
        }

        public int Altura
        {
            get { return altura; }
            set
            {
                if (value < 0)
                    altura = altura * (-1);
                altura = value;
            }
        }

        public override void Desenhar(Color corDesenho, Graphics g)
        {
            Pen pen = new Pen(corDesenho);
            g.DrawRectangle(pen, base.X, base.Y, largura, altura);
        }

        public override void DesenharSelecionadas(Graphics g)
        {
            Pen pen = new Pen(Color.Red, 2);
            g.DrawRectangle(pen, base.X, base.Y, largura, altura);
        }

        public override string ToString()
        {
            return transformaString("r", 5) +
                    transformaString(base.X, 5) +
                    transformaString(base.Y, 5) +
                    transformaString(Cor.R, 5) +
                    transformaString(Cor.G, 5) +
                    transformaString(Cor.B, 5) +
                    transformaString(largura, 5) +
                    transformaString(altura, 5);
        }

        public int CompareTo(Retangulo other)
        {
            if (base.CompareTo(other) != 0)
                return base.CompareTo(other);
            if (largura - other.largura == 0)
                return altura - other.altura;
            return largura - other.largura;
        }
    }
}
