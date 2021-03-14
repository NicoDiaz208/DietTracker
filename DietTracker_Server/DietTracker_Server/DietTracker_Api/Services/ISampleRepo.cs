using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietTracker_Api.Services
{
    interface ISampleRepo
    {
        public Sample Add(Sample sample);
        public IEnumerable<Sample> GetAll();
    }
}
