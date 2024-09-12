using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Grafico
{
    class Polilinha : Ponto
    {
        ListaSimples<Ponto> pontos;

        public Polilinha(int x1, int y1, Color novaCor) : base(x1, y1, novaCor)
        {
            pontos = new ListaSimples<Ponto>();
        }

        public ListaSimples<Ponto> Pontos
        {
            get { return pontos; }
            set
            {
                pontos = value;
            }
        }

        public override void Desenhar(Color corDesenho, Graphics g)
        {
            Pen pen = new Pen(corDesenho);

            Ponto inicial = new Ponto(base.X, base.Y, Cor);
            var atual = pontos.Primeiro;
            while (atual != null)
            {
                Ponto final = atual.Info;
                g.DrawLine(pen, inicial.X, inicial.Y,
                            final.X, final.Y);
                inicial = final;
                atual = atual.Prox;
            }
        }

        public override string ToString()
        {
            String cadeia = transformaString("m", 5) + // linha múltipla
                    transformaString(base.X, 5) +
                    transformaString(base.Y, 5) +
                    transformaString(Cor.R, 5) +
                    transformaString(Cor.G, 5) +
                    transformaString(Cor.B, 5) +
                    transformaString(pontos.QuantosNos, 5);
            var atual = pontos.Primeiro;
            while (atual != null)
            {
                Ponto pontoAtual = atual.Info;
                cadeia += transformaString(pontoAtual.X, 5);
                cadeia += transformaString(pontoAtual.Y, 5);
                atual = atual.Prox;
            }
            return cadeia;
        }
    }
}
