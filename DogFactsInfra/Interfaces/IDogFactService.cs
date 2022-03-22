using OvermindDogFacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvermindDogFacts.Interfaces
{
    public interface IDogFactService
    {
        Task ExecuteAsync(ConfigurationFact request);
    }
}
