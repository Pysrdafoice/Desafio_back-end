using System.Runtime.CompilerServices;

namespace Desafio_back_end
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem-Vindo ao sistema Escolar, o que deseja ?");

            Console.WriteLine("1 = Você é Professor ou Aluno ?? ");
            //Se for professor , ele vai cadastrar a disciplina e o registro do Aluno,
            //se for aluno, ele vai cadastrar a matricula e ver nota do aluno.

            Console.WriteLine("1 = Registrar Aluno ");
            Console.WriteLine("1 = ver Boletim do Aluno ");
            Console.WriteLine("1 = Registra Nota ");
            Console.WriteLine("1 = Registrar Aluno ");



        }
    }

        class Pessoa
    {
        public string Nome { get; set; }
        public int CPF { get; set; }
        public int DatadeNascimento { get; set; }
    }
    class Aluno
    {
        public Pessoa Pessoa { get; set; }
        public int Matricula { get; set; }
        public string Curso { get; set; }
        public double Notas { get; set; }
    }

    class Estudante : Aluno
    {
        public string Turma { get; set; }
        string Situacao { get; set; }
        string disciplina { get; set; }

    }
    class Professor
    {
        public Pessoa Pessoa { get; set; }
        public int Registro { get; set; }
        public string Disciplina { get; set; }
    }
}
