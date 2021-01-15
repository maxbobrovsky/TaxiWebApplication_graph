using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiWebApplication.Services
{
    public class KnnService
    {

        public int Parse(string Address)
        {
            double[][] auxData = LoadData();

            int numCoord = 2;
            int numReg = 4;

            //string address = "50.51875 30.23978300000000003";

            string[] mas = Address.Split(' ');
            mas[0] = mas[0].Replace('.', ',');
            mas[1] = mas[1].Replace('.', ',');
            double[] unknown = new double[2];
            for (int i = 0; i < mas.Length; i++)
            {
                unknown[i] = double.Parse(mas[i]);
            }

            return Classify(unknown, auxData, 4, 1);
        }
        private static int Classify(double[] unknown, double[][] auxData, int numReg, int k)
        {
            int n = auxData.Length;
            IndexAndDistance[] info = new IndexAndDistance[n];

            for (int i = 0; i < n; i++)
            {
                IndexAndDistance curr = new IndexAndDistance();
                double dist = Distance(unknown, auxData[i]);

                curr.idx = i;
                curr.dist = dist;

                info[i] = curr;
            }

            Array.Sort(info);

            Console.WriteLine("Nearest / Distance / Class");
            Console.WriteLine("==========================");
            for (int i = 0; i < k; ++i)
            {
                int c = (int)auxData[info[i].idx][2];
                string dist = info[i].dist.ToString("F4");
                Console.WriteLine("( " + auxData[info[i].idx][0] +
                  "," + auxData[info[i].idx][1] + " )  :  " +
                  dist + "        " + c);
            }

            int result = Vote(info, auxData, numReg, k);

            return result;
        }

        private static int Vote(IndexAndDistance[] info, double[][] auxData, int numReg, int k)
        {
            int[] votes = new int[numReg];

            for (int i = 0; i < k; i++)
            {
                int idx = info[i].idx;

                int reg = (int)auxData[idx][2];

                ++votes[reg];
            }

            int mostVotes = 0;

            int mostVotesClass = 0;

            for (int j = 0; j < numReg; j++)
            {
                if (votes[j] > mostVotes)
                {
                    mostVotes = votes[j];
                    mostVotesClass = j;
                }
            }
            return mostVotesClass;
        }

        public static double Distance(double[] unknown, double[] data)
        {
            double sum = 0.0;

            for (int i = 0; i < unknown.Length; i++)
            {
                sum += (unknown[i] - data[i]) * (unknown[i] - data[i]);
            }

            return Math.Sqrt(sum);
        }

        private static double[][] LoadData()
        {

            // coordinates of regions divided on 10()
            double[][] data = new double[48][];

            // Kyiv region
            data[0] = new double[] { 4.98, 3.01, 0 };
            data[1] = new double[] { 5.045, 3.052, 0 };
            data[2] = new double[] { 5.05, 2.99, 0 };
            data[3] = new double[] { 5.055, 3.021, 0 };
            data[4] = new double[] { 4.954, 3.088, 0 };
            data[5] = new double[] { 5.056, 2.953, 0 };
            data[6] = new double[] { 5.03, 3.047, 0 };
            data[7] = new double[] { 5.021, 3.057, 0 };
            data[8] = new double[] { 5.0645, 2.99, 0 };
            data[9] = new double[] { 4.9366, 2.966, 0 };
            data[10] = new double[] { 5.007, 3.145, 0 };
            data[11] = new double[] { 5.0257, 3.178, 0 };

            //Zhytomir region
            data[12] = new double[] { 5.015, 2.84, 1 };
            data[13] = new double[] { 5.0586, 2.7636, 1 };
            data[14] = new double[] { 5.0953, 2.8646, 1 };
            data[15] = new double[] { 5.077, 2.926, 1 };
            data[16] = new double[] { 5.122, 2.7644, 1 };
            data[17] = new double[] { 4.991, 2.859, 1 };
            data[18] = new double[] { 5.1327, 2.88, 1 };
            data[19] = new double[] { 4.992, 2.777, 1 };
            data[20] = new double[] { 4.972, 2.921, 1 };
            data[21] = new double[] { 5.003, 2.902, 1 };
            data[22] = new double[] { 5.047, 2.826, 1 };
            data[23] = new double[] { 5.029, 2.767, 1 };

            // Cherkasy region
            data[24] = new double[] { 4.9444, 3.206, 2 };
            data[25] = new double[] { 4.9227, 3.185, 2 };
            data[26] = new double[] { 4.875, 3.022, 2 };
            data[27] = new double[] { 4.975, 3.147, 2 };
            data[28] = new double[] { 4.9246, 3.010, 2 };
            data[29] = new double[] { 4.9073, 3.266, 2 };
            data[30] = new double[] { 4.9426, 3.1262, 2 };
            data[31] = new double[] { 4.8996, 2.98, 2 };
            data[32] = new double[] { 4.9076, 3.097, 2 };
            data[33] = new double[] { 4.8997, 3.14, 2 };
            data[34] = new double[] { 4.929, 3.1445, 2 };
            data[35] = new double[] { 4.995, 3.214, 2 };

            //Vinitsya region
            data[36] = new double[] { 4.9233, 2.8468, 3 };
            data[37] = new double[] { 4.9715, 2.8836, 3 };
            data[38] = new double[] { 4.8975, 2.885, 3 };
            data[39] = new double[] { 4.908, 2.768, 3 };
            data[40] = new double[] { 4.846, 2.78, 3 };
            data[41] = new double[] { 4.824, 2.83, 3 };
            data[42] = new double[] { 4.904, 2.811, 3 };
            data[43] = new double[] { 4.866, 2.975, 3 };
            data[44] = new double[] { 4.821, 2.934, 3 };
            data[45] = new double[] { 4.919, 2.953, 3 };
            data[46] = new double[] { 4.876, 2.807, 3 };
            data[47] = new double[] { 4.9265, 2.831, 2 };

            return data;
        }

        public double DistanceToDest(double[] unknown, double[] data)
        {
            for (int i = 0; i <= 1; i++)
            {
                unknown[i] = unknown[i] * Math.PI / 180;
                data[i] = data[i] * Math.PI / 180;
            }
            const double R = 6371;

            double sin1 = Math.Sin((unknown[0] - data[0]) / 2);
            double sin2 = Math.Sin((unknown[1] - data[1]) / 2);


            return 2 * R * Math.Asin(Math.Sqrt(sin1 * sin1 + Math.Cos(unknown[0]) * Math.Cos(data[0]) * sin2 * sin2));
        }
    }

    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        public int idx;
        public double dist;

        public int CompareTo(IndexAndDistance other)
        {
            if (this.dist < other.dist) return -1;
            else if (this.dist > other.dist) return +1;
            else return 0;
        }
    }


}

