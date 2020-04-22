using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class Endereco : EntidadeBase
    {
        public string NomeLogradouro { get; set; }

        public string Numero { get; set; }

        public string Cep { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string TipoLogradouro { get; set; }

        public int EstacionamentoId { get; set; }

        public Estacionamento Estacionamento { get; set; }

    }
}
