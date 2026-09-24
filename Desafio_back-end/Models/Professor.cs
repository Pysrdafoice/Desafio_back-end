using System;
using System.Collections.Generic;

namespace Desafio_back_end.Models
{
    public class Professor : Pessoa
    {
        public const double SalarioBase =
            3500.00;


        public List<string> Turmas
        {
            get;
            set;
        }
            = new List<string>();


        public Professor(
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