# TED-Algoritmo-N2
Reserva de Hotel
# Sistema de Reserva de Hotel

Trabalho da disciplina de **Algoritmos** do curso de **Engenharia de Software**.

**Autora:** Alice Rosa Vieira da Silva

## Sobre o projeto

Sistema em **C#** que simula a reserva de quartos de um hotel. É possível consultar quartos, fazer reservas, buscar informações e realizar o check-in dos hóspedes por ordem de chegada.

O projeto tem **duas versões**, que usam as mesmas estruturas de dados e os mesmos algoritmos:

- **Versão de console:** funciona por menu, em tela de texto.
- **Versão com interface gráfica:** janela simples feita com Windows Forms.

O objetivo principal é aplicar **algoritmos de busca, ordenação e seleção** sobre **estruturas de dados lineares**.

## Estrutura do repositório

```
/
├── ReservaHotelConsole/     versão de console (Program.cs e arquivo .csproj)
├── ReservaHotelUI/          versão com interface gráfica (FormPrincipal.cs, Program.cs e .csproj)
├── prints/                  prints de execução
└── README.md
```

## Estruturas de dados lineares utilizadas

| Estrutura | Uso no projeto |
|---|---|
| `List<Quarto>` | Armazena os quartos do hotel |
| `List<Reserva>` | Armazena as reservas feitas |
| `Queue<Reserva>` | Fila de check-in (primeiro a reservar, primeiro a ser atendido) |

## Algoritmos implementados

Todos os algoritmos foram implementados manualmente, sem usar métodos prontos como `Sort()`.

### Busca

| Algoritmo | Onde é usado | Complexidade |
|---|---|---|
| Busca linear | Procurar reservas pelo nome do hóspede | O(n) |
| Busca binária | Procurar um quarto pelo número (lista ordenada) | O(log n) |

### Ordenação

| Algoritmo | Onde é usado | Complexidade |
|---|---|---|
| Insertion Sort | Ordenar quartos por preço da diária | O(n²) |
| Bubble Sort | Ordenar reservas por data de check-in | O(n²) |

### Seleção

| Algoritmo | Onde é usado | Complexidade |
|---|---|---|
| Seleção por filtro | Listar quartos disponíveis até um valor máximo | O(n) |
| Selection Sort | Ordenar quartos por capacidade | O(n²) |

## Funcionalidades

1. Mostrar quartos
2. Buscar reserva pelo nome (busca linear)
3. Buscar quarto pelo número (busca binária)
4. Ordenar quartos por preço (Insertion Sort)
5. Ordenar reservas por data (Bubble Sort)
6. Listar quartos disponíveis até um valor (seleção por filtro)
7. Ordenar quartos por capacidade (Selection Sort)
8. Fazer reserva
9. Fazer check-in (atende a fila)

Na versão de console, cada funcionalidade é uma opção do menu. Na interface gráfica, cada uma é um botão.

## Como executar

**Pré-requisito:** [.NET SDK](https://dotnet.microsoft.com/download) instalado (versão 8).

```bash
git clone https://github.com/SEU-USUARIO/NOME-DO-REPOSITORIO.git
cd NOME-DO-REPOSITORIO
```

**Versão de console:**

```bash
cd ReservaHotelConsole
dotnet run
```

**Versão com interface gráfica** (apenas Windows):

```bash
cd ReservaHotelUI
dotnet run
```

## Testes (prints de execução)

Os prints de execução estão no PDF do trabalho e também na pasta `prints/` do repositório.

## Tecnologias

- Linguagem: C#
- Plataforma: .NET 8
- Interface: console e Windows Forms
- Controle de versão: Git e GitHub
