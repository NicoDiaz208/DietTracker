using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietTracker_Api.Services
{

    public record Sample(string name);

    public class SampleRepo : ISampleRepo
    {
        private List<Sample> samples { get; } = new();
        public Sample Add(Sample sample)
        {
            samples.Add(sample);
            return sample;
        }

        public IEnumerable<Sample> GetAll() => samples;

    }
}
