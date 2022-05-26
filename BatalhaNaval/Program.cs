﻿/*
 * Elementos * 
 *      2 Matrizes 10x10 (um pra cada jogador)
 *             - Peças -
 *      1 Porta Avião (5 tiles)     PS
 *      2 Navio Tanque (4 tiles)    NT
 *      3 Destroyers (3 tiles)      DS
 *      4 Submarinos (2 tiles)      SB
 * 
 * Ordem do programa * 
 * Perguntar o nome dos jogadores
 * Posicionar as peças
 * 
 * Regras Gerais
O programa só deve receber entradas válidas, ou seja, a sigla deve ser alguma das correspondentes.
A embarcação não pode ser de um tamnaho maior do que o especificado para cada uma delas.
Uma embarcação não pode sobrepor a outra.
Um mapa do campo adversario deve ser mostrado cada vez que o jogador for efetuar um disparo.
O mapa deve representar os espaços da seguinte maneira:
    Em branco: Espaços que não receberam disparos
    Letra 'A': Espaço que recebeu disparo mas não tinha embarcação
    Letra 'X': Espaço que recebeu disparo e tinha uma embarcação.
A cada turno o programa deve limpar a tela e apresentar o tabuleiro adversário
Quando um disparo for ceiteio o programa deve apresentar uma mensagem e também quando o disparo for errado.
 * 
 * 
 * Ideias para bornout:
 * Mudar a cor de fundo do console pra representar as embarcações
 * Fazer um tabuleiro legal :)
 * 
 * 
 * Desafio: Fazer um modo vs. Computador
*/

bool AcabouJogo = false;

Jogador Jogador1 = CadastrarJogador();
Jogador Jogador2 = SegundoJogador();


PreencherTabuleiro(Jogador1);
PreencherTabuleiro(Jogador2);

Console.Clear();
Console.BackgroundColor = ConsoleColor.Blue;
Console.WriteLine($"O jogador {Jogador1.Nome} irá preencher o tabuleiro");
Console.BackgroundColor = ConsoleColor.Black;
Console.WriteLine("Aperte ENTER para continuar");
Console.ReadLine();

do
{
    RenderizarTabuleiro(Jogador1);
    PosicionarPecas(Jogador1);
} while (PosicionarPecas(Jogador1));

RenderizarTabuleiro(Jogador1);
Console.BackgroundColor = ConsoleColor.Blue;
Console.WriteLine($"Tabuleiro completado com SUCESSO");
Console.BackgroundColor = ConsoleColor.Black;
Console.WriteLine("Aperte ENTER para continuar");
Console.ReadLine();

if (Jogador2.EhRobo == false)
{
    Console.Clear();
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.WriteLine($"O jogador {Jogador2.Nome} irá preencher o tabuleiro");
    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine("Aperte ENTER para continuar");
    Console.ReadLine();
    do
    {
        RenderizarTabuleiro(Jogador2);
        PosicionarPecas(Jogador2);
    } while (PosicionarPecas(Jogador2));
}

// --- * ---
// Funções

Jogador CadastrarJogador()
{
    string Nome;
    do
    {
        Console.WriteLine("Digite o nome do jogador");
        Nome = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(Nome))
            Console.WriteLine("Input inválido");
    } while (string.IsNullOrEmpty(Nome));

    Jogador NovoJogador = new Jogador(Nome, new string[10, 10]);

    return NovoJogador;
}

Jogador SegundoJogador()
{
    string Resposta;
    do
    {
        Console.WriteLine("Deseja jogar contra o computador? (S/N)");
        Resposta = Console.ReadLine();
        if (Resposta != "S" && Resposta != "N" || Resposta == null)
        {
            Console.WriteLine("Input incorreto");
        }
    } while (Resposta != "S" && Resposta != "N" || Resposta == null);

    if (Resposta == "S")
    {
        Jogador Robo = new Jogador("Computador", new string[10, 10]);
        Robo.EhRobo = true;
        return Robo;
    }
    else
        return CadastrarJogador();
}

void PreencherTabuleiro(Jogador Jogador)
{
    for (int i = 0; i < Jogador.Tabuleiro.GetLength(0); i++)
        for (int j = 0; j < Jogador.Tabuleiro.GetLength(1); j++)
        {
            Jogador.Tabuleiro[i, j] = ".";
            Jogador.TabuleiroAtaque[i, j] = ".";
        }
}

