using System;
using System.ComponentModel.DataAnnotations;

namespace Questao1.models
{
    internal class ContaBancaria {
        internal int Conta { get; set; }
        internal string Titular { get; set; }
        internal double? Saldo { get; set; } = 0;
        internal double Taxa { get; set; } = 3.50;

        internal ContaBancaria(int numero, string nomeTitular, double? depositoInicial = 0.00)
        {
            Conta = numero;
            Titular = nomeTitular;
            Saldo = depositoInicial;
        }

        internal double? Deposito(double quantia){
            return Saldo = Saldo + quantia;
        }

        internal double? Saque(double quantia)
        {
            if (quantia < Saldo)
            {
                return Saldo = (Saldo - quantia) - Taxa;
            }
            else
            {
                return HandleError();
            }
        }

        private double HandleError()
        {
            Console.WriteLine($"Saldo insuficiente.");
            return -1;
        }
    }
}
