using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public class AtualizarEstacionamento
    {
        public int Id { get; set; }
        public string NomeEstacionamento { get; set; }

        public string Descricao { get; set; }

        public double PrecoHora { get; set; }

        public double Avaliacao { get; set; }

        public int NumeroVagas { get; set; }

        public int NumeroVagasDisponiveis { get; set; }

        public string NomeLogradouro { get; set; }

        public string Numero { get; set; }

        public string Cep { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string TipoLogradouro { get; set; }

    }
}