void RenderizarTabuleiro(Jogador Jogador)
{
    Console.Clear();
    Console.WriteLine($"Vez do jogador: {Jogador.Nome}");
    for (int i = 0; i < Jogador.Tabuleiro.GetLength(0); i++)
    {
        if (i == 0) Console.Write("  ");
        Console.Write(i + 1 + "  ");
    }
    Console.WriteLine();
    for (int i = 0; i < Jogador.Tabuleiro.GetLength(0); i++)
    {
        Console.Write(Convert.ToChar(65 + i));
        for (int j = 0; j < Jogador.Tabuleiro.GetLength(1); j++)
        {
            Console.Write(" ");
            Console.Write(Jogador.Tabuleiro[i, j]);
            if (j < Jogador.Tabuleiro.GetLength(1) - 1) Console.Write(" ");
        }
        Console.WriteLine();
    }
}

void ImprimirErro(string Erro) {
    Console.BackgroundColor = ConsoleColor.Red;
    Console.Write(Erro);
    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine("\nAperte ENTER para reiniciar.");
    Console.ReadLine();
}

bool ValidarCoordenada(string Coordenada)
{
    List<string> Coordenadas = new List<string>() {
        "A","B","C","D","E","F","G","H","I","J"
    };
    if (Coordenadas.Contains(Coordenada[0].ToString()))
    {
        if (int.TryParse(Coordenada[1].ToString(), out int lixo))
        {
            if (Coordenada.Length > 2)
            {
                if (int.TryParse(Coordenada[2].ToString(), out int numero) && numero == 0) return true;
                else return false;
            }
            else return true;
        }
        else return false;
    }
    else return false;
};

