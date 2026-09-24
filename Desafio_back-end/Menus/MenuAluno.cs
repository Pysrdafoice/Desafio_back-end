using System;
using Desafio_back_end.Models;
using Desafio_back_end.Services;

namespace Desafio_back_end
{
    internal class MenuAluno
    {
        private readonly IServicoEscolar servico;


        public MenuAluno(IServicoEscolar servico)
        {
            this.servico = servico;
        }


        public void menuAluno()
        {
            string opcaoAluno = "";

            do
            {
                try
                {
                    Console.WriteLine(
                        "\n=================================");

                    Console.WriteLine(
                        "          MENU ALUNO");

                    Console.WriteLine(
                        "=================================");

                    Console.WriteLine(
                        "1 - Verificar Turma");

                    Console.WriteLine(
                        "2 - Ver Notas");

                    Console.WriteLine(
                        "0 - Voltar ao Menu Principal");

                    Console.Write(
                        "Escolha uma opção: ");

                    opcaoAluno =
                        Console.ReadLine() ?? "";

                    switch (opcaoAluno)
                    {
                        case "1":
                            ExibirTurmaAluno();
                            break;

                        case "2":
                            ExibirNotasAluno();
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
                        $"\n[Erro no Menu Aluno]: " +
                        $"{ex.Message}");
                }

            } while (opcaoAluno != "0");
        }


        // ============================================================
        // TURMA
        // ============================================================

        private void ExibirTurmaAluno()
        {
            try
            {
                Console.WriteLine(
                    "\n--- VERIFICAR TURMA ---");

                Console.Write(
                    "Informe sua matrícula: ");

                string matricula =
                    Console.ReadLine() ?? "";

                Program.ValidarMatriculaFormat(
                    matricula);

                Aluno aluno =
                    servico.BuscarAluno(matricula);

                if (aluno == null)
                {
                    Console.WriteLine(
                        "\nMatrícula não cadastrada!");

                    Console.ReadLine();
                    return;
                }

                var dadosTurma =
                    servico.ObterTurmaComMateria(
                        aluno.CodigoTurma);

                Console.WriteLine(
                    "\n=================================");

                Console.WriteLine(
                    $"Aluno: {aluno.Nome}");

                Console.WriteLine(
                    $"Matrícula: {aluno.Matricula}");

                Console.WriteLine(
                    $"Turma: {aluno.CodigoTurma}");

                Console.WriteLine(
                    $"Matéria: " +
                    $"{NomeMateria(dadosTurma.Materia)}");

                Console.WriteLine(
                    "=================================");

                Console.WriteLine(
                    "\nALUNOS DA TURMA:");

                foreach (string colega
                    in dadosTurma.Colegas)
                {
                    Console.WriteLine(
                        $"- {colega}");
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
        // NOTAS
        // ============================================================

        private void ExibirNotasAluno()
        {
            try
            {
                Console.WriteLine(
                    "\n--- CONSULTA DE NOTAS ---");

                Console.Write(
                    "Informe sua matrícula: ");

                string matricula =
                    Console.ReadLine() ?? "";

                Program.ValidarMatriculaFormat(
                    matricula);

                Aluno aluno =
                    servico.BuscarAluno(matricula);

                if (aluno == null)
                {
                    Console.WriteLine(
                        "\nMatrícula não cadastrada!");

                    Console.ReadLine();
                    return;
                }

                Console.WriteLine(
                    "\n=================================");

                Console.WriteLine(
                    $"ALUNO: {aluno.Nome}");

                Console.WriteLine(
                    $"MATRÍCULA: {aluno.Matricula}");

                Console.WriteLine(
                    $"TURMA: {aluno.CodigoTurma}");

                Console.WriteLine(
                    "=================================");

                double soma = 0;
                int quantidadeNotas = 0;

                Console.WriteLine(
                    "\n--- NOTAS NAS 12 MATÉRIAS ---");

                foreach (Materia materia
                    in Enum.GetValues(typeof(Materia)))
                {
                    if (aluno.MateriasComNota
                        .Contains(materia))
                    {
                        double nota =
                            aluno.NotasPorMateria[materia];

                        Console.WriteLine(
                            $"{(int)materia} - " +
                            $"{NomeMateria(materia)}: " +
                            $"{nota:N1}");

                        soma += nota;
                        quantidadeNotas++;
                    }
                    else
                    {
                        Console.WriteLine(
                            $"{(int)materia} - " +
                            $"{NomeMateria(materia)}: " +
                            "Não registrada");
                    }
                }

                if (quantidadeNotas > 0)
                {
                    Console.WriteLine(
                        $"\nMÉDIA DAS NOTAS: " +
                        $"{soma / quantidadeNotas:N2}");
                }
                else
                {
                    Console.WriteLine(
                        "\nMÉDIA: Ainda não existem " +
                        "notas registradas.");
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