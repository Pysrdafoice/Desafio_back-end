using System;
using System.Collections.Generic;

namespace Desafio_back_end
{
    public interface IServicoEscolar
    {
        Dictionary<string, Aluno> ObterAlunos();
        Aluno BuscarAluno(string matricula);
        string CadastrarAlunoSomenteNome(string nome);
        string GerarMatriculaAleatoria();
        (string Materia, List<string> Turma) GerarTurmaEComMateriaAleatoria();
        Dictionary<string, double> GerarNotas12Materias();
    }

    public class ServicoEscolar : IServicoEscolar
    {
        private Dictionary<string, Aluno> dicionarioAlunos = new Dictionary<string, Aluno>();
        private Random random = new Random();

        private readonly string[] materias = new string[]
        {
            "Matemática", "Português", "História", "Geografia", "Biologia", "Física",
            "Química", "Inglês", "Artes", "Educação Física", "Filosofia", "Sociologia"
        };

        public ServicoEscolar() => PreCadastrarAlunosIniciais();
        private void PreCadastrarAlunosIniciais()
        {
            string[] nomesIniciais = { "Ana Silva", "Bruno Souza", "Carla Dias", "Daniel Oliveira", "Elena Costa" };
            foreach (var nome in nomesIniciais)
            {
                CadastrarAlunoSomenteNome(nome);
            }
        }

        public Dictionary<string, Aluno> ObterAlunos() => dicionarioAlunos;

        public Aluno BuscarAluno(string matricula)
        {
            if (dicionarioAlunos.TryGetValue(matricula, out var aluno))
                return aluno;
            return null;
        }

        public string GerarMatriculaAleatoria()
        {
            string matricula;

            do
            {
                matricula = random.Next(100000, 1000000).ToString();
            } while (dicionarioAlunos.ContainsKey(matricula));

            return matricula;
        }

        public string CadastrarAlunoSomenteNome(string nome)
        {
            string matricula = GerarMatriculaAleatoria();
            Aluno novoAluno = new Aluno
            {
                Nome = nome,
                Matricula = matricula,
                NotasPorMateria = GerarNotas12Materias()
            };

            dicionarioAlunos.Add(matricula, novoAluno);
            return matricula;
        }

        public (string Materia, List<string> Turma) GerarTurmaEComMateriaAleatoria()
        {
            string materiaSorteada = materias[random.Next(materias.Length)];
            int quantidade = random.Next(10, 31);
            List<string> turma = new List<string>();

            for (int i = 1; i <= quantidade; i++)
            {
                turma.Add($"Aluno Simulado {i}");
            }

            return (materiaSorteada, turma);
        }

        public Dictionary<string, double> GerarNotas12Materias()
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