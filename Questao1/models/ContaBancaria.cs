using System;
using System.ComponentModel.DataAnnotations;

namespace Questao1.models
{
    internal class ContaBancaria {
        internal int conta { get; set; }
        internal string titular { get; set; }
        internal double? saldo { get; set; } = 0;
        [DataType(DataType.Currency)]
        internal double taxa { get; set; } = 3.50;

        internal ContaBancaria(int numero, string nomeTitular, double? depositoInicial = 0.00)
        {
            conta = numero;
            titular = nomeTitular;
            saldo = depositoInicial;
        }

        internal double? Deposito(double quantia){
            return saldo = saldo + quantia;
        }

        internal double? Saque(double quantia)
        {
            if (quantia < saldo)
            {
                return saldo = (saldo - quantia) - taxa;
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
