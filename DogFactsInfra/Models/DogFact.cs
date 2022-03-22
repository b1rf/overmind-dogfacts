using OvermindDogFacts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvermindDogFacts.Model
{
    internal class DogFact
    {
        public int dog { get; set; }

        public ICollection<Fact> facts { get; set; }
    }
}
