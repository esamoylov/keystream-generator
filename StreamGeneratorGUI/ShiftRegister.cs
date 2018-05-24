using System;

namespace StreamGeneratorGUI
{
    struct ShiftRegister
    {
        private int[] powX;     // Массив числовых значений степеней x
        private int maxPow;     // Наибольшая степень x
        public byte[] u;

        public ShiftRegister(int[] powers, int max)
        {
            maxPow = max;
            u = new byte[maxPow];
            powX = powers;
        }

        /// <summary>
        /// Случайным образом задает первые n бит
        /// (n равно наибольшей степени x)
        /// </summary>
        public void GenerateFirstBits()
        {
            Random r = new Random();
            int sum;

            do
            {
                sum = 0;

                for (int i = 0; i < powX[0]; i++)
                {
                    u[i] = (byte)r.Next(0, 2);
                    sum += u[i];
                }

            } while (sum == 0);

            if (powX[0] < maxPow) GetLastBits();
        }

        public void SetFirstBits(string bits)
        {
            for (int i = 0; i < powX[0]; i++)
            {
                u[i] = byte.Parse(bits[i].ToString());
            }

            if (powX[0] < maxPow) GetLastBits();
        }

        private void GetLastBits()
        {
            for (int i = powX[0]; i < maxPow; i++)
                ComputeU(i);
        }

        public void ComputeU(int n)
        {
            int temp = 0;

            for (int i = 1; i < powX.Length; i++)
                temp += u[(n - (powX[0] - powX[i])) % maxPow];

            u[n % maxPow] = (byte)(temp % 2);
        }
    }
}