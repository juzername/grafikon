using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signals
{
    public class SignalDocument : Document
    {
        private List<SignalValue> signals = new List<SignalValue>();

        public IReadOnlyList<SignalValue> Signals {
            get { return signals; }
        }

        private SignalValue[] testValues = new SignalValue[]
        {
            new SignalValue(33, new DateTime(2019, 05, 11, 10, 21, 5, 99)),
            new SignalValue(-20, new DateTime(2019, 05, 11, 10, 21, 6, 489)),
            new SignalValue(10, new DateTime(2019, 05, 11, 10, 21, 7, 157)),
            new SignalValue(40, new DateTime(2019, 05, 11, 10, 21, 8, 19)),
            new SignalValue(-55, new DateTime(2019, 05, 11, 10, 21, 9, 77)),
            new SignalValue(12, new DateTime(2019, 05, 11, 10, 21, 10, 155)),
        };

        public SignalDocument(string name) : base(name)
        {
            signals.AddRange(testValues);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void LoadDocument(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    signals.Clear();

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Trim();
                        string[] columns = line.Split('\t');
                        double val = Double.Parse(columns[0]);
                        DateTime utcDt = DateTime.Parse(columns[1]);
                        DateTime localDt = utcDt.ToLocalTime();

                        SignalValue sv = new SignalValue(val, localDt);
                        signals.Add(sv);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            UpdateAllViews();
            TraceValues();
        }

        public override void SaveDocument(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (SignalValue sv in signals)
                {
                    string dt = sv.TimeStamp.ToUniversalTime().ToString("o");
                    sw.WriteLine($"{sv.Value}\t{dt}");
                }
            }

        }

        public override string ToString()
        {
            return base.ToString();
        }

        void TraceValues()
        {
            foreach (SignalValue signal in signals)
                Trace.WriteLine(signal.Value.ToString());
        }

    }
}
