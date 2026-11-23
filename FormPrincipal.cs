using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

class Quarto
{
    public int numero;
    public string tipo;
    public int capacidade;
    public double preco;
    public bool disponivel = true;
}

class Reserva
{
    public int codigo;
    public string hospede;
    public int quarto;
    public DateTime checkin;
    public int noites;
}

class FormPrincipal : Form
{
    // listas e fila (estruturas lineares)
    List<Quarto> quartos = new List<Quarto>();
    List<Reserva> reservas = new List<Reserva>();
    Queue<Reserva> fila = new Queue<Reserva>();
    int proximoCodigo = 1;

    // controles da tela
    ListBox lista;
    TextBox txtNome, txtQuarto, txtValor, txtData, txtNoites;

    public FormPrincipal()
    {
        Text = "Reserva de Hotel";
        ClientSize = new Size(900, 620);
        StartPosition = FormStartPosition.CenterScreen;

        CriarDados();
        CriarTela();
        MostrarQuartos(quartos);
    }

    // ================= DADOS INICIAIS =================
    void CriarDados()
    {
        CriarQuarto(101, "Solteiro", 1, 150);
        CriarQuarto(102, "Casal", 2, 220);
        CriarQuarto(103, "Casal", 2, 250);
        CriarQuarto(201, "Familia", 4, 380);
        CriarQuarto(202, "Familia", 4, 400);
        CriarQuarto(301, "Suite", 2, 550);

        CriarReserva("Ana", 102, new DateTime(2026, 12, 20), 3);
        CriarReserva("Bruno", 201, new DateTime(2026, 12, 5), 5);
        CriarReserva("Carla", 301, new DateTime(2026, 12, 12), 2);
    }

    void CriarQuarto(int numero, string tipo, int capacidade, double preco)
    {
        Quarto q = new Quarto();
        q.numero = numero;
        q.tipo = tipo;
        q.capacidade = capacidade;
        q.preco = preco;
        quartos.Add(q);
    }

    void CriarReserva(string hospede, int numeroQuarto, DateTime data, int noites)
    {
        Reserva r = new Reserva();
        r.codigo = proximoCodigo;
        proximoCodigo++;
        r.hospede = hospede;
        r.quarto = numeroQuarto;
        r.checkin = data;
        r.noites = noites;

        reservas.Add(r);
        fila.Enqueue(r);

        foreach (Quarto q in quartos)
        {
            if (q.numero == numeroQuarto)
            {
                q.disponivel = false;
            }
        }
    }

    // ================= MONTAGEM DA TELA =================
    void CriarTela()
    {
        CriarLabel("Nome do hospede:", 15);
        txtNome = CriarTextBox(35);

        CriarLabel("Numero do quarto:", 70);
        txtQuarto = CriarTextBox(90);

        CriarLabel("Valor maximo (R$):", 125);
        txtValor = CriarTextBox(145);

        CriarLabel("Data de check-in (dd/mm/aaaa):", 180);
        txtData = CriarTextBox(200);

        CriarLabel("Quantas noites:", 235);
        txtNoites = CriarTextBox(255);

        CriarBotao("Mostrar quartos", 300, BotaoMostrarQuartos);
        CriarBotao("Buscar reserva por nome", 332, BotaoBuscaLinear);
        CriarBotao("Buscar quarto por numero", 364, BotaoBuscaBinaria);
        CriarBotao("Ordenar quartos por preco", 396, BotaoInsertionSort);
        CriarBotao("Ordenar reservas por data", 428, BotaoBubbleSort);
        CriarBotao("Quartos disponiveis ate o valor", 460, BotaoSelecaoFiltro);
        CriarBotao("Ordenar quartos por capacidade", 492, BotaoSelectionSort);
        CriarBotao("Fazer reserva", 524, BotaoFazerReserva);
        CriarBotao("Fazer check-in (fila)", 556, BotaoCheckin);

        lista = new ListBox();
        lista.Left = 250;
        lista.Top = 15;
        lista.Width = 635;
        lista.Height = 590;
        lista.Font = new Font("Consolas", 10);
        Controls.Add(lista);
    }

    void CriarLabel(string texto, int y)
    {
        Label l = new Label();
        l.Text = texto;
        l.Left = 15;
        l.Top = y;
        l.Width = 220;
        Controls.Add(l);
    }

    TextBox CriarTextBox(int y)
    {
        TextBox t = new TextBox();
        t.Left = 15;
        t.Top = y;
        t.Width = 220;
        Controls.Add(t);
        return t;
    }

    void CriarBotao(string texto, int y, EventHandler acao)
    {
        Button b = new Button();
        b.Text = texto;
        b.Left = 15;
        b.Top = y;
        b.Width = 220;
        b.Height = 28;
        b.Click += acao;
        Controls.Add(b);
    }

    // ================= MOSTRAR NA LISTA =================
    void Mostrar(string texto)
    {
        lista.Items.Add(texto);
    }

    string TextoQuarto(Quarto q)
    {
        string status = q.disponivel ? "Disponivel" : "Ocupado";
        return "Quarto " + q.numero + " | " + q.tipo + " | " + q.capacidade + " pessoas | R$ " + q.preco + " | " + status;
    }

    string TextoReserva(Reserva r)
    {
        return "Reserva " + r.codigo + " | " + r.hospede + " | Quarto " + r.quarto + " | " + r.checkin.ToString("dd/MM/yyyy") + " | " + r.noites + " noites";
    }

    void MostrarQuartos(List<Quarto> l)
    {
        lista.Items.Clear();
        foreach (Quarto q in l)
        {
            Mostrar(TextoQuarto(q));
        }
    }

    // ================= BOTOES =================
    void BotaoMostrarQuartos(object sender, EventArgs e)
    {
        MostrarQuartos(quartos);
    }

