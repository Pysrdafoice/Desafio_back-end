using System;
using System.Collections.Generic;
using System.Linq;
using Desafio_back_end.Models;

namespace Desafio_back_end.Services
{
    public class ServicoEscolar : IServicoEscolar
    {
        private readonly Dictionary<string, Aluno> alunos =
            new Dictionary<string, Aluno>();

        private readonly Dictionary<string, Professor> professores =
            new Dictionary<string, Professor>();

        private readonly Random random =
            new Random();


        public ServicoEscolar()
        {
            PreCadastrarAlunos();
        }


        // ============================================================
        // ALUNOS
        // ============================================================

        public Dictionary<string, Aluno> ObterAlunos()
        {
            return alunos;
        }


        public Aluno BuscarAluno(
            string matricula)
        {
            if (alunos.ContainsKey(matricula))
            {
                return alunos[matricula];
            }

            return null;
        }


        public string CadastrarAluno(
            string nome,
            string cpf,
            DateTime dataNascimento)
        {
            if (alunos.Values.Any(
                aluno => aluno.CPF == cpf))
            {
                throw new ArgumentException(
                    "Já existe um aluno " +
                    "cadastrado com este CPF.");
            }


            string matricula =
                GerarMatricula();

            string codigoTurma =
                GerarTurma();


            Aluno aluno =
                new Aluno(
                    nome,
                    cpf,
                    dataNascimento);


            aluno.Matricula =
                matricula;

            aluno.CodigoTurma =
                codigoTurma;


            alunos.Add(
                matricula,
                aluno);


            return matricula;
        }


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


        public (
            Materia Materia,
            List<string> Colegas
        )
        ObterTurmaComMateria(
            string codigoTurma)
        {
            Materia materiaSorteada =
                SortearMateria();


            List<string> colegas =
                alunos.Values
                    .Where(aluno =>
                        aluno.CodigoTurma
                            == codigoTurma)
                    .Select(aluno =>
                        aluno.Nome)
                    .ToList();


            return (
                materiaSorteada,
                colegas
            );
        }


        // ============================================================
        // PROFESSORES
        // ============================================================

        public Dictionary<string, Professor>
            ObterProfessores()
        {
            return professores;
        }


        public Professor BuscarProfessor(
            string cpf)
        {
            if (professores.ContainsKey(cpf))
            {
                return professores[cpf];
            }

            return null;
        }


        public Professor CadastrarProfessor(
            string nome,
            string cpf,
            DateTime dataNascimento)
        {
            if (professores.Values.Any(
                professor => professor.CPF == cpf))
            {
                throw new ArgumentException(
                    "Já existe um professor " +
                    "cadastrado com este CPF.");
            }


            Professor professor =
                new Professor(
                    nome,
                    cpf,
                    dataNascimento);


            professor.Turmas =
                GerarTurmasProfessor();


            professores.Add(
                cpf,
                professor);


            return professor;
        }


        // ============================================================
        // PRÉ-CADASTRO
        // ============================================================

        private void PreCadastrarAlunos()
        {
            AdicionarAlunoInicial(
                "Ana Silva",
                "11111111111",
                new DateTime(2005, 5, 10),
                "AM1");

            AdicionarAlunoInicial(
                "Bruno Souza",
                "22222222222",
                new DateTime(2006, 3, 15),
                "AV2");

            AdicionarAlunoInicial(
                "Carla Dias",
                "33333333333",
                new DateTime(2005, 8, 20),
                "AN3");

            AdicionarAlunoInicial(
                "Daniel Oliveira",
                "44444444444",
                new DateTime(2006, 1, 12),
                "BN1");

            AdicionarAlunoInicial(
                "Elena Costa",
                "55555555555",
                new DateTime(2005, 11, 25),
                "CV2");
        }


        private void AdicionarAlunoInicial(
            string nome,
            string cpf,
            DateTime dataNascimento,
            string codigoTurma)
        {
            string matricula =
                GerarMatricula();


            Aluno aluno =
                new Aluno(
                    nome,
                    cpf,
                    dataNascimento);


            aluno.Matricula =
                matricula;

            aluno.CodigoTurma =
                codigoTurma;


            alunos.Add(
                matricula,
                aluno);
        }


        // ============================================================
        // MATRÍCULA
        // ============================================================

        private string GerarMatricula()
        {
            string matricula;

            do
            {
                matricula =
                    random
                        .Next(100000, 1000000)
                        .ToString();

            } while (
                alunos.ContainsKey(matricula));


            return matricula;
        }


        // ============================================================
        // TURMA
        // ============================================================

        private string GerarTurma()
        {
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


            int ano =
                random.Next(1, 4);


            string letra =
                letras[
                    random.Next(
                        letras.Length)];


            string turno =
                turnos[
                    random.Next(
                        turnos.Length)];


            return $"{letra}{turno}{ano}";
        }


        // ============================================================
        // TURMAS DO PROFESSOR
        // ============================================================

        private List<string>
            GerarTurmasProfessor()
        {
            List<string> turmasDisponiveis =
                alunos.Values
                    .Select(aluno =>
                        aluno.CodigoTurma)
                    .Distinct()
                    .ToList();


            List<string> turmasProfessor =
                new List<string>();


            if (turmasDisponiveis.Count == 0)
            {
                return turmasProfessor;
            }


            int quantidade =
                random.Next(
                    1,
                    Math.Min(
                        3,
                        turmasDisponiveis.Count)
                    + 1);


            while (
                turmasProfessor.Count
                < quantidade)
            {
                string turma =
                    turmasDisponiveis[
                        random.Next(
                            turmasDisponiveis.Count)];


                if (!turmasProfessor.Contains(
                    turma))
                {
                    turmasProfessor.Add(
                        turma);
                }
            }


            return turmasProfessor;
        }


        // ============================================================
        // MATÉRIA
        // ============================================================

        private Materia SortearMateria()
        {
            Materia[] materias =
                (Materia[])Enum.GetValues(
                    typeof(Materia));


            int indice =
                random.Next(
                    materias.Length);


            return materias[indice];
        }
    }
}