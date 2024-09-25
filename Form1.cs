// Ana Clara Martin da Silveira - 23122
// Sofia Tasselli Kawamura - 23157

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
        private ListaSimples<Ponto> figurasSelecionadas = new ListaSimples<Ponto>();

        bool esperaPonto = false;
        bool esperaInicioReta = false;
        bool esperaFimReta = false;
        bool esperaCentroCirculo = false;
        bool esperaRaioCirculo = false;
        bool esperaInicioElipse = false;
        bool esperaDiagonalElipse = false;
        bool esperaInicioRetangulo = false;
        bool esperaFimRetangulo = false;
        bool esperaInicioPolilinha = false;
        bool esperaPolilinha = false;

        Color corAtual = Color.Black;
        private static Ponto p1 = new Ponto(0, 0, Color.Black);
        int raio1;

        private void LimparEsperas()
        {
            stMensagem.Items[1].Text = "sem mensagem";
            esperaPonto = false;
            esperaInicioReta = false;
            esperaFimReta = false;
            esperaCentroCirculo = false;
            esperaRaioCirculo = false;
            esperaInicioElipse = false;
            esperaDiagonalElipse = false; 
            esperaInicioRetangulo = false;
            esperaFimRetangulo = false;
            esperaInicioPolilinha = false;
            esperaPolilinha = false;
        }

        private void LimparFiguras()
        {
            figuras = new ListaSimples<Ponto>();
            LimparEsperas();
            pbAreaDesenho.Invalidate();
        }

        private void LimparFigurasSelecionadas()
        {
            figurasSelecionadas = new ListaSimples<Ponto>();
            LimparEsperas();
            pbAreaDesenho.Invalidate();
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

            var atualSelecionadas = figurasSelecionadas.Primeiro;

            while (atualSelecionadas != null)
            {
                Ponto figuraSelecionadaAtual = atualSelecionadas.Info ;
                figuraSelecionadaAtual.DesenharSelecionadas(g);
                atualSelecionadas = atualSelecionadas.Prox;
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                LimparFiguras();
                try
                {
                    StreamReader arqFiguras = new StreamReader(dlgAbrir.FileName);
                    String linha = arqFiguras.ReadLine();

                    Double xInfEsq = Convert.ToDouble(linha.Substring(5, 5).Trim());
                    Double yInfEsq = Convert.ToDouble(linha.Substring(10, 5).Trim());
                    Double xSupDir = Convert.ToDouble(linha.Substring(15, 5).Trim());
                    Double ySupDir = Convert.ToDouble(linha.Substring(20, 5).Trim());

                    while ((linha = arqFiguras.ReadLine()) != null)
                    {
                        String tipo = linha.Substring(0, 5).Trim();

                        int xBase = Convert.ToInt32(linha.Substring(5, 5).Trim());
                        int yBase = Convert.ToInt32(linha.Substring(10, 5).Trim());
                        int corR = Convert.ToInt32(linha.Substring(15, 5).Trim());
                        int corG = Convert.ToInt32(linha.Substring(20, 5).Trim());
                        int corB = Convert.ToInt32(linha.Substring(25, 5).Trim());

                        this.Left = (int)xInfEsq;
                        this.Top = (int)yInfEsq;
                        this.Width = (int)Math.Round(xSupDir - xInfEsq);
                        this.Height = (int)Math.Round(ySupDir - yInfEsq);

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
                            case 'e': // figura é uma elipse 
                                int raio1 = Convert.ToInt32(linha.Substring(30, 5).Trim());
                                int raio2 = Convert.ToInt32(linha.Substring(35, 5).Trim());
                                figuras.InserirAposFim(new Elipse(xBase, yBase, raio1, raio2, cor));
                                break;
                            case 'r': // figura é um retângulo
                                int largura = Convert.ToInt32(linha.Substring(30, 5).Trim());
                                int altura = Convert.ToInt32(linha.Substring(35, 5).Trim());
                                figuras.InserirAposFim(new Retangulo(xBase, yBase, largura, altura, cor));
                                break;
                            case 'm': // figura é uma linha múltipla (polilinha)
                                Polilinha polilinha = new Polilinha(xBase, yBase, cor);
                                int qtosPontos = Convert.ToInt32(linha.Substring(30, 5).Trim());
                                for (int i = 0; i < qtosPontos; i++)
                                {
                                    int xPonto = Convert.ToInt32(linha.Substring((35+(10*i)), 5).Trim());
                                    int yPonto = Convert.ToInt32(linha.Substring((40+(10*i)), 5).Trim());
                                    polilinha.Pontos.InserirAposFim(new Ponto(xPonto, yPonto, cor));
                                }
                                figuras.InserirAposFim(polilinha);
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (dlgSalvar.ShowDialog() == DialogResult.OK)
            {
                StreamWriter arquivo = new StreamWriter(dlgSalvar.FileName);

                double xInfEsq = this.Left;
                double yInfEsq = this.Top;
                double xSupDir = this.Left + this.Width;
                double ySupDir = this.Top + this.Height;

                string linha =  xInfEsq.ToString().PadLeft(10, '0') + 
                                yInfEsq.ToString().PadLeft(5, '0') +
                                xSupDir.ToString().PadLeft(5, '0') + 
                                ySupDir.ToString().PadLeft(5, '0');
                arquivo.WriteLine(linha);

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
                stMensagem.Items[1].Text = "sem mensagem";
            }
            else if (esperaInicioReta)
            {
                p1.Cor = corAtual;
                p1.X = e.X;
                p1.Y = e.Y;
                esperaInicioReta = false;
                esperaFimReta = true;
                stMensagem.Items[1].Text = "clique no ponto final da reta";
            }
            else if (esperaFimReta)
            {
                esperaInicioReta = false;
                esperaFimReta = false;
                Reta novaLinha = new Reta(p1.X, p1.Y, e.X, e.Y, corAtual);
                figuras.InserirAposFim(novaLinha);
                novaLinha.Desenhar(novaLinha.Cor, pbAreaDesenho.CreateGraphics());
                stMensagem.Items[1].Text = "sem mensagem";
            }
            else if (esperaCentroCirculo)
            {
                p1.Cor = corAtual;
                p1.X = e.X;
                p1.Y = e.Y;
                esperaCentroCirculo = false;
                esperaRaioCirculo = true;
                stMensagem.Items[1].Text = "clique no raio do círculo";
            }
            else if (esperaRaioCirculo)
            {
                esperaCentroCirculo = false;
                esperaRaioCirculo = false;
                int raio = (int)Math.Round(Math.Sqrt(Math.Pow(e.X - p1.X, 2) + Math.Pow(e.Y - p1.Y, 2)));
                Circulo novoCirculo = new Circulo(p1.X, p1.Y, raio, corAtual);
                figuras.InserirAposFim(novoCirculo);
                novoCirculo.Desenhar(novoCirculo.Cor, pbAreaDesenho.CreateGraphics());
                stMensagem.Items[1].Text = "sem mensagem";
            }
            else if (esperaInicioElipse)
            {
                p1.Cor = corAtual;
                p1.X = e.X;
                p1.Y = e.Y;
                esperaInicioElipse = false;
                esperaDiagonalElipse = true;
                stMensagem.Items[1].Text = "clique na diagonal da elipse";
            }
            else if (esperaDiagonalElipse)
            {
                esperaInicioElipse = false;
                esperaDiagonalElipse = false;

                int raio1 = (e.X - p1.X) / 2; // metade do diâmetro
                int raio2 = (e.Y - p1.Y) / 2;
                if (e.X < p1.X)
                    raio1 *= (-1);
                if (e.Y < p1.Y)
                    raio2 *= (-1);

                int centroX = (e.X + p1.X) / 2; // ponto médio entre as diagonais 
                int centroY = (e.Y + p1.Y) / 2;

                Elipse novaElipse = new Elipse(centroX, centroY, raio1, raio2, corAtual);
                figuras.InserirAposFim(novaElipse);
                novaElipse.Desenhar(novaElipse.Cor, pbAreaDesenho.CreateGraphics());
                stMensagem.Items[1].Text = "sem mensagem";
            }
            else if (esperaInicioRetangulo)
            {
                p1.Cor = corAtual;
                p1.X = e.X;
                p1.Y = e.Y;
                esperaInicioRetangulo = false;
                esperaFimRetangulo = true;
                stMensagem.Items[1].Text = "clique na diagonal do retângulo";
            }
            else if (esperaFimRetangulo)
            {
                esperaInicioRetangulo = false;
                esperaFimRetangulo = false;

                int altura = e.Y - p1.Y;
                int largura = e.X - p1.X;

                if (e.Y < p1.Y)
                {
                    altura *= (-1);
                    p1.Y = e.Y;
                }
                if (e.X < p1.X)
                {
                    largura *= (-1);
                    p1.X = e.X;
                }

                Retangulo novoRetangulo = new Retangulo(p1.X, p1.Y, largura, altura, corAtual);
                figuras.InserirAposFim(novoRetangulo);
                novoRetangulo.Desenhar(novoRetangulo.Cor, pbAreaDesenho.CreateGraphics());
                stMensagem.Items[1].Text = "sem mensagem";
            }
            else if (esperaInicioPolilinha)
            {
                Polilinha novaPolilinha = new Polilinha(e.X, e.Y, corAtual);
                figuras.InserirAposFim(novaPolilinha);
                esperaInicioPolilinha = false;
                esperaPolilinha = true;
                stMensagem.Items[1].Text = "clique no próximo ponto da linha";
            }
            else if (esperaPolilinha)
            {
                Polilinha polilinha = (Polilinha)figuras.Ultimo.Info;
                polilinha.Pontos.InserirAposFim(new Ponto(e.X, e.Y, corAtual));
                polilinha.Desenhar(polilinha.Cor, pbAreaDesenho.CreateGraphics());
            }
        }

        private void btnPonto_Click(object sender, EventArgs e)
        {
            LimparEsperas();
            stMensagem.Items[1].Text = "clique no local do ponto desejado";
            esperaPonto = true;
        }

        private void btnReta_Click(object sender, EventArgs e)
        {
            LimparEsperas();
            stMensagem.Items[1].Text = "clique no ponto inicial da reta";
            esperaInicioReta = true;
        }

        private void btnCirculo_Click(object sender, EventArgs e)
        {
            LimparEsperas();
            stMensagem.Items[1].Text = "clique no ponto central do círculo";
            esperaCentroCirculo = true;
        }

        private void btnElipse_Click(object sender, EventArgs e)
        {
            LimparEsperas();
            stMensagem.Items[1].Text = "clique no ponto inicial da elipse";
            esperaInicioElipse = true;
        }

        private void btnRetangulo_Click(object sender, EventArgs e)
        {
            LimparEsperas();
            stMensagem.Items[1].Text = "clique no ponto inicial do retângulo";
            esperaInicioRetangulo = true;
        }

        private void btnPolilinha_Click(object sender, EventArgs e)
        {
            LimparEsperas();
            stMensagem.Items[1].Text = "clique no ponto inicial da polilinha";
            esperaInicioPolilinha = true;
        }

        private void btnCor_Click(object sender, EventArgs e)
        {
            if (dlgCor.ShowDialog() == DialogResult.OK)
            {
                corAtual = dlgCor.Color;

                var figuraSelecionada = figurasSelecionadas.Primeiro;
                while (figuraSelecionada != null)
                {
                    var atual = figuras.Primeiro;
                    while (atual != null)
                    {
                        if (atual.Info.CompareTo(figuraSelecionada.Info) == 0)
                        {
                            atual.Info.Cor = corAtual;
                            break;
                        }
                        atual = atual.Prox;
                    }
                    figuraSelecionada = figuraSelecionada.Prox;
                }
                LimparFigurasSelecionadas();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparFiguras();
            LimparFigurasSelecionadas();
        }

        private void txtPosicaoSelecionar_KeyPress(object sender, KeyPressEventArgs e)
        {
            // verifica se o caracter digitado é um número
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            LimparEsperas();
            bool conversao = int.TryParse(txtPosicaoSelecionar.Text, out int indice);

            // para verificar se a posição (da lista) digitada pelo usuário existe
            bool indiceExiste = indice < figuras.QuantosNos && indice >= 0;

            if (!indiceExiste || !conversao)
            {
                MessageBox.Show("Digite uma posição válida!");
                txtPosicaoSelecionar.Text = string.Empty;
            }

            else
            {
                int contador = 0;
                var atual = figuras.Primeiro;
                while (contador != indice)
                {
                    atual = atual.Prox;
                    contador++;
                }
                if (!figurasSelecionadas.Existe(atual.Info))
                    figurasSelecionadas.InserirAposFim(atual.Info);
            }
            pbAreaDesenho.Invalidate();
        }

        private void btnDesselecionar_Click(object sender, EventArgs e)
        {
            LimparEsperas();
            bool conversao = int.TryParse(txtPosicaoSelecionar.Text, out int indice);

            // para verificar se a posição (da lista) digitada pelo usuário existe
            bool indiceExiste = indice < figuras.QuantosNos && indice >= 0;

            if (!indiceExiste || !conversao)
            {
                MessageBox.Show("Digite uma posição válida!");
                txtPosicaoSelecionar.Text = string.Empty;
            }

            else
            {
                int contador = 0;
                var figuraDesejada = figuras.Primeiro;
                while (contador != indice)
                {
                    figuraDesejada = figuraDesejada.Prox;
                    contador++;
                }

                figurasSelecionadas.Excluir(figuraDesejada.Info);
            }
            pbAreaDesenho.Invalidate();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            LimparEsperas();
            var atual = figurasSelecionadas.Primeiro;
            while (atual != null)
            {
                figuras.Excluir(atual.Info);
                figurasSelecionadas.Excluir(atual.Info);
                atual = atual.Prox;
            }
            pbAreaDesenho.Invalidate();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
