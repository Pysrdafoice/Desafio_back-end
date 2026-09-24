using System;
using System.Globalization;
using Desafio_back_end.Models;
using Desafio_back_end.Services;

namespace Desafio_back_end
{
    internal class MenuProfessor
    {
        private readonly IServicoEscolar servico;


        public MenuProfessor(
            IServicoEscolar servico)
        {
            this.servico = servico;
        }


        public void menuProfessor()
        {
            string opcaoProf = "";

            do
            {
                try
                {
                    Console.WriteLine(
                        "\n=================================");

                    Console.WriteLine(
                        "       MENU PROFESSOR");

                    Console.WriteLine(
                        "=================================");

                    Console.WriteLine(
                        "1 - Registrar Aluno");

                    Console.WriteLine(
                        "2 - Registrar Nota");

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


        // ============================================================
        // REGISTRAR ALUNO
        // ============================================================

        private void RegistrarAluno()
        {
            try
            {
                Console.WriteLine(
                    "\n--- REGISTRO DE ALUNO ---");

                Console.Write(
                    "Digite o nome do aluno: ");

                string nome =
                    Console.ReadLine() ?? "";

                Program.ValidarNome(nome);


                Console.Write(
                    "Informe o CPF do aluno: ");

                string cpf =
                    Console.ReadLine() ?? "";

                Program.ValidarCPF(cpf);


                Console.Write(
                    "Informe a data de nascimento " +
                    "(dd/MM/yyyy): ");

                DateTime dataNascimento =
                    Program.ValidarData(
                        Console.ReadLine() ?? "");


                string matricula =
                    servico.CadastrarAluno(
                        nome,
                        cpf,
                        dataNascimento);


                Console.WriteLine(
                    "\nA matricula foi realizada " +
                    "com sucesso!");

                Console.WriteLine(
                    $"A sua Matricula é: " +
                    $"{matricula}");

                Console.WriteLine(
                    "Use a matricula para ter acesso " +
                    "ao seu sistema de aluno.");

                Console.WriteLine(
                    "\nPressione ENTER para continuar.");

                Console.ReadLine();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");

                Console.ReadLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");

                Console.ReadLine();
            }
        }


        // ============================================================
        // REGISTRAR PROFESSOR
        // ============================================================

        private void RegistrarProfessor()
        {
            try
            {
                Console.WriteLine(
                    "\n--- REGISTRO DE PROFESSOR ---");

                Console.Write(
                    "Digite o nome do professor: ");

                string nome =
                    Console.ReadLine() ?? "";

                Program.ValidarNome(nome);


                Console.Write(
                    "Informe o CPF do professor: ");

                string cpf =
                    Console.ReadLine() ?? "";

                Program.ValidarCPF(cpf);


                Console.Write(
                    "Informe a data de nascimento " +
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


                Console.WriteLine(
                    "\nTurmas atribuídas:");

                if (professor.Turmas.Count == 0)
                {
                    Console.WriteLine(
                        "- Nenhuma turma atribuída.");
                }
                else
                {
                    foreach (string turma
                        in professor.Turmas)
                    {
                        Console.WriteLine(
                            $"- {turma}");
                    }
                }

                Console.WriteLine(
                    "\nPressione ENTER para continuar.");

                Console.ReadLine();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");

                Console.ReadLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");

                Console.ReadLine();
            }
        }


        // ============================================================
        // REGISTRAR NOTA
        // ============================================================

        private void RegistrarNota()
        {
            try
            {
                Console.WriteLine(
                    "\n--- REGISTRO DE NOTA ---");

                Console.Write(
                    "Informe a matrícula do aluno: ");

                string matricula =
                    Console.ReadLine() ?? "";

                Program.ValidarMatriculaFormat(
                    matricula);


                Aluno aluno =
                    servico.BuscarAluno(matricula);

                if (aluno == null)
                {
                    Console.WriteLine(
                        "\nAluno não encontrado!");

                    Console.ReadLine();
                    return;
                }


                Console.WriteLine(
                    "\n--- MATÉRIAS ---");

                foreach (Materia materia
                    in Enum.GetValues(typeof(Materia)))
                {
                    Console.WriteLine(
                        $"{(int)materia} - " +
                        $"{NomeMateria(materia)}");
                }


                Console.Write(
                    "\nInforme o número da matéria: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int codigoMateria))
                {
                    throw new FormatException(
                        "Informe um número válido.");
                }


                if (!Enum.IsDefined(
                    typeof(Materia),
                    codigoMateria))
                {
                    throw new FormatException(
                        "Matéria inválida. " +
                        "Escolha de 1 a 12.");
                }


                Materia materiaSelecionada =
                    (Materia)codigoMateria;


                Console.Write(
                    $"\nDigite a nota de " +
                    $"{NomeMateria(materiaSelecionada)} " +
                    $"para {aluno.Nome}: ");


                if (!double.TryParse(
                    Console.ReadLine(),
                    NumberStyles.Float,
                    CultureInfo.GetCultureInfo("pt-BR"),
                    out double nota))
                {
                    throw new FormatException(
                        "A nota deve ser um número válido.");
                }


                if (nota < 0 || nota > 10)
                {
                    throw new ArgumentException(
                        "A nota deve estar entre 0 e 10.");
                }


                servico.RegistrarNota(
                    matricula,
                    materiaSelecionada,
                    nota);


                Console.WriteLine(
                    "\nNota registrada com sucesso!");

                Console.WriteLine(
                    $"Aluno: {aluno.Nome}");

                Console.WriteLine(
                    $"Matéria: " +
                    $"{NomeMateria(materiaSelecionada)}");

                Console.WriteLine(
                    $"Nota: {nota:N1}");

                Console.WriteLine(
                    "\nPressione ENTER para continuar.");

                Console.ReadLine();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");

                Console.ReadLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");

                Console.ReadLine();
            }
        }


        // ============================================================
        // HOLERITE
        // ============================================================

        private void ExibirHolerite()
        {
            try
            {
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
                        "\nProfessor não encontrado!");

                    Console.ReadLine();
                    return;
                }


                Console.WriteLine(
                    "\n=================================");

                Console.WriteLine(
                    "            HOLERITE");

                Console.WriteLine(
                    "=================================");

                Console.WriteLine(
                    $"Professor: {professor.Nome}");

                Console.WriteLine(
                    $"CPF: {professor.CPF}");

                Console.WriteLine(
                    $"Salário Base: " +
                    $"R$ {Professor.SalarioBase:N2}");


                Console.WriteLine(
                    "\nTurmas:");

                foreach (string turma
                    in professor.Turmas)
                {
                    Console.WriteLine(
                        $"- {turma}");
                }


                Console.WriteLine(
                    "=================================");

                Console.WriteLine(
                    "\nPressione ENTER para continuar.");

                Console.ReadLine();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Formato]: " +
                    $"{ex.Message}");

                Console.ReadLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"\n[Erro de Validação]: " +
                    $"{ex.Message}");

                Console.ReadLine();
            }
        }


        // ============================================================
        // NOME DAS MATÉRIAS
        // ============================================================

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