namespace HyperThreading
{
    static class Program
    {
        static int[] array = new int[10];
        static int sumResult = 0;
        static int productResult = 1;
        static readonly object lockObject = new();

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            InitArray();

            Console.WriteLine("Згенеровано масив: ");
            ShowArray(array);

            Thread sumThread = new(SumElements);
            Thread productThread = new(ProductElements);

            sumThread.Start();
            productThread.Start();

            sumThread.Join();
            productThread.Join();

            Console.WriteLine($"Сума елементів: {sumResult}");
            Console.WriteLine($"Добуток елементів: {productResult}");
        }

        static void InitArray()
        {
            Random random = new();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0, 26);
            }
        }

        static void ShowArray(int[] array)
        {
            foreach (int elem in array)
            {
                if (elem == array[^1])
                {
                    Console.Write($"{elem}\n");
                }
                else
                {
                    Console.Write($"{elem}, ");
                }
            }
        }

        static void SumElements()
        {
            lock (lockObject)
            {
                foreach (int element in array)
                {
                    sumResult += element;
                }
            }
        }

        static void ProductElements()
        {
            lock (lockObject)
            {
                foreach (int element in array)
                {
                    productResult *= element;
                }
            }
        }
    }
}

