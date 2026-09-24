using System;
using Desafio_back_end.Models;
using Desafio_back_end.Services;

namespace Desafio_back_end
{
    internal class MenuAluno
    {
        private readonly IServicoEscolar servico;


        // =========================
        // CONSTRUTOR
        // =========================

        public MenuAluno(
            IServicoEscolar servico)
        {
            this.servico = servico;
        }


        // =========================
        // MENU ALUNO
        // =========================

        public void menuAluno()
        {
            string opcaoAluno = "";

            do
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine(
                        "\n--- MENU ALUNO ---");

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

                            Console.ReadKey();

                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"\n[Erro no Menu Aluno]: " +
                        $"{ex.Message}");

                    Console.ReadKey();
                }

            } while (opcaoAluno != "0");
        }


        // =========================
        // TURMA DO ALUNO
        // =========================

        private void ExibirTurmaAluno()
        {
            try
            {
                Console.Clear();

                Console.WriteLine(
                    "\n--- VERIFICAR TURMA DO ALUNO ---");

                Console.Write(
                    "Informe sua Matrícula " +
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
                        "\nMatrícula não cadastrada!");

                    Console.WriteLine(
                        "\nPressione ENTER para voltar.");

                    Console.ReadLine();

                    return;
                }


                var dadosTurma =
                    servico.ObterTurmaComMateria(
                        aluno.CodigoTurma);


                Console.WriteLine(
                    "\n==================================================");

                Console.WriteLine(
                    $" ALUNO: {aluno.Nome} " +
                    $"| MATRÍCULA: {aluno.Matricula}");

                Console.WriteLine(
                    $" TURMA: {aluno.CodigoTurma} " +
                    $"| MATÉRIA SORTEADA: " +
                    $"{NomeMateria(dadosTurma.Materia)}");

                Console.WriteLine(
                    "==================================================");


                Console.WriteLine(
                    $" LISTA DE ALUNOS DA TURMA " +
                    $"({dadosTurma.Colegas.Count} Integrantes):");


                foreach (
                    string colega
                    in dadosTurma.Colegas)
                {
                    Console.WriteLine(
                        $" - {colega}");
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
        // NOTAS DO ALUNO
        // =========================

        private void ExibirNotasAluno()
        {
            try
            {
                Console.Clear();

                Console.WriteLine(
                    "\n--- CONSULTA DE NOTAS ---");

                Console.Write(
                    "Informe sua Matrícula " +
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
                        "\nMatrícula não cadastrada!");

                    Console.WriteLine(
                        "\nPressione ENTER para voltar.");

                    Console.ReadLine();

                    return;
                }


                Console.WriteLine(
                    "\n========================================");

                Console.WriteLine(
                    $"ALUNO: {aluno.Nome}");

                Console.WriteLine(
                    $"MATRÍCULA: {aluno.Matricula}");

                Console.WriteLine(
                    $"TURMA: {aluno.CodigoTurma}");

                Console.WriteLine(
                    "========================================");


                double soma = 0;

                int quantidadeNotas = 0;


                Console.WriteLine(
                    "\n--- NOTAS NAS 12 MATÉRIAS ---");


                foreach (
                    Materia materia
                    in Enum.GetValues(
                        typeof(Materia)))
                {
                    if (
                        aluno.MateriasComNota
                            .Contains(materia))
                    {
                        double nota =
                            aluno.NotasPorMateria[
                                materia];


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
                        $"\nMÉDIA DAS NOTAS " +
                        $"REGISTRADAS: " +
                        $"{soma / quantidadeNotas:N2}");
                }
                else
                {
                    Console.WriteLine(
                        "\nMÉDIA: Ainda não existem " +
                        "notas registradas.");
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