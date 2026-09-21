using System;
using System.Collections.Generic;

namespace Desafio_back_end
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataDeNascimento { get; set; }
    }

    public class Aluno : Pessoa
    {
        public string Matricula { get; set; }
        public List<double> Notas { get; set; } = new List<double>();
        public Dictionary<string, double> NotasPorMateria { get; set; } = new Dictionary<string, double>();
    }

    public class Professor : Pessoa
    {
        public double Salario { get; set; }
        public List<string> Turmas { get; set; } = new List<string>();
    }

}