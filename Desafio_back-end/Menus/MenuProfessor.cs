using System;
using System.Globalization;
using Desafio_back_end.Models;
using Desafio_back_end.Services;

namespace Desafio_back_end
{
    internal class MenuProfessor
    {
        private readonly IServicoEscolar servico;


        // =========================
        // CONSTRUTOR
        // =========================

        public MenuProfessor(
            IServicoEscolar servico)
        {
            this.servico = servico;
        }


        // =========================
        // MENU PROFESSOR
        // =========================

        public void menuProfessor()
        {
            string opcaoProf = "";

            do
            {
                try
                {
                    Console.Clear();

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

                            Console.ReadKey();

                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"\n[Erro no Menu Professor]: " +
                        $"{ex.Message}");

                    Console.ReadKey();
                }

            } while (opcaoProf != "0");
        }


        // =========================
        // REGISTRAR ALUNO
        // =========================

        private void RegistrarAluno()
        {
            try
            {
                Console.Clear();

                Console.WriteLine(
                    "\n--- REGISTRO DE ALUNO ---");


                Console.Write(
                    "Digite o Nome do Aluno: ");

                string nome =
                    Console.ReadLine() ?? "";

                Program.ValidarNome(nome);


                Console.Write(
                    "Informe o CPF do Aluno " +
                    "(11 dígitos): ");

                string cpf =
                    Console.ReadLine() ?? "";

                Program.ValidarCPF(cpf);


                Console.Write(
                    "Informe a Data de Nascimento " +
                    "(dd/MM/yyyy): ");

                DateTime dataNascimento =
                    Program.ValidarData(
                        Console.ReadLine() ?? "");


                string matriculaGerada =
                    servico.CadastrarAluno(
                        nome,
                        cpf,
                        dataNascimento);


                Console.WriteLine(
                    "\nA matrícula foi realizada " +
                    "com sucesso!");

                Console.WriteLine(
                    $"A sua Matrícula é: " +
                    $"{matriculaGerada}");

                Console.WriteLine(
                    "Use a matrícula para ter acesso " +
                    "ao seu sistema de aluno.");


                Console.WriteLine();

                Console.WriteLine(
                    "Pressione ENTER para voltar.");

                Console.ReadLine();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");

                Console.ReadKey();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");

                Console.ReadKey();
            }
        }


        // =========================
        // REGISTRAR PROFESSOR
        // =========================

        private void RegistrarProfessor()
        {
            try
            {
                Console.Clear();

                Console.WriteLine(
                    "\n--- REGISTRO DE PROFESSOR ---");


                Console.Write(
                    "Digite o Nome do Professor: ");

                string nome =
                    Console.ReadLine() ?? "";

                Program.ValidarNome(nome);


                Console.Write(
                    "Informe o CPF do Professor " +
                    "(11 dígitos): ");

                string cpf =
                    Console.ReadLine() ?? "";

                Program.ValidarCPF(cpf);


                Console.Write(
                    "Informe a Data de Nascimento " +
                    "(dd/MM/yyyy): ");

                DateTime dataNascimento =
                    Program.ValidarData(
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


                Console.WriteLine();

                Console.WriteLine(
                    "Pressione ENTER para voltar.");

                Console.ReadLine();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");

                Console.ReadKey();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");

                Console.ReadKey();
            }
        }


        // =========================
        // REGISTRAR NOTA
        // =========================

        private void RegistrarNota()
        {
            try
            {
                Console.Clear();

                Console.WriteLine(
                    "\n--- REGISTRO DE NOTA ---");


                ExibirDicionarioAlunos();


                Console.Write(
                    "\nInforme a Matrícula do aluno " +
                    "(6 dígitos): ");

                string matricula =
                    Console.ReadLine() ?? "";


                Program.ValidarMatriculaFormat(
                    matricula);


                Aluno aluno =
                    servico.BuscarAluno(
                        matricula);


                if (aluno == null)
                {
                    Console.WriteLine(
                        "\nAluno não encontrado!");

                    Console.WriteLine(
                        "\nPressione ENTER para voltar.");

                    Console.ReadLine();

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


                if (!int.TryParse(
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


                if (!double.TryParse(
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


                Console.WriteLine();

                Console.WriteLine(
                    "Pressione ENTER para voltar.");

                Console.ReadLine();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");

                Console.ReadKey();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");

                Console.ReadKey();
            }
        }


        // =========================
        // HOLERITE
        // =========================

        private void ExibirHolerite()
        {
            try
            {
                Console.Clear();

                Console.WriteLine(
                    "\n--- HOLERITE DO PROFESSOR ---");


                Console.Write(
                    "Informe o CPF do professor: ");

                string cpf =
                    Console.ReadLine() ?? "";


                Program.ValidarCPF(cpf);


                Professor professor =
                    servico.BuscarProfessor(cpf);


                if (professor == null)
                {
                    Console.WriteLine(
                        "\nProfessor não encontrado.");

                    Console.WriteLine(
                        "\nPressione ENTER para voltar.");

                    Console.ReadLine();

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


                Console.WriteLine();

                Console.WriteLine(
                    "Pressione ENTER para voltar.");

                Console.ReadLine();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");

                Console.ReadKey();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");

                Console.ReadKey();
            }
        }


        // =========================
        // LISTA DE ALUNOS
        // =========================

        private void ExibirDicionarioAlunos()
        {
            var dict =
                servico.ObterAlunos();


            Console.WriteLine(
                "\n=======================================================================");

            Console.WriteLine(
                $" DICIONÁRIO DE ALUNOS " +
                $"CADASTRADOS (Total: {dict.Count})");

            Console.WriteLine(
                "=======================================================================");


            foreach (var item in dict)
            {
                Console.WriteLine(
                    $" Chave (Matrícula): " +
                    $"{item.Key} | " +
                    $"Nome: {item.Value.Nome} | " +
                    $"Turma: {item.Value.CodigoTurma}");
            }


            Console.WriteLine(
                "-----------------------------------------------------------------------");
        }


        // =========================
        // NOME DAS MATÉRIAS
        // =========================

        private string NomeMateria(
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