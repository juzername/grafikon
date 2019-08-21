using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signals
{
    public class SignalValue
    {
        private readonly double value;
        private readonly DateTime timeStamp;

        public SignalValue(double val, DateTime time)
        {
            value = val;
            timeStamp = time;
        }

        public double Value
        {
            get { return value; }
        }

        public DateTime TimeStamp
        {
            get { return timeStamp; }
        }

        public override string ToString()
        {
            return string.Format("Value: {0}, TimeStamp: {1}", Value, TimeStamp);
        }
    }
}
