using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Desafio_back_end
{
    internal class Program
    {
        static IServicoEscolar servico = new ServicoEscolar();

        static void Main(string[] args)
        {
            bool executando = true;

            while (executando)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\n*** SISTEMA ESCOLAR ***");
                    Console.WriteLine("1 - ÁREA DO ALUNO");
                    Console.WriteLine("2 - ÁREA DO PROFESSOR");
                    Console.WriteLine("0 - SAIR");
                    Console.Write("Escolha uma opção: ");

                    string opcao = Console.ReadLine();
                    

                    switch (opcao)
                    {
                        case "1": MenuAluno(); break;
                        case "2": MenuProfessor(); break;
                        case "0": executando = false; break;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Erro Inesperado]: {ex.Message}");
                }
            }
        }
        static void MenuProfessor()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\n--- MENU PROFESSOR ---");
                Console.WriteLine("1 - Registrar Aluno");
                Console.WriteLine("2 - Lançar Nota");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                string op = Console.ReadLine();
                if (op == "1") RegistrarAluno();
                else if (op == "2") RegistrarNota();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Erro no Menu Professor]: {ex.Message}");
            }
        }

        static void RegistrarAluno()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\n--- REGISTRO DE ALUNO ---");
                Console.Write("Digite o Nome do Aluno: ");
                string nome = Console.ReadLine();

                ValidarNome(nome);

                string matriculaGerada = servico.CadastrarAlunoSomenteNome(nome);
                Console.WriteLine($"\nSUCCESS: Aluno cadastrado! Matrícula de 6 dígitos gerada: {matriculaGerada}");

                ExibirDicionarioAlunos();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[Erro de Validação]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Erro Inesperado]: {ex.Message}");
            }
        }

        static void RegistrarNota()
        {
            try
            {
                Console.WriteLine("\n--- LANÇAMENTO DE NOTA ---");
                ExibirDicionarioAlunos();

                Console.Write("Aviso: Selecione o aluno informando a sua Matrícula de 6 dígitos: ");
                string mat = Console.ReadLine();

                ValidarMatriculaFormat(mat);

                Aluno aluno = servico.BuscarAluno(mat);
                if (aluno == null)
                {
                    Console.WriteLine("Aviso: Aluno não localizado para a matrícula digitada!");
                    return;
                }

                Console.Write($"Digite a nota para o aluno {aluno.Nome} (0 a 10): ");
                string inputNota = Console.ReadLine();

                if (!double.TryParse(inputNota, out double nota) || nota < 0 || nota > 10)
                {
                    throw new FormatException("A nota deve ser um formato numérico válido entre 0 e 10!");
                }

                aluno.Notas.Add(nota);
                Console.WriteLine($"Nota {nota} atribuída com sucesso ao aluno {aluno.Nome}!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"[Erro de Formato]: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[Erro de Validação]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Erro Inesperado]: {ex.Message}");
            }
        }
        static void MenuAluno()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\n--- MENU ALUNO ---");
                Console.WriteLine("1 - Verificar Turma e Matéria Aleatória");
                Console.WriteLine("2 - Consultar Notas por Matrícula");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                string op = Console.ReadLine();
                if (op == "1") ExibirTurma();
                else if (op == "2") ExibirNotasPorMatricula();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Erro no Menu Aluno]: {ex.Message}");
            }
        }

        static void ExibirTurma()
        {
            try
            {
                var resultado = servico.GerarTurmaEComMateriaAleatoria();
                Console.WriteLine($"\n--- MATÉRIA REGISTRADA: {resultado.Materia.ToUpper()} ---");
                Console.WriteLine($"--- TURMA ATUAL ({resultado.Turma.Count} Alunos) ---");

                foreach (var colega in resultado.Turma)
                {
                    Console.WriteLine($"- {colega}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Erro ao Gerar Turma]: {ex.Message}");
            }
        }

        static void ExibirNotasPorMatricula()
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
                    Console.WriteLine("Aviso: Nenhuma matrícula encontrada com este código no dicionário!");
                    return;
                }

                Console.WriteLine($"\n========================================");
                Console.WriteLine($"ALUNO: {aluno.Nome} | MATRÍCULA: {aluno.Matricula}");
                Console.WriteLine($"========================================");

                Console.WriteLine("\n--- NOTAS DAS 12 MATÉRIAS ---");
                double soma = 0;
                foreach (var item in aluno.NotasPorMateria)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                    soma += item.Value;
                }

                Console.WriteLine($"\nMÉDIA GERAL DAS MATÉRIAS: {Math.Round(soma / aluno.NotasPorMateria.Count, 2)}");

                if (aluno.Notas.Count > 0)
                {
                    Console.WriteLine($"Notas avulsas registradas pelo professor: {string.Join(", ", aluno.Notas)}");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"[Erro de Formato]: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[Erro de Validação]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Erro Inesperado]: {ex.Message}");
            }
        }
        static void ExibirDicionarioAlunos()
        {
            var dict = servico.ObterAlunos();
            Console.Clear();
            Console.WriteLine($"\n==================================================");
            Console.WriteLine($" DICIONÁRIO DE ALUNOS (Total Cadastrado: {dict.Count})");
            Console.WriteLine($"==================================================");

            foreach (KeyValuePair<string, Aluno> item in dict)
            {
                Console.WriteLine($" Chave (Matrícula): {item.Key} => Valor (Nome): {item.Value.Nome}");
            }
            Console.WriteLine($"--------------------------------------------------\n");
        }

        static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome não pode estar em branco.");
            }
            if (!Regex.IsMatch(nome, @"^[a-zA-ZÀ-ÿ\s]+$"))
            {
                throw new ArgumentException("O nome não pode conter números nem caracteres especiais!");
            }
        }

        static void ValidarMatriculaFormat(string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
            {
                throw new ArgumentException("A matrícula é obrigatória.");
            }
            if (!Regex.IsMatch(matricula, @"^\d{6}$"))
            {
                throw new FormatException("A matrícula deve conter exatamente 6 dígitos numéricos!");
            }
        }
    }
}