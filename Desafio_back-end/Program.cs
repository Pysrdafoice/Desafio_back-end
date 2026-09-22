using Desafio_back_end.Interface;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Desafio_back_end
{
    internal class Program
    {
        static ServicoEscolar servico = new ServicoEscolar();

        static void Main(string[] args)
        {
            string opcaoMain = "";

            do
            {
                try
                {
                    Console.WriteLine("\n*** SISTEMA ESCOLAR ***");
                    Console.WriteLine("1 - ÁREA DO ALUNO");
                    Console.WriteLine("2 - ÁREA DO PROFESSOR");
                    Console.WriteLine("0 - SAIR");
                    Console.Write("Escolha uma opção: ");

                    opcaoMain = Console.ReadLine();

                    switch (opcaoMain)
                    {
                        case "1":
                            MenuAluno();
                            break;
                        case "2":
                            MenuProfessor();
                            break;
                        case "0":
                            Console.WriteLine("Encerrando o programa...");
                            break;
                        default:
                            Console.WriteLine("\nCaractere inválido, informe somente as opções informadas !!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Erro Inesperado]: {ex.Message}");
                }

            } while (opcaoMain != "0");
        }

        // --- MENU DO PROFESSOR ---
        static void MenuProfessor()
        {
            string opcaoProf = "";

            do
            {
                try
                {
                    Console.WriteLine("\n--- MENU PROFESSOR ---");
                    Console.WriteLine("1 - Registrar Aluno");
                    Console.WriteLine("2 - Registrar Notas");
                    Console.WriteLine("0 - Voltar ao Menu Principal");
                    Console.Write("Escolha uma opção: ");

                    opcaoProf = Console.ReadLine();

                    switch (opcaoProf)
                    {
                        case "1":
                            RegistrarAluno();
                            break;
                        case "2":
                            RegistrarNota();
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("\nCaractere inválido, informe somente as opções informadas !!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Erro no Menu Professor]: {ex.Message}");
                }

            } while (opcaoProf != "0");
        }

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

                Console.Write("Informe o Turno do Aluno (M - Matutino, V - Vespertino, N - Noturno): ");
                string turno = Console.ReadLine();
                if (!Regex.IsMatch(turno, @"^[mVnMvN]$"))
                    throw new ArgumentException("Turno inválido! Escolha entre M, V ou N.");

                Console.Write("Informe a Turma do Aluno (A, B ou C): ");
                string letraTurma = Console.ReadLine();
                if (!Regex.IsMatch(letraTurma, @"^[a-cA-C]$"))
                    throw new ArgumentException("Turma inválida! Escolha entre A, B ou C.");

                string matriculaGerada = servico.CadastrarAluno(nome, cpf, dataNascimento, ano, turno, letraTurma);

                Console.WriteLine($"\nALUNO CADASTRADO COM SUCESSO!");
                Console.WriteLine($"Matrícula Gerada: {matriculaGerada}");

                ExibirDicionarioAlunos();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n[Erro de Validação]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Erro Inesperado]: {ex.Message}");
            }
        }

        static void RegistrarNota()
        {
            try
            {
                Console.WriteLine("\n--- REGISTRO DE NOTA ---");
                ExibirDicionarioAlunos();

                Console.Write("Informe a Matrícula do aluno para registrar a nota (6 dígitos): ");
                string mat = Console.ReadLine();
                ValidarMatriculaFormat(mat);

                Aluno aluno = servico.BuscarAluno(mat);
                if (aluno == null)
                {
                    Console.WriteLine("\nAluno não encontrado para a matrícula informada!");
                    return;
                }

                Console.Write($"Digite a nota para o aluno {aluno.Nome} (0 a 10): ");
                string inputNota = Console.ReadLine();

                if (!double.TryParse(inputNota, out double nota) || nota < 0 || nota > 10)
                {
                    throw new FormatException("A nota deve ser um valor numérico válido entre 0 e 10!");
                }

                aluno.Notas.Add(nota);
                Console.WriteLine($"\nNota {nota} atribuída com sucesso ao aluno {aluno.Nome}!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"\n[Erro de Formato]: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n[Erro de Validação]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Erro Inesperado]: {ex.Message}");
            }
        }

        // --- MENU DO ALUNO ---
        static void MenuAluno()
        {
            string opcaoAluno = "";

            do
            {
                try
                {
                    Console.WriteLine("\n--- MENU ALUNO ---");
                    Console.WriteLine("1 - Verificar Turma");
                    Console.WriteLine("2 - Ver Notas");
                    Console.Write("Escolha uma opção: ");

                    opcaoAluno = Console.ReadLine();

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
                            Console.WriteLine("\nCaractere inválido, informe somente as opções informadas !!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Erro no Menu Aluno]: {ex.Message}");
                }

            } while (opcaoAluno != "0");
        }

        static void ExibirTurmaAluno()
        {
            try
            {
                Console.WriteLine("\n--- VERIFICAR TURMA DO ALUNO ---");
                Console.Write("Informe sua Matrícula (6 dígitos): ");
                string mat = Console.ReadLine();
                ValidarMatriculaFormat(mat);

                Aluno aluno = servico.BuscarAluno(mat);
                if (aluno == null)
                {
                    Console.WriteLine("\nMatrícula não cadastrada no sistema!");
                    return;
                }

                var dadosTurma = servico.ObterTurmaComMateria(aluno.CodigoTurma);

                Console.WriteLine($"\n==================================================");
                Console.WriteLine($" ALUNO: {aluno.Nome} | MATRÍCULA: {aluno.Matricula}");
                Console.WriteLine($" TURMA: {aluno.CodigoTurma} | MATÉRIA SORTEADA: {dadosTurma.Materia.ToUpper()}");
                Console.WriteLine($"==================================================");
                Console.WriteLine($" LISTA DE ALUNOS DA TURMA ({dadosTurma.Colegas.Count} Integrantes):");

                foreach (var colega in dadosTurma.Colegas)
                {
                    Console.WriteLine($" - {colega}");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"\n[Erro de Formato]: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n[Erro de Validação]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Erro Inesperado]: {ex.Message}");
            }
        }

        static void ExibirNotasAluno()
        {
            try
            {
                Console.WriteLine("\n--- CONSULTA DE NOTAS ---");
                Console.Write("Informe sua Matrícula (6 dígitos): ");
                string mat = Console.ReadLine();
                ValidarMatriculaFormat(mat);

                Aluno aluno = servico.BuscarAluno(mat);
                if (aluno == null)
                {
                    Console.WriteLine("\nMatrícula não cadastrada no sistema!");
                    return;
                }

                Console.WriteLine($"\n========================================");
                Console.WriteLine($"ALUNO: {aluno.Nome} | MATRÍCULA: {aluno.Matricula} | TURMA: {aluno.CodigoTurma}");
                Console.WriteLine($"========================================");

                Console.WriteLine("\n--- NOTAS NAS 12 MATÉRIAS ---");
                double soma = 0;
                foreach (var item in aluno.NotasPorMateria)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                    soma += item.Value;
                }

                Console.WriteLine($"\nMÉDIA GERAL: {Math.Round(soma / aluno.NotasPorMateria.Count, 2)}");

                if (aluno.Notas.Count > 0)
                {
                    Console.WriteLine($"Notas registradas pelo professor: {string.Join(", ", aluno.Notas)}");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"\n[Erro de Formato]: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n[Erro de Validação]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Erro Inesperado]: {ex.Message}");
            }
        }

        // --- MÉTODOS AUXILIARES ---
        static void ExibirDicionarioAlunos()
        {
            var dict = servico.ObterAlunos();
            Console.WriteLine($"\n=======================================================================");
            Console.WriteLine($" DICIONÁRIO DE ALUNOS CADASTRADOS (Total: {dict.Count})");
            Console.WriteLine($"=======================================================================");

            foreach (KeyValuePair<string, Aluno> item in dict)
            {
                Console.WriteLine($" Chave (Matrícula): {item.Key} | Nome: {item.Value.Nome} | Turma: {item.Value.CodigoTurma}");
            }
            Console.WriteLine($"-----------------------------------------------------------------------\n");
        }

        static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome não pode estar em branco.");

            if (!Regex.IsMatch(nome, @"^[a-zA-ZÀ-ÿ\s]+$"))
                throw new ArgumentException("O nome não pode conter números ou caracteres especiais!");
        }

        static void ValidarMatriculaFormat(string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new ArgumentException("A matrícula é obrigatória.");

            if (!Regex.IsMatch(matricula, @"^\d{6}$"))
                throw new FormatException("A matrícula deve conter exatamente 6 dígitos numéricos!");
        }
    }
}