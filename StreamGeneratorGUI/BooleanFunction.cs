using System;
using System.IO;

namespace StreamGeneratorGUI
{
    struct BooleanFunction
    {
        ShiftRegister[] sr;
        int maxPow;
        byte bfLastCoeff;
        object[] boolFuncArray;
        byte[] bfCoefficients;

        public BooleanFunction(string[] srEquations, string function)
        {
            // Обработка булевой функции с " + 1" в конце
            if (function[function.Length - 1] == '1' && !Char.IsLetter(function[function.Length - 2]))
            {
                bfLastCoeff = 1;
                function = function.Remove(function.Length - 2);
                if (function[function.Length - 1] == '+')
                    function = function.Remove(function.Length - 1);
            }
            else
                bfLastCoeff = 0;

            // Окружение знака '+' пробелами, чтобы избежать ошибки, если пользователь ввел
            // булеву функцию без пробелов
            function = function.Replace("+", " + ");

            sr = new ShiftRegister[srEquations.Length];

            // Наибольшая степень x среди всех уравнений
            int max = 0;
            foreach (var s in srEquations)
            {
                int first = int.Parse(s[0].ToString());
                if (max < first) max = first;
            }

            maxPow = max;

            for (int i = 0; i < srEquations.Length; i++)
            {
                // Перевод строки в массив символов, а затем в массив чисел
                string[] splitted = srEquations[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int[] powX = new int[splitted.Length];

                for (int j = 0; j < splitted.Length; j++)
                    powX[j] = int.Parse(splitted[j]);

                sr[i] = new ShiftRegister(powX, max);
            }

            // Создание массива байт размера "количество плюсов в функции + 1"
            // Для расчета функции из строки
            int countPlus = 0;
            foreach (var c in function)
                if (c == '+') countPlus++;

            bfCoefficients = new byte[countPlus + 1];

            // Разделить строку на массив символов, затем преобразовать символы, являющиеся числами в int
            string[] array = function.ToLower().Split(new char[] { ' ', 'x', 'u' }, StringSplitOptions.RemoveEmptyEntries);
            boolFuncArray = new object[array.Length];
            boolFuncArray = StringToObjectArray(array);
        }

        /// <summary>
        /// Преобразует булеву функцию из строки в массив обьектов, содержащий номера переменных
        /// (тип int) и символ '+' (тип char)
        /// </summary>
        /// <param name="array">Булева функция в виде массива строк</param>
        /// <returns></returns>
        private object[] StringToObjectArray(string[] array)
        {
            object[] funcArray = new object[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                if (String.Equals(array[i], "+"))
                {
                    funcArray[i] = Convert.ToChar(array[i]);
                    continue;
                }

                funcArray[i] = int.Parse(array[i]) - 1;
            }

            return funcArray;
        }

        public void GenBits()
        {
            foreach (var r in sr)
                r.GenerateFirstBits();
        }

        public void SetBits(string[] bits)
        {
            for (int i = 0; i < sr.Length; i++)
            {
                sr[i].SetFirstBits(bits[i]);
            }
        }

        public void FilferGenerator(long n, string path)
        {
            StreamWriter sw = new StreamWriter(path);

            for (int i = 0; i < n; i++)
            {
                if (i > 0)
                    sr[0].ComputeU(i + maxPow - 1);

                int position = 0;

                for (int k = 0; k < bfCoefficients.Length; k++)
                    bfCoefficients[k] = 1;

                for (int j = 0; j < boolFuncArray.Length; j++)
                {
                    if (!boolFuncArray[j].Equals('+'))
                        bfCoefficients[position] *= sr[0].u[(i + (int)boolFuncArray[j]) % maxPow];
                    else
                        position++;
                }

                byte f = bfLastCoeff;
                foreach (var c in bfCoefficients)
                    f += c;

                sw.Write((byte)(f % 2));                
            }

            sw.Close();
        }

        public void CombiningGenerator(long n, string path)
        {
            StreamWriter sw = new StreamWriter(path);

            for (int i = 0; i < n; i++)
            {
                if (i >= maxPow)
                {
                    foreach (var r in sr)
                        r.ComputeU(i);
                }

                int position = 0;

                for (int k = 0; k < bfCoefficients.Length; k++)
                    bfCoefficients[k] = 1;

                for (int j = 0; j < boolFuncArray.Length; j++)
                {
                    if (!boolFuncArray[j].Equals('+'))
                        bfCoefficients[position] *= sr[(int)boolFuncArray[j]].u[i % maxPow];
                    else
                        position++;
                }

                byte f = bfLastCoeff;
                foreach (var c in bfCoefficients)
                    f += c;

                sw.Write((byte)(f % 2));
            }

            sw.Close();
        }

        /// <summary>
        /// Метод не вызывается основным кодом
        /// Находится здесь для тестирования работы генератора
        /// </summary>
        /// <param name="n">Длина последовательности</param>
        public void Generate(int n)
        {
            StreamWriter sw = new StreamWriter(@"C:\Test1.txt");

            // Фильтрующий генератор (один регистр сдвига)
            if(sr.Length == 1)
            {
                for (int i = 0; i < n; i++)
                {
                    if (i > 0)
                        sr[0].ComputeU(i+maxPow-1);

                    byte f = (byte)((sr[0].u[i%maxPow]*sr[0].u[(i+1)%maxPow] + 
                        sr[0].u[i%maxPow]*sr[0].u[(i+2)%maxPow] + sr[0].u[(i+2)%maxPow]) % 2);

                    sw.Write(f);
                    Console.Write(f);
                }
                Console.WriteLine();
            }

            // Комбинирующий генератор (несколько регистров сдвига)
            else
            {
                for (int i = 0; i < n; i++)
                {
                    if (i >= maxPow)
                    {
                        foreach (var r in sr)
                            r.ComputeU(i);
                    }

                    byte f = (byte)((sr[0].u[i % maxPow] * sr[1].u[i % maxPow] +
                        sr[0].u[i % maxPow] * sr[2].u[i % maxPow] + sr[2].u[i % maxPow]) % 2);

                    sw.Write(f);
                    Console.Write(f);
                }
            }           

            sw.Close();
        }
    }
}