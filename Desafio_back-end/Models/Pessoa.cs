using System;

namespace Desafio_back_end.Models
{
    public class Pessoa
    {
        public string Nome
        {
            get;
            protected set;
        }

        public string CPF
        {
            get;
            protected set;
        }

        public DateTime DataDeNascimento
        {
            get;
            protected set;
        }


        public Pessoa(
            string nome,
            string cpf,
            DateTime dataNascimento)
        {
            Nome = nome;
            CPF = cpf;
            DataDeNascimento = dataNascimento;
        }


        protected virtual void RegistrarPessoa(
            string nome,
            string cpf,
            DateTime dataNascimento)
        {
            Nome = nome;
            CPF = cpf;
            DataDeNascimento = dataNascimento;
        }
    }
}