    // BUSCA 1: busca linear - percorre as reservas procurando o nome
    void BotaoBuscaLinear(object sender, EventArgs e)
    {
        lista.Items.Clear();
        string nome = txtNome.Text.ToLower();
        bool achou = false;

        for (int i = 0; i < reservas.Count; i++)
        {
            if (reservas[i].hospede.ToLower().Contains(nome))
            {
                Mostrar(TextoReserva(reservas[i]));
                achou = true;
            }
        }
        if (!achou)
        {
            Mostrar("Nenhuma reserva encontrada.");
        }
    }

    // BUSCA 2: busca binaria - a lista de quartos esta em ordem de numero
    void BotaoBuscaBinaria(object sender, EventArgs e)
    {
        lista.Items.Clear();
        int numero;
        if (!int.TryParse(txtQuarto.Text, out numero))
        {
            MessageBox.Show("Digite apenas numeros no campo do quarto.");
            return;
        }

        int inicio = 0;
        int fim = quartos.Count - 1;

        while (inicio <= fim)
        {
            int meio = (inicio + fim) / 2;

            if (quartos[meio].numero == numero)
            {
                Mostrar(TextoQuarto(quartos[meio]));
                return;
            }
            else if (quartos[meio].numero < numero)
            {
                inicio = meio + 1;
            }
            else
            {
                fim = meio - 1;
            }
        }
        Mostrar("Quarto nao encontrado.");
    }

    // ORDENACAO 1: insertion sort por preco
    void BotaoInsertionSort(object sender, EventArgs e)
    {
        List<Quarto> copia = new List<Quarto>(quartos);

        for (int i = 1; i < copia.Count; i++)
        {
            Quarto atual = copia[i];
            int j = i - 1;

            while (j >= 0 && copia[j].preco > atual.preco)
            {
                copia[j + 1] = copia[j];
                j--;
            }
            copia[j + 1] = atual;
        }
        MostrarQuartos(copia);
    }

    // ORDENACAO 2: bubble sort por data de check-in
    void BotaoBubbleSort(object sender, EventArgs e)
    {
        List<Reserva> copia = new List<Reserva>(reservas);

        for (int i = 0; i < copia.Count - 1; i++)
        {
            for (int j = 0; j < copia.Count - 1 - i; j++)
            {
                if (copia[j].checkin > copia[j + 1].checkin)
                {
                    Reserva temp = copia[j];
                    copia[j] = copia[j + 1];
                    copia[j + 1] = temp;
                }
            }
        }

        lista.Items.Clear();
        foreach (Reserva r in copia)
        {
            Mostrar(TextoReserva(r));
        }
    }

    // SELECAO 1: quartos disponiveis com preco ate o valor digitado
    void BotaoSelecaoFiltro(object sender, EventArgs e)
    {
        double max;
        if (!double.TryParse(txtValor.Text, out max))
        {
            MessageBox.Show("Digite um valor valido.");
            return;
        }

        lista.Items.Clear();
        bool achou = false;
        foreach (Quarto q in quartos)
        {
            if (q.disponivel && q.preco <= max)
            {
                Mostrar(TextoQuarto(q));
                achou = true;
            }
        }
        if (!achou)
        {
            Mostrar("Nenhum quarto encontrado.");
        }
    }

    // SELECAO 2: selection sort por capacidade
    void BotaoSelectionSort(object sender, EventArgs e)
    {
        List<Quarto> copia = new List<Quarto>(quartos);

        for (int i = 0; i < copia.Count - 1; i++)
        {
            int menor = i;
            for (int j = i + 1; j < copia.Count; j++)
            {
                if (copia[j].capacidade < copia[menor].capacidade)
                {
                    menor = j;
                }
            }
            Quarto temp = copia[i];
            copia[i] = copia[menor];
            copia[menor] = temp;
        }
        MostrarQuartos(copia);
    }

    // RESERVA: o quarto escolhido e a reserva entram na fila
    void BotaoFazerReserva(object sender, EventArgs e)
    {
        string nome = txtNome.Text;
        if (nome == "")
        {
            MessageBox.Show("Digite o nome do hospede.");
            return;
        }

        int numero;
        if (!int.TryParse(txtQuarto.Text, out numero))
        {
            MessageBox.Show("Digite apenas numeros no campo do quarto.");
            return;
        }

        Quarto escolhido = null;
        foreach (Quarto q in quartos)
        {
            if (q.numero == numero)
            {
                escolhido = q;
            }
        }

        if (escolhido == null)
        {
            MessageBox.Show("Quarto nao existe.");
            return;
        }
        if (!escolhido.disponivel)
        {
            MessageBox.Show("Quarto ocupado.");
            return;
        }

        DateTime data;
        if (!DateTime.TryParse(txtData.Text, out data))
        {
            MessageBox.Show("Data invalida. Use dd/mm/aaaa.");
            return;
        }

        int noites;
        if (!int.TryParse(txtNoites.Text, out noites) || noites <= 0)
        {
            MessageBox.Show("Quantidade de noites invalida.");
            return;
        }

        CriarReserva(nome, numero, data, noites);
        MessageBox.Show("Reserva feita! Total: R$ " + (escolhido.preco * noites));
        MostrarQuartos(quartos);
    }

    // CHECK-IN: atende o primeiro da fila
    void BotaoCheckin(object sender, EventArgs e)
    {
        lista.Items.Clear();
        if (fila.Count == 0)
        {
            Mostrar("Ninguem na fila.");
            return;
        }

        Reserva r = fila.Dequeue();
        Mostrar("Check-in feito:");
        Mostrar(TextoReserva(r));
        Mostrar("Ainda na fila: " + fila.Count);
    }
}
