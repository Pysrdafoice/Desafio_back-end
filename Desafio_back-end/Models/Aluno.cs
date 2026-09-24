using System;
using System.Collections.Generic;

namespace Desafio_back_end.Models
{
    public class Aluno : Pessoa
    {
        public string Matricula
        {
            get;
            set;
        }

        public string CodigoTurma
        {
            get;
            set;
        }


        public Dictionary<Materia, double>
            NotasPorMateria
        {
            get;
            set;
        }
            = new Dictionary<Materia, double>();


        public HashSet<Materia>
            MateriasComNota
        {
            get;
            set;
        }
            = new HashSet<Materia>();


        public Aluno(
            string nome,
            string cpf,
            DateTime dataNascimento)
            : base(
                nome,
                cpf,
                dataNascimento)
        {
            RegistrarPessoa(
                nome,
                cpf,
                dataNascimento);
        }


        protected override void RegistrarPessoa(
            string nome,
            string cpf,
            DateTime dataNascimento)
        {
            base.RegistrarPessoa(
                nome,
                cpf,
                dataNascimento);
        }
    }
}