using Desafio_back_end.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_back_end
{
    internal class MenuAluno
    {

        // =========================
        // MENU ALUNO
        // =========================

        static void MenuAluno()
        {
            string opcaoAluno = "";


            do
            {
                try
                {
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


        // =========================
        // TURMA DO ALUNO
        // =========================

        static void ExibirTurmaAluno()
        {
            try
            {
                Console.WriteLine(
                    "\n--- VERIFICAR TURMA DO ALUNO ---");


                Console.Write(
                    "Informe sua Matrícula " +
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
                        "\nMatrícula não cadastrada!");

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
        // NOTAS DO ALUNO
        // =========================

        static void ExibirNotasAluno()
        {
            try
            {
                Console.WriteLine(
                    "\n--- CONSULTA DE NOTAS ---");


                Console.Write(
                    "Informe sua Matrícula " +
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
                        "\nMatrícula não cadastrada!");

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
        // LISTA DE ALUNOS
        // =========================

        static void ExibirDicionarioAlunos()
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
    }
}
