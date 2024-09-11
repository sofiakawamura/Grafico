using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafico
{
    public partial class frmGrafico : Form
    {
        public frmGrafico()
        {
            InitializeComponent();
        }

        private ListaSimples<Ponto> figuras = new ListaSimples<Ponto>();
        bool esperaPonto = false;
        bool esperaInicioReta = false;
        bool esperaFimReta = false;
        Color corAtual = Color.Black;
        private static Ponto p1 = new Ponto(0, 0, Color.Black);

        private void limparEsperas()
        {
            esperaPonto = false;
            esperaInicioReta = false;
            esperaFimReta = false;
        }

        private void pbAreaDesenho_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; // acessa contexto gráfico
            var atual = figuras.Primeiro;
            while (atual != null)
            {
                Ponto figuraAtual = atual.Info ;
                figuraAtual.Desenhar(figuraAtual.Cor, g);
                atual = atual.Prox;
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader arqFiguras = new StreamReader(dlgAbrir.FileName);
                    String linha = arqFiguras.ReadLine();

                    Double xInfEsq = Convert.ToDouble(linha.Substring(0, 5).Trim());
                    Double yInfEsq = Convert.ToDouble(linha.Substring(5, 5).Trim());
                    Double xSupDir = Convert.ToDouble(linha.Substring(10, 5).Trim());
                    Double ySupDir = Convert.ToDouble(linha.Substring(15, 5).Trim());
                    while ((linha = arqFiguras.ReadLine()) != null)
                    {
                        String tipo = linha.Substring(0, 5).Trim();

                        int xBase = Convert.ToInt32(linha.Substring(5, 5).Trim());
                        int yBase = Convert.ToInt32(linha.Substring(10, 5).Trim());
                        int corR = Convert.ToInt32(linha.Substring(15, 5).Trim());
                        int corG = Convert.ToInt32(linha.Substring(20, 5).Trim());
                        int corB = Convert.ToInt32(linha.Substring(25, 5).Trim());
                        Color cor = new Color();
                        cor = Color.FromArgb(255, corR, corG, corB);
                        switch (tipo[0])

                        {

                            case 'p': // figura é um ponto
                                figuras.InserirAposFim(new Ponto(xBase, yBase, cor));
                                break;
                            case 'l': // figura é uma reta
                                int xFinal = Convert.ToInt32(linha.Substring(30, 5).Trim());
                                int yFinal = Convert.ToInt32(linha.Substring(35, 5).Trim());
                                figuras.InserirAposFim(new Reta(xBase, yBase, xFinal, yFinal, cor));
                                break;
                            case 'c': // figura é um círculo
                                int raio = Convert.ToInt32(linha.Substring(30, 5).Trim());
                                figuras.InserirAposFim(new Circulo(xBase, yBase, raio, cor));
                                break;
                        }
                    }

                    arqFiguras.Close();
                    this.Text = dlgAbrir.FileName;
                    pbAreaDesenho.Invalidate();
                }
                catch (IOException)
                {
                    Console.WriteLine("Erro de leitura no arquivo");
                }
            }
        }

        private void pbAreaDesenho_MouseMove(object sender, MouseEventArgs e)
        {
            stMensagem.Items[3].Text = e.X + ", " + e.Y;
        }

        private void pbAreaDesenho_MouseClick(object sender, MouseEventArgs e)
        {
            if (esperaPonto)
            {
                Ponto novoPonto = new Ponto(e.X, e.Y, corAtual);
                figuras.InserirAposFim(novoPonto);
                novoPonto.Desenhar(novoPonto.Cor, pbAreaDesenho.CreateGraphics());
                esperaPonto = false;
                stMensagem.Items[1].Text = "";
            }
            else if (esperaInicioReta)
            {
                p1.Cor = corAtual;
                p1.X = e.X;
                p1.Y = e.Y;
                esperaInicioReta = false;
                esperaFimReta = true;
                stMensagem.Items[1].Text = "Mensagem: clique o ponto final da reta";
            }
            else if (esperaFimReta)
            {
                esperaInicioReta = false;
                esperaFimReta = false;
                Reta novaLinha = new Reta(p1.X, p1.Y, e.X, e.Y, corAtual);
                figuras.InserirAposFim(novaLinha);
                novaLinha.Desenhar(novaLinha.Cor, pbAreaDesenho.CreateGraphics());
            }
        }

        private void btnPonto_Click(object sender, EventArgs e)
        {
            stMensagem.Items[1].Text = "clique no local do ponto desejado:";
            limparEsperas();
            esperaPonto = true;
        }

        private void btnReta_Click(object sender, EventArgs e)
        {
            stMensagem.Items[1].Text = "Clique no local do ponto inicial da reta:";
            limparEsperas();
            esperaInicioReta = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (dlgSalvar.ShowDialog() == DialogResult.OK)
            {
                StreamWriter arquivo = new StreamWriter(dlgSalvar.FileName);

                var atual = figuras.Primeiro;
                while (atual != null)
                {
                    Ponto figuraAtual = atual.Info;
                    arquivo.WriteLine(figuraAtual.ToString());
                    atual = atual.Prox;
                }

                arquivo.Close();
                
            }
        }
    }
}
