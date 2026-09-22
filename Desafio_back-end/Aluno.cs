using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_back_end
{
    public class Aluno : Pessoa
    {
        public required string Matricula { get; set; }
        public required string CodigoTurma { get; set; }
        public List<double> Notas { get; set; } = new List<double>();
        public Dictionary<string, double> NotasPorMateria { get; set; } = new Dictionary<string, double>();
    }
}
