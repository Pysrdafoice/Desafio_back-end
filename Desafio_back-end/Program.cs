using Desafio_back_end.Menus
using Desafio_back_end.Models;
using Desafio_back_end.Services;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Desafio_back_end
{
    internal class Program
    {
        static IServicoEscolar servico =
            new ServicoEscolar();

        static void Main(string[] args)
        {
            string opcaoMain = "";

            do
            {
                try
                {
                    Console.WriteLine("\n=================================");
                    Console.WriteLine("       SISTEMA ESCOLAR");
                    Console.WriteLine("=================================");
                    Console.WriteLine("1 - ÁREA DO ALUNO");
                    Console.WriteLine("2 - ÁREA DO PROFESSOR");
                    Console.WriteLine("0 - SAIR");
                    Console.Write("Escolha uma opção: ");

                    opcaoMain = Console.ReadLine() ?? "";

                    switch (opcaoMain)
                    {
                        case "1":
                            MenuAluno menuAluno =
                                new MenuAluno(servico);

                            menuAluno.menuAluno();
                            break;

                        case "2":
                            MenuProfessor menuProfessor =
                                new MenuProfessor(servico);

                            menuProfessor.menuProfessor();
                            break;

                        case "0":
                            Console.WriteLine(
                                "\nEncerrando o programa...");

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
                        $"\n[Erro inesperado]: {ex.Message}");
                }

            } while (opcaoMain != "0");
        }


        // ============================================================
        // VALIDAÇÕES
        // ============================================================

        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException(
                    "O nome não pode estar em branco.");
            }

            if (!Regex.IsMatch(
                nome,
                @"^[a-zA-ZÀ-ÿ\s]+$"))
            {
                throw new ArgumentException(
                    "O nome não pode conter números " +
                    "ou caracteres especiais.");
            }
        }


        public static void ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                throw new ArgumentException(
                    "O CPF é obrigatório.");
            }

            if (!Regex.IsMatch(cpf, @"^\d{11}$"))
            {
                throw new FormatException(
                    "O CPF deve conter exatamente " +
                    "11 dígitos numéricos.");
            }
        }


        public static DateTime ValidarData(
            string dataTexto)
        {
            if (string.IsNullOrWhiteSpace(dataTexto))
            {
                throw new ArgumentException(
                    "A data de nascimento é obrigatória.");
            }

            if (!DateTime.TryParseExact(
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
                    "A data de nascimento não pode " +
                    "ser no futuro.");
            }

            return data;
        }


        public static void ValidarMatriculaFormat(
            string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
            {
                throw new ArgumentException(
                    "A matrícula é obrigatória.");
            }

            if (!Regex.IsMatch(
                matricula,
                @"^\d{6}$"))
            {
                throw new FormatException(
                    "A matrícula deve conter " +
                    "exatamente 6 dígitos numéricos.");
            }
        }
    }
}