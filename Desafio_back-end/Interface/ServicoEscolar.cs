using System;
using System.Collections.Generic;

namespace Desafio_back_end.Interface
{
    public class ServicoEscolar : IServicoEscolar
    {
        private Dictionary<string, Aluno> dicionarioAlunos = new Dictionary<string, Aluno>();
        private Dictionary<string, Professor> dicionarioProfessores = new Dictionary<string, Professor>();
        private Random random = new Random();

        private readonly string[] materias = new string[]
        {
            "Matemática", "Português", "História", "Geografia", "Biologia", "Física",
            "Química", "Inglês", "Artes", "Educação Física", "Filosofia", "Sociologia"
        };

        public ServicoEscolar() => PreCadastrarAlunosIniciais();

        private void PreCadastrarAlunosIniciais()
        {
            CadastrarAlunoPreDefinido("Ana Silva", "AM1");
            CadastrarAlunoPreDefinido("Bruno Souza", "Av2");
            CadastrarAlunoPreDefinido("Carla Dias", "AN3");
            CadastrarAlunoPreDefinido("Daniel Oliveira", "BN1");
            CadastrarAlunoPreDefinido("Elena Costa", "CV2");
        }

        private void CadastrarAlunoPreDefinido(string nome, string codigoTurma)
        {
            string matricula = GerarMatriculaAleatoria();
            Aluno aluno = new Aluno
            {
                Nome = nome,
                CPF = "00000000000",
                DataDeNascimento = new DateTime(2008, 1, 1),
                Matricula = matricula,
                CodigoTurma = codigoTurma,
                NotasPorMateria = GerarNotas12Materias()
            };
            dicionarioAlunos.Add(matricula, aluno);
        }

        // ===================== ALUNOS =====================

        public Dictionary<string, Aluno> ObterAlunos() => dicionarioAlunos;

        public Aluno BuscarAluno(string matricula)
        {
            dicionarioAlunos.TryGetValue(matricula, out var aluno);
            return aluno;
        }

        public string CadastrarAluno(string nome, string cpf, DateTime dataNascimento, string ano, string turno, string letraTurma)
        {
            string codigoTurma = $"{letraTurma.ToUpper()}{turno.ToUpper()}{ano}";
            string matricula = GerarMatriculaAleatoria();

            Aluno novoAluno = new Aluno
            {
                Nome = nome,
                CPF = cpf,
                DataDeNascimento = dataNascimento,
                Matricula = matricula,
                CodigoTurma = codigoTurma,
                NotasPorMateria = GerarNotas12Materias()
            };

            dicionarioAlunos.Add(matricula, novoAluno);
            return matricula;
        }

        public (string Materia, List<string> Colegas) ObterTurmaComMateria(string codigoTurma)
        {
            string materiaSorteada = materias[random.Next(materias.Length)];
            List<string> colegas = new List<string>();

            // Adiciona os alunos reais cadastrados nessa turma
            foreach (var item in dicionarioAlunos.Values)
            {
                if (item.CodigoTurma.Equals(codigoTurma, StringComparison.OrdinalIgnoreCase))
                {
                    colegas.Add($"{item.Nome} (Matrícula: {item.Matricula})");
                }
            }

            // Completa com simulação aleatória para compor a turma
            int quantidadeExtra = random.Next(5, 25);
            for (int i = 1; i <= quantidadeExtra; i++)
            {
                colegas.Add($"Aluno Simulado {i}");
            }

            return (materiaSorteada, colegas);
        }

        // ===================== PROFESSORES =====================

        public Dictionary<string, Professor> ObterProfessores() => dicionarioProfessores;

        public Professor BuscarProfessor(string matricula)
        {
            dicionarioProfessores.TryGetValue(matricula, out var professor);
            return professor;
        }

        public string CadastrarProfessor(string nome, string cpf, DateTime dataNascimento, double salario, List<string> turmas)
        {
            string matricula = GerarMatriculaAleatoria();

            Professor novoProfessor = new Professor
            {
                Nome = nome,
                CPF = cpf,
                DataDeNascimento = dataNascimento,
                Matricula = matricula,
                Salario = salario,
                Turmas = turmas ?? new List<string>()
            };

            dicionarioProfessores.Add(matricula, novoProfessor);
            return matricula;
        }

        // ===================== AUXILIARES =====================

        private string GerarMatriculaAleatoria()
        {
            string matricula;
            do
            {
                matricula = random.Next(100000, 1000000).ToString();
            } while (dicionarioAlunos.ContainsKey(matricula) || dicionarioProfessores.ContainsKey(matricula));

            return matricula;
        }

        private Dictionary<string, double> GerarNotas12Materias()
        {
            var dict = new Dictionary<string, double>();
            foreach (var mat in materias)
            {
                dict[mat] = Math.Round(random.NextDouble() * 10, 1);
            }
            return dict;
        }
    }
}