bool PosicionarPecas(Jogador Jogador)
{
    Dictionary<string, int> TilesPecas = new Dictionary<string, int>()
    {
        {"PS", 4},
        {"NT", 3},
        {"DS", 2},
        {"SB", 1}
    };

    Dictionary<string, string> TraducaoPecas = new Dictionary<string, string>()
    {
        {"PS", "P"},
        {"NT", "N"},
        {"DS", "D"},
        {"SB", "S"}
    };

    Dictionary<string, string> NomeDasPecas = new Dictionary<string, string>() {
        {"PS", "Porta Avião"},
        {"NT", "Navio Tanque"},
        {"DS", "Destroyer"},
        {"SB", "Submarino"}
    };

    string Input, num, Peca = String.Empty;
    int X1, Y1, X2, Y2;

    do
    {
        Console.Clear();
        RenderizarTabuleiro(Jogador);
        Console.WriteLine();
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine("POSICIONE UMA PEÇA");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine("---------------------------Lista de peças---------------------------");
        Console.WriteLine("Peça \t\t\t Tamanho \t\t\t Quantidade");
        Console.WriteLine();

        foreach (var Sigla in TilesPecas.Keys) {
        Console.WriteLine($"{NomeDasPecas[Sigla]}({Sigla})   \t {TilesPecas[Sigla] + 1} espaços \t\t\t {Jogador.QntPecas[Sigla]}");
        }

        Console.WriteLine("Digite uma Peça (Sigla):");
        Input = Console.ReadLine();
        Input = Input.ToUpper();
        if (string.IsNullOrEmpty(Input) || !Jogador.QntPecas.ContainsKey(Input))
        {
            ImprimirErro("Input inválido");
        }
        else if (Jogador.QntPecas[Input] == 0)
        {
            ImprimirErro($"Você não pode posicionar mais peças do tipo {Input}");
        }
    } while (string.IsNullOrEmpty(Input) || !Jogador.QntPecas.ContainsKey(Input) || Jogador.QntPecas[Input] == 0);

    string PecaEscolhida = Input;
    bool VerificaçãoCoordenadas, EspacoOcupado;
    do
    {
        EspacoOcupado = false;
        do
        {
            VerificaçãoCoordenadas = true;
            do
            {
                Console.WriteLine("Digite a primeira posição:");
                Input = Console.ReadLine();
                if (string.IsNullOrEmpty(Input) || !ValidarCoordenada(Input))
                {
                    Console.WriteLine("Input inválido");
                }
            } while (string.IsNullOrEmpty(Input) || !ValidarCoordenada(Input));

            if (Input.Length > 2) num = (Input[1].ToString() + Input[2]);
            else num = (Input[1].ToString());
            Y1 = int.Parse(num) - 1;
            X1 = (int)Input[0] - 65;

            do
            {
                Console.WriteLine("Digite a segunda posição:");
                Input = Console.ReadLine();
                if (string.IsNullOrEmpty(Input) || !ValidarCoordenada(Input))
                {
                    Console.WriteLine("Input inválido");
                }
            } while (string.IsNullOrEmpty(Input) || !ValidarCoordenada(Input));

            if (Input.Length > 2) num = (Input[1].ToString() + Input[2]);
            else num = (Input[1].ToString());
            Y2 = int.Parse(num) - 1;
            X2 = (int)Input[0] - 65;

            if (Math.Abs(X2 - X1) != TilesPecas[PecaEscolhida] && Math.Abs(Y2 - Y1) != TilesPecas[PecaEscolhida])
            {

                ImprimirErro("A distância das coordendas é diferente do tamanho da peça escolhida");
                VerificaçãoCoordenadas = false;
            }
        } while (!VerificaçãoCoordenadas);

        if (X1 == X2)
        {
            if (Y2 > Y1)
            {
                for (int i = Y1; i <= Y2; i++)
                {
                    if (Jogador.Tabuleiro[X1, i] != ".")
                    {
                        EspacoOcupado = true;
                        break;
                    };
                }
            }
            else
            {
                for (int i = Y2; i <= Y1; i++)
                {
                    if (Jogador.Tabuleiro[X1, i] != ".")
                    {
                        EspacoOcupado = true;
                        break;
                    };
                }


            }
        }
        else
        {
            if (X2 > X1)
            {
                for (int i = X1; i <= X2; i++)
                {
                    if (Jogador.Tabuleiro[i, Y1] != ".")
                    {
                        EspacoOcupado = true;
                        break;
                    };
                }
            }
            else
            {
                for (int i = X2; i <= X1; i++)
                {
                    if (Jogador.Tabuleiro[i, Y1] != ".")
                    {
                        EspacoOcupado = true;
                        break;
                    };
                }
            }
        }
        if (EspacoOcupado) {
            ImprimirErro("O espaço selecionado esta ocupado");
        }
    } while (EspacoOcupado);

    if (X1 == X2)
    {
        if (Y2 > Y1)
        {
            for (int i = Y1; i <= Y2; i++)
            {
                Jogador.Tabuleiro[X1, i] = TraducaoPecas[PecaEscolhida];
            }
        }
        else
        {
            for (int i = Y2; i <= Y1; i++)
            {
                Jogador.Tabuleiro[X1, i] = TraducaoPecas[PecaEscolhida];
            }
        }
    }
    else
    {
        if (X2 > X1)
        {
            for (int i = X1; i <= X2; i++)
            {
                Jogador.Tabuleiro[i, Y1] = TraducaoPecas[PecaEscolhida];
            }
        }
        else
        {
            for (int i = X2; i <= X1; i++)
            {
                Jogador.Tabuleiro[i, Y1] = TraducaoPecas[PecaEscolhida];
            }
        }
    }

    Jogador.QntPecas[PecaEscolhida]--;
    bool temPeca = true;

    foreach (var NumPeca in Jogador.QntPecas)
    {
        temPeca = true;
        if (NumPeca.Value != 0)
        {
            temPeca = true;
            break;
        }
        else temPeca = false;
    }
    return temPeca;
}
public class Jogador
{
    public string Nome { get; set; } = "";
    public string[,] Tabuleiro { get; set; } = new string[10, 10];
    public string[,] TabuleiroAtaque { get; set; } = new string[10, 10];
    public bool EhRobo { get; set; } = false;

    public Dictionary<string, int> QntPecas { get; set; } = new Dictionary<string, int>() {
        {"PS", 1},
        {"NT", 2},
        {"DS", 3},
        {"SB", 4}
    };

    public Jogador(string nome, string[,] tabuleiro)
    {
        Nome = nome;
        Tabuleiro = tabuleiro;
    }
}