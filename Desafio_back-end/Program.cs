using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Desafio_back_end.Models;
using Desafio_back_end.Services;

namespace Desafio_back_end
{
    internal class Program
    {
        static IServicoEscolar servico =
            new ServicoEscolar();


        // =========================
        // MENU PRINCIPAL
        // =========================

        static void Main(string[] args)
        {
            string opcaoMain = "";


            do
            {
                try
                {
                    Console.WriteLine(
                        "\n*** SISTEMA ESCOLAR ***");

                    Console.WriteLine(
                        "1 - ÁREA DO ALUNO");

                    Console.WriteLine(
                        "2 - ÁREA DO PROFESSOR");

                    Console.WriteLine(
                        "0 - SAIR");

                    Console.Write(
                        "Escolha uma opção: ");


                    opcaoMain =
                        Console.ReadLine() ?? "";


                    switch (opcaoMain)
                    {
                        case "1":
                            MenuAluno();
                            break;

                        case "2":
                            MenuProfessor();
                            break;

                        case "0":
                            Console.WriteLine(
                                "Encerrando o programa...");
                            break;

                        default:
                            Console.WriteLine(
                                "\nOpção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"\n[Erro Inesperado]: " +
                        $"{ex.Message}");
                }

            } while (opcaoMain != "0");
        }


        // =========================
        // MENU PROFESSOR
        // =========================

        static void MenuProfessor()
        {
            string opcaoProf = "";


            do
            {
                try
                {
                    Console.WriteLine(
                        "\n--- MENU PROFESSOR ---");

                    Console.WriteLine(
                        "1 - Registrar Aluno");

                    Console.WriteLine(
                        "2 - Registrar Notas");

                    Console.WriteLine(
                        "3 - Cadastrar Professor");

                    Console.WriteLine(
                        "4 - Holerite");

                    Console.WriteLine(
                        "0 - Voltar ao Menu Principal");

                    Console.Write(
                        "Escolha uma opção: ");


                    opcaoProf =
                        Console.ReadLine() ?? "";


                    switch (opcaoProf)
                    {
                        case "1":
                            RegistrarAluno();
                            break;

                        case "2":
                            RegistrarNota();
                            break;

                        case "3":
                            RegistrarProfessor();
                            break;

                        case "4":
                            ExibirHolerite();
                            break;

                        case "0":
                            break;

                        default:
                            Console.WriteLine(
                                "\nOpção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"\n[Erro no Menu Professor]: " +
                        $"{ex.Message}");
                }

            } while (opcaoProf != "0");
        }


        // =========================
        // REGISTRAR ALUNO
        // =========================

        static void RegistrarAluno()
        {
            try
            {
                Console.WriteLine("\n--- REGISTRO DE ALUNO ---");

                Console.Write("Digite o Nome do Aluno: ");
                string nome = Console.ReadLine();
                ValidarNome(nome);

                Console.Write("Digite o CPF do Aluno (somente números): ");
                string cpf = Console.ReadLine();
                if (!Regex.IsMatch(cpf, @"^\d{11}$"))
                    throw new ArgumentException("CPF inválido! Deve conter exatamente 11 dígitos numéricos.");

                Console.Write("Digite a Data de Nascimento (dd/mm/aaaa): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
                {
                    throw new ArgumentException("Data de nascimento inválida! Use o formato dd/mm/aaaa.");
                }

                Console.Write("Informe o Ano Letivo do Aluno (1, 2 ou 3): ");
                string ano = Console.ReadLine();
                if (!Regex.IsMatch(ano, @"^[1-3]$"))
                    throw new ArgumentException("Ano inválido! Escolha entre 1, 2 ou 3.");

                Console.Write(
                    "Informe o CPF do Aluno (11 dígitos): ");

                string cpf =
                    Console.ReadLine() ?? "";

                string matriculaGerada = servico.CadastrarAluno(nome, cpf, dataNascimento, ano, turno, letraTurma);


                Console.Write(
                    "Informe a Data de Nascimento " +
                    "(dd/MM/yyyy): ");

                DateTime dataNascimento =
                    ValidarData(
                        Console.ReadLine() ?? "");


                string matriculaGerada =
                    servico.CadastrarAluno(
                        nome,
                        cpf,
                        dataNascimento);


                Console.WriteLine(
                    "\nA matricula foi realizada com sucesso!");

                Console.WriteLine(
                    $"A sua Matricula é: " +
                    $"{matriculaGerada}");

                Console.WriteLine(
                    "Use a matricula para ter acesso " +
                    "ao seu sistema de aluno.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");
            }
        }


        // =========================
        // REGISTRAR PROFESSOR
        // =========================

        static void RegistrarProfessor()
        {
            try
            {
                Console.WriteLine(
                    "\n--- REGISTRO DE PROFESSOR ---");


                Console.Write(
                    "Digite o Nome do Professor: ");

                string nome =
                    Console.ReadLine() ?? "";

                ValidarNome(nome);


                Console.Write(
                    "Informe o CPF do Professor " +
                    "(11 dígitos): ");

                string cpf =
                    Console.ReadLine() ?? "";

                ValidarCPF(cpf);


                Console.Write(
                    "Informe a Data de Nascimento " +
                    "(dd/MM/yyyy): ");

                DateTime dataNascimento =
                    ValidarData(
                        Console.ReadLine() ?? "");


                Professor professor =
                    servico.CadastrarProfessor(
                        nome,
                        cpf,
                        dataNascimento);


                Console.WriteLine(
                    "\ncadastro de professor(A) " +
                    "feito com sucesso");


                Console.WriteLine(
                    $"Salário Base: " +
                    $"R$ {Professor.SalarioBase:N2}");


                if (professor.Turmas.Count > 0)
                {
                    Console.WriteLine(
                        "Salas/Turmas atribuídas: " +
                        $"{string.Join(
                            ", ",
                            professor.Turmas)}");
                }
                else
                {
                    Console.WriteLine(
                        "Nenhuma sala/turma foi atribuída.");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");
            }
        }


        // =========================
        // REGISTRAR NOTA
        // =========================

        static void RegistrarNota()
        {
            try
            {
                Console.WriteLine(
                    "\n--- REGISTRO DE NOTA ---");


                ExibirDicionarioAlunos();


                Console.Write(
                    "Informe a Matrícula do aluno " +
                    "(6 dígitos): ");

                string matricula =
                    Console.ReadLine() ?? "";

                ValidarMatriculaFormat(
                    matricula);


                Aluno aluno =
                    servico.BuscarAluno(
                        matricula);


                if (aluno == null)
                {
                    Console.WriteLine(
                        "\nAluno não encontrado!");

                    return;
                }


                Console.WriteLine(
                    "\n--- MATÉRIAS ---");


                foreach (
                    Materia materia
                    in Enum.GetValues(
                        typeof(Materia)))
                {
                    Console.WriteLine(
                        $"{(int)materia} = " +
                        $"{NomeMateria(materia)}");
                }


                Console.Write(
                    "\nInforme o número da matéria: ");


                if (
                    !int.TryParse(
                        Console.ReadLine(),
                        out int codigoMateria)
                    ||
                    !Enum.IsDefined(
                        typeof(Materia),
                        codigoMateria))
                {
                    throw new FormatException(
                        "Matéria inválida. " +
                        "Escolha um número de 1 a 12.");
                }


                Materia materiaSelecionada =
                    (Materia)codigoMateria;


                Console.Write(
                    $"Digite a nota de " +
                    $"{NomeMateria(materiaSelecionada)} " +
                    $"para {aluno.Nome} (0 a 10): ");


                if (
                    !double.TryParse(
                        Console.ReadLine(),
                        NumberStyles.Float,
                        CultureInfo.GetCultureInfo(
                            "pt-BR"),
                        out double nota))
                {
                    throw new FormatException(
                        "A nota deve ser um número válido.");
                }


                if (nota < 0 || nota > 10)
                {
                    throw new FormatException(
                        "A nota deve estar entre 0 e 10.");
                }


                servico.RegistrarNota(
                    matricula,
                    materiaSelecionada,
                    nota);


                Console.WriteLine(
                    $"\nNota {nota:N1} " +
                    "registrada com sucesso!");

                Console.WriteLine(
                    $"Matéria: " +
                    $"{NomeMateria(materiaSelecionada)}");

                Console.WriteLine(
                    $"Aluno: {aluno.Nome}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");
            }
        }


        // =========================
        // HOLERITE
        // =========================

        static void ExibirHolerite()
        {
            try
            {
                Console.WriteLine(
                    "\n--- HOLERITE DO PROFESSOR ---");


                Console.Write(
                    "Informe o CPF do professor: ");

                string cpf =
                    Console.ReadLine() ?? "";

                ValidarCPF(cpf);


                Professor professor =
                    servico.BuscarProfessor(cpf);


                if (professor == null)
                {
                    Console.WriteLine(
                        "\nProfessor não encontrado.");

                    return;
                }


                Console.WriteLine(
                    "\n========================================");

                Console.WriteLine(
                    "              HOLERITE");

                Console.WriteLine(
                    "========================================");


                Console.WriteLine(
                    $"Professor: {professor.Nome}");

                Console.WriteLine(
                    $"CPF: {professor.CPF}");

                Console.WriteLine(
                    $"Salário Base: " +
                    $"R$ {Professor.SalarioBase:N2}");


                Console.WriteLine(
                    "\nSalas/Turmas em que deu aula:");


                if (professor.Turmas.Count == 0)
                {
                    Console.WriteLine(
                        "- Nenhuma turma atribuída.");
                }
                else
                {
                    foreach (
                        string turma
                        in professor.Turmas)
                    {
                        Console.WriteLine(
                            $"- {turma}");
                    }
                }


                Console.WriteLine(
                    "========================================");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");
            }
        }

        MenuAluno opMenuAluno = new MenuAluno();


        // =========================
        // VALIDAÇÕES
        // =========================

        static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException(
                    "O nome não pode estar em branco.");
            }

            if (
                !Regex.IsMatch(
                    nome,
                    @"^[a-zA-ZÀ-ÿ\s]+$"))
            {
                throw new ArgumentException(
                    "O nome não pode conter " +
                    "números ou caracteres especiais!");
            }
        }


        static void ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                throw new ArgumentException(
                    "O CPF é obrigatório.");
            }


            if (
                !Regex.IsMatch(
                    cpf,
                    @"^\d{11}$"))
            {
                throw new FormatException(
                    "O CPF deve conter " +
                    "exatamente 11 dígitos numéricos!");
            }
        }


        static DateTime ValidarData(
            string dataTexto)
        {
            if (string.IsNullOrWhiteSpace(
                dataTexto))
            {
                throw new ArgumentException(
                    "A data de nascimento " +
                    "é obrigatória.");
            }


            if (
                !DateTime.TryParseExact(
                    dataTexto,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime data))
            {
                throw new FormatException(
                    "Data inválida! " +
                    "Use o formato dd/MM/yyyy.");
            }


            if (data > DateTime.Today)
            {
                throw new ArgumentException(
                    "A data de nascimento " +
                    "não pode ser no futuro.");
            }


            return data;
        }


        static void ValidarMatriculaFormat(
            string matricula)
        {
            if (
                string.IsNullOrWhiteSpace(
                    matricula))
            {
                throw new ArgumentException(
                    "A matrícula é obrigatória.");
            }


            if (
                !Regex.IsMatch(
                    matricula,
                    @"^\d{6}$"))
            {
                throw new FormatException(
                    "A matrícula deve conter " +
                    "exatamente 6 dígitos numéricos!");
            }
        }


        // =========================
        // NOME DAS MATÉRIAS
        // =========================

        static string NomeMateria(
            Materia materia)
        {
            switch (materia)
            {
                case Materia.Matematica:
                    return "Matemática";

                case Materia.Portugues:
                    return "Português";

                case Materia.Historia:
                    return "História";

                case Materia.Geografia:
                    return "Geografia";

                case Materia.Biologia:
                    return "Biologia";

                case Materia.Fisica:
                    return "Física";

                case Materia.Quimica:
                    return "Química";

                case Materia.Ingles:
                    return "Inglês";

                case Materia.Artes:
                    return "Artes";

                case Materia.EducacaoFisica:
                    return "Educação Física";

                case Materia.Filosofia:
                    return "Filosofia";

                case Materia.Sociologia:
                    return "Sociologia";

                default:
                    return materia.ToString();
            }
        }
    }
}