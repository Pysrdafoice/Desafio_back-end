using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_back_end
{
    public class Professor : Pessoa
    {
        public double Salario { get; set; }
        public List<string> Turmas { get; set; } = new List<string>();
    }
}
