using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvermindDogFacts.Models
{
    public class ConfigurationFact
    {
        public string EndPoint { get; set; } = "https://dog-facts-api.herokuapp.com/api/v1/resources/dogs";

        public string Input { get; set; }

        public string Output { get; set; }

        public string Processed { get; set; }

        public string validade()
        {
            if (!Uri.IsWellFormedUriString(EndPoint, UriKind.Absolute))
                return "Url de endpoint inválida!";

            if (!Directory.Exists(Input))
                return "Diretório de entrada não existe!";

            if (!Directory.Exists(Output))
                return "Diretório de saída não existe!";

            if (!Directory.Exists(Processed))
                return "Diretório de processados não existe!";

            return string.Empty;
        }
    }
}
