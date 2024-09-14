using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class ListaSimples<Dado> where Dado : IComparable<Dado>
{
    NoLista<Dado> primeiro, ultimo, anterior, atual;
    int quantosNos;
    bool primeiroAcessoDoPercurso;

    public ListaSimples()
    {
        primeiro = ultimo = anterior = atual = null;
        quantosNos = 0;
        primeiroAcessoDoPercurso = false;
    }

    public void PercorrerLista()
    {
        atual = primeiro;
        while (atual != null)
        {
            Console.WriteLine(atual.Info);
            atual = atual.Prox;
        }
    }
    public bool EstaVazia
    {
        get => primeiro == null;
    }

    public NoLista<Dado> Primeiro
    {
        get => primeiro;
    }

    public NoLista<Dado> Ultimo
    {
        get => ultimo;
    }

    public int QuantosNos
    {
        get => quantosNos;
    }

    public void InserirAntesDoInicio(Dado novoDado)
    {
        var novoNo = new NoLista<Dado>(novoDado);

        if (EstaVazia)
            ultimo = novoNo;

        novoNo.Prox = primeiro;
        primeiro    = novoNo;
        quantosNos++;
    }

    public void InserirAposFim(Dado novoDado)
    {
        var novoNo = new NoLista<Dado>(novoDado);

        if (EstaVazia)
            primeiro = novoNo;
        else
            ultimo.Prox = novoNo;

        ultimo = novoNo;
        quantosNos++;
    }

    public void Listar(ListBox lsb)
    {
        lsb.Items.Clear();
        atual = primeiro;
        while (atual != null)
        {
            lsb.Items.Add(atual.Info);
            atual = atual.Prox;
        }
    }

    public bool Existe(Dado outroProcurado)
    {
        anterior = null;
        atual = primeiro;

        if (EstaVazia)
            return false;

        if (outroProcurado.CompareTo(primeiro.Info) < 0)
            return false;

        if (outroProcurado.CompareTo(ultimo.Info) > 0)
        {
            anterior = ultimo;
            atual = null;
            return false;
        }

        bool achou = false;
        bool fim = false;

        while (!achou && !fim)
            if (atual == null)
                fim = true;
            else if (outroProcurado.CompareTo(atual.Info) == 0)
                achou = true;
            else if (atual.Info.CompareTo(outroProcurado) > 0)
                fim = true;
            else
            {
                anterior = atual;
                atual = atual.Prox;
            }

        return achou;   
    }

    public void Excluir(Dado dado)
    {
        if (Existe(dado))
        {
            atual = primeiro;
            anterior = null;
            while (atual.Info.CompareTo(dado) != 0)
            {
                anterior = atual;
                atual = atual.Prox;
            }

            if (dado.CompareTo(primeiro.Info) == 0)
                primeiro = atual.Prox;
            else
                anterior.Prox = atual.Prox;

            quantosNos--;
        }
    }
}
