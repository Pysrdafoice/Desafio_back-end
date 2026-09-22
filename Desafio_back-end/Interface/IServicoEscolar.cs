using System;
using System.Collections.Generic;

namespace Desafio_back_end.Interface
{
    public interface IServicoEscolar
    {
        // --- Alunos ---
        Dictionary<string, Aluno> ObterAlunos();
        Aluno BuscarAluno(string matricula);
        string CadastrarAluno(string nome, string cpf, DateTime dataNascimento, string ano, string turno, string letraTurma);
        (string Materia, List<string> Colegas) ObterTurmaComMateria(string codigoTurma);

        // --- Professores ---
        //Dictionary<string, Professor> ObterProfessores();
        //Professor BuscarProfessor(string matricula);
        //string CadastrarProfessor(string nome, string cpf, DateTime dataNascimento, double salario, List<string> turmas);
    }
}
