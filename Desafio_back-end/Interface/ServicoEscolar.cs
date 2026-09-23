using System;
using System.Collections.Generic;
using System.Linq;
using Desafio_back_end.Models;

namespace Desafio_back_end.Services
{
    public class ServicoEscolar : IServicoEscolar
    {
        private readonly Dictionary<string, Aluno> dicionarioAlunos
            = new Dictionary<string, Aluno>();

        private readonly Dictionary<string, Professor> dicionarioProfessores
            = new Dictionary<string, Professor>();

        private readonly Random random = new Random();

        private readonly Materia[] materias =
            (Materia[])Enum.GetValues(typeof(Materia));


        // =========================
        // CONSTRUTOR
        // =========================

        public ServicoEscolar()
        {
            PreCadastrarAlunosIniciais();
        }


        // =========================
        // ALUNOS INICIAIS
        // =========================

        private void PreCadastrarAlunosIniciais()
        {
            CadastrarAlunoPreDefinido(
                "Ana Silva",
                "00000000000",
                "AM1");

            CadastrarAlunoPreDefinido(
                "Bruno Souza",
                "00000000001",
                "AV2");

            CadastrarAlunoPreDefinido(
                "Carla Dias",
                "00000000002",
                "AN3");

            CadastrarAlunoPreDefinido(
                "Daniel Oliveira",
                "00000000003",
                "BN1");

            CadastrarAlunoPreDefinido(
                "Elena Costa",
                "00000000004",
                "CV2");
        }


        private void CadastrarAlunoPreDefinido(
            string nome,
            string cpf,
            string codigoTurma)
        {
            string matricula = GerarMatriculaAleatoria();

            Aluno aluno = new Aluno(
                nome,
                cpf,
                new DateTime(2008, 1, 1))
            {
                Matricula = matricula,
                CodigoTurma = codigoTurma
            };

            dicionarioAlunos.Add(
                matricula,
                aluno);
        }


        // =========================
        // ALUNOS
        // =========================

        public Dictionary<string, Aluno> ObterAlunos()
        {
            return dicionarioAlunos;
        }


        public Aluno BuscarAluno(string matricula)
        {
            dicionarioAlunos.TryGetValue(
                matricula,
                out Aluno aluno);

            return aluno;
        }


        public string CadastrarAluno(
            string nome,
            string cpf,
            DateTime dataNascimento)
        {
            if (dicionarioAlunos.Values.Any(
                a => a.CPF == cpf))
            {
                throw new ArgumentException(
                    "Já existe um aluno cadastrado com este CPF.");
            }


            string matricula =
                GerarMatriculaAleatoria();


            // =========================
            // GERAÇÃO AUTOMÁTICA DA TURMA
            // =========================

            string[] letras =
            {
                "A",
                "B",
                "C"
            };

            string[] turnos =
            {
                "M",
                "V",
                "N"
            };

            string ano =
                random.Next(1, 4).ToString();


            string codigoTurma =
                $"{letras[random.Next(letras.Length)]}" +
                $"{turnos[random.Next(turnos.Length)]}" +
                $"{ano}";


            Aluno novoAluno =
                new Aluno(
                    nome,
                    cpf,
                    dataNascimento)
                {
                    Matricula = matricula,
                    CodigoTurma = codigoTurma
                };


            dicionarioAlunos.Add(
                matricula,
                novoAluno);


            return matricula;
        }


        // =========================
        // REGISTRO DE NOTAS
        // =========================

        public void RegistrarNota(
            string matricula,
            Materia materia,
            double nota)
        {
            Aluno aluno =
                BuscarAluno(matricula);


            if (aluno == null)
            {
                throw new ArgumentException(
                    "Aluno não encontrado.");
            }


            if (nota < 0 || nota > 10)
            {
                throw new ArgumentException(
                    "A nota deve estar entre 0 e 10.");
            }


            aluno.NotasPorMateria[materia] =
                nota;

            aluno.MateriasComNota.Add(
                materia);
        }


        // =========================
        // TURMA
        // =========================

        public (
            Materia Materia,
            List<string> Colegas)
            ObterTurmaComMateria(
                string codigoTurma)
        {
            Materia materiaSorteada =
                materias[random.Next(
                    materias.Length)];


            List<string> colegas =
                new List<string>();


            foreach (
                Aluno aluno
                in dicionarioAlunos.Values)
            {
                if (
                    aluno.CodigoTurma.Equals(
                        codigoTurma,
                        StringComparison.OrdinalIgnoreCase))
                {
                    colegas.Add(
                        $"{aluno.Nome} " +
                        $"(Matrícula: {aluno.Matricula})");
                }
            }


            if (colegas.Count == 0)
            {
                colegas.Add(
                    "Nenhum aluno cadastrado nesta turma.");
            }


            return (
                materiaSorteada,
                colegas);
        }


        // =========================
        // PROFESSORES
        // =========================

        public Dictionary<string, Professor>
            ObterProfessores()
        {
            return dicionarioProfessores;
        }


        public Professor BuscarProfessor(
            string cpf)
        {
            dicionarioProfessores.TryGetValue(
                cpf,
                out Professor professor);

            return professor;
        }


        public Professor CadastrarProfessor(
            string nome,
            string cpf,
            DateTime dataNascimento)
        {
            if (
                dicionarioProfessores
                    .ContainsKey(cpf))
            {
                throw new ArgumentException(
                    "Já existe um professor cadastrado com este CPF.");
            }


            Professor professor =
                new Professor(
                    nome,
                    cpf,
                    dataNascimento);


            // =========================
            // TURMAS AUTOMÁTICAS
            // =========================

            List<string> turmasDisponiveis =
                dicionarioAlunos.Values
                    .Select(a => a.CodigoTurma)
                    .Distinct(
                        StringComparer.OrdinalIgnoreCase)
                    .ToList();


            int quantidadeTurmas =
                Math.Min(
                    random.Next(1, 4),
                    turmasDisponiveis.Count);


            for (
                int i = 0;
                i < quantidadeTurmas;
                i++)
            {
                int indice =
                    random.Next(
                        turmasDisponiveis.Count);


                professor.Turmas.Add(
                    turmasDisponiveis[indice]);


                turmasDisponiveis.RemoveAt(
                    indice);
            }


            dicionarioProfessores.Add(
                cpf,
                professor);


            return professor;
        }


        // =========================
        // MATRÍCULA
        // =========================

        private string GerarMatriculaAleatoria()
        {
            string matricula;


            do
            {
                matricula =
                    random
                        .Next(
                            100000,
                            1000000)
                        .ToString();

            } while (
                dicionarioAlunos
                    .ContainsKey(matricula));


            return matricula;
        }
    }
}