using System;
using System.Collections.Generic;
using Desafio_back_end.Models;

namespace Desafio_back_end.Services
{
    public interface IServicoEscolar
    {
        // =========================
        // ALUNOS
        // =========================

        Dictionary<string, Aluno> ObterAlunos();

        Aluno BuscarAluno(string matricula);

        string CadastrarAluno(
            string nome,
            string cpf,
            DateTime dataNascimento);

        void RegistrarNota(
            string matricula,
            Materia materia,
            double nota);

        (Materia Materia, List<string> Colegas)
            ObterTurmaComMateria(string codigoTurma);


        // =========================
        // PROFESSORES
        // =========================

        Dictionary<string, Professor> ObterProfessores();

        Professor BuscarProfessor(string cpf);

        Professor CadastrarProfessor(
            string nome,
            string cpf,
            DateTime dataNascimento);
    }
}
