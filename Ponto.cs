// Ana Clara Martin da Silveira - 23122
// Sofia Tasselli Kawamura - 23157

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace Grafico
{
    class Ponto : IComparable<Ponto>
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
            g.DrawEllipse(pen, x, y, 2, 2);
        }

        public virtual void DesenharSelecionadas(Graphics g)
        {
            Pen pen = new Pen(Color.Red, 2);
            g.DrawEllipse(pen, x, y, 2, 2);
        }

        public int CompareTo(Ponto other)
        {
            int diferencaX = X - other.X;
            if (diferencaX == 0)
                return Y - other.Y;
            return diferencaX;
        }

        public String transformaString(int valor, int quantasPosicoes)
        {
            String cadeia = valor + "";
            while (cadeia.Length < quantasPosicoes)
                cadeia = "0" + cadeia;
            return cadeia.Substring(0, quantasPosicoes); // corta, se necessário, para
                                                         // tamanho máximo
        }

        public String transformaString(String valor, int quantasPosicoes)
        {
            String cadeia = valor + "";
            while (cadeia.Length < quantasPosicoes)
                cadeia = cadeia + " ";
            return cadeia.Substring(0, quantasPosicoes); // corta, se necessário, para
                                                         // tamanho máximo
        }

        public override String ToString()
        {
            return  transformaString("p", 5) +
                    transformaString(X, 5) +
                    transformaString(Y, 5) +
                    transformaString(Cor.R, 5) +
                    transformaString(Cor.G, 5) +
                    transformaString(Cor.B, 5);
        }
    }
}