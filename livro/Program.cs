// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static List<Livro> biblioteca = new List<Livro>();
    static Dictionary<string, int> emprestimosPorUsuario = new Dictionary<string, int>();

    static void Main()
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("---------- Biblioteca ----------");
            Console.WriteLine("Escolha com qual tipo de conta entrar:");
            Console.WriteLine("1 - Administrador.");
            Console.WriteLine("2 - Usuário.");
            Console.WriteLine("3 - Sair.");

            if (int.TryParse(Console.ReadLine(), out int tipoConta))
            {
                switch (tipoConta)
                {
                    case 1:
                        MenuAdministrador();
                        break;

                    case 2:
                        MenuUsuario();
                        break;

                    case 3:
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }

    static void MenuAdministrador()
    {
        Console.WriteLine("Escolha o que deseja fazer:");
        Console.WriteLine("1 - Cadastrar novos livros.");
        Console.WriteLine("2 - Sair.");

        if (int.TryParse(Console.ReadLine(), out int opcaoAdm))
        {
            switch (opcaoAdm)
            {
                case 1:
                    CadastrarLivro();
                    break;

                case 2:
                    Console.WriteLine("Saindo...");
                    Thread.Sleep(1800);
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static void MenuUsuario()
    {
        Console.WriteLine("Digite seu nome:");
        string nomeUsuario = Console.ReadLine();

        if (!emprestimosPorUsuario.ContainsKey(nomeUsuario))
        {
            emprestimosPorUsuario[nomeUsuario] = 0; // Inicializa o contador de empréstimos do usuário
        }

        bool continuarUsuario = true;
        while (continuarUsuario)
        {
            Console.WriteLine("Escolha o que deseja fazer:");
            Console.WriteLine("1 - Pegar um livro emprestado.");
            Console.WriteLine("2 - Devolver um livro.");
            Console.WriteLine("3 - Sair.");

            if (int.TryParse(Console.ReadLine(), out int opcaoUsu))
            {
                switch (opcaoUsu)
                {
                    case 1:
                        PegarEmprestado(nomeUsuario);
                        break;

                    case 2:
                        DevolverLivro();
                        break;

                    case 3:
                        continuarUsuario = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }
    }

    static void CadastrarLivro()
    {
        Console.Write("Autor do livro: ");
        string autor = Console.ReadLine();
        Console.Write("Nome do livro: ");
        string nomeLivro = Console.ReadLine();
        Console.Write("Gênero do livro: ");
        string genero = Console.ReadLine();

        biblioteca.Add(new Livro(autor, nomeLivro, genero));
        Console.WriteLine("Livro cadastrado com sucesso!");
        Console.ReadLine();
    }

    static void PegarEmprestado(string usuario)
    {
        if (emprestimosPorUsuario[usuario] >= 3)
        {
            Console.WriteLine("Você já pegou o máximo de 3 livros emprestados.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Livros disponíveis:");
        foreach (var livro in biblioteca)
        {
            Console.WriteLine($"Autor: {livro.Autor}, Nome: {livro.Nome}, Gênero: {livro.Genero}");
        }

        Console.Write("Digite o nome do livro que deseja pegar emprestado: ");
        string nomeLivro = Console.ReadLine();

        var livroEncontrado = biblioteca.Find(l => l.Nome.Equals(nomeLivro, StringComparison.OrdinalIgnoreCase));
        if (livroEncontrado != null)
        {
            biblioteca.Remove(livroEncontrado);
            emprestimosPorUsuario[usuario]++;
            Console.WriteLine($"Você pegou '{livroEncontrado.Nome}' emprestado com sucesso!");
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }

        Console.ReadLine();
    }

    static void DevolverLivro()
    {
        Console.Write("Digite o nome do livro que deseja devolver: ");
        string nomeLivro = Console.ReadLine();

        Console.Write("Autor do livro: ");
        string autor = Console.ReadLine();
        Console.Write("Gênero do livro: ");
        string genero = Console.ReadLine();

        biblioteca.Add(new Livro(autor, nomeLivro, genero));
        Console.WriteLine($"Você devolveu '{nomeLivro}' com sucesso!");
        Console.ReadLine();
    }
}

class Livro
{
    public string Autor { get; set; }
    public string Nome { get; set; }
    public string Genero { get; set; }

    public Livro(string autor, string nome, string genero)
    {
        Autor = autor;
        Nome = nome;
        Genero = genero;
    }
}
