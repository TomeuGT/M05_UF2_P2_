using System;
using System.Diagnostics;

namespace AlgoritmosOrdenado
{
    public class ArraySort
    {
        public int[] array;
        public int[] arrayIncreasing;
        public int[] arrayDecreasing;

        public ArraySort(int elements, Random random)
        {
            array = new int[elements];
            arrayIncreasing = new int[elements];
            arrayDecreasing = new int[elements];
            for (int i = 0; i < elements; i++)
            {
                array[i] = random.Next();
            }
            Array.Copy(array, arrayIncreasing, elements);
            Array.Sort(arrayIncreasing);

            Array.Copy(arrayIncreasing, arrayDecreasing, elements);
            Array.Reverse(arrayDecreasing);
        }

        public void Benchmark(Action<int[]> function)
        {
            int[] temp = new int[array.Length];
            Array.Copy(array, temp, array.Length);
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine(function.Method.Name);

            stopwatch.Start();
            function(temp);
            stopwatch.Stop();
            Console.WriteLine("Random: " + stopwatch.ElapsedMilliseconds + "ms " + stopwatch.ElapsedTicks + "ticks");
            stopwatch.Reset();

            stopwatch.Start();
            function(temp);
            stopwatch.Stop();
            Console.WriteLine("Increasing: " + stopwatch.ElapsedMilliseconds + "ms " + stopwatch.ElapsedTicks + "ticks");
            stopwatch.Reset();

            Array.Reverse(temp);
            stopwatch.Start();
            function(temp);
            stopwatch.Stop();
            Console.WriteLine("Decreasing: " + stopwatch.ElapsedMilliseconds + "ms " + stopwatch.ElapsedTicks + "ticks");
        }

        public void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }
        public void BubbleSortEarlyExit(int[] arr)
        {
            bool isOrdered = true;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                isOrdered = true;
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        isOrdered = false;
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
                if (isOrdered)
                    return;
            }
        }
        public void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }
        public void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = QuickSortIndex(arr, left, right);
                QuickSort(arr, left, pivot);
                QuickSort(arr, pivot + 1, right);
            }
        }
        public int QuickSortIndex(int[] arr, int left, int right)
        {
            int pivot = arr[(left + right) / 2];

            while (true)
            {
                while (arr[left] < pivot)
                {
                    left++;
                }
                while (arr[right] > pivot)
                {
                    right--;
                }
                if (right <= left)
                {
                    return right;
                }
                else
                {
                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                    right--; left++;
                }
            }
        }
        public void InsertionSort(int[] arr)
        {
            {
                int n = arr.Length;
                for (int i = 1; i < n; ++i)
                {
                    int key = arr[i];
                    int j = i - 1;

                    while (j >= 0 && arr[j] > key)
                    {
                        arr[j + 1] = arr[j];
                        j = j - 1;
                    }
                    arr[j + 1] = key;
                }
            }
        }

        public void heapify(int[] array, int n, int i)
        {
            int largest = i;

            int l = 2 * i + 1;

            int r = 2 * i + 2;

            n = array.Length;


            if (l < n && array[l] > array[largest])
                largest = l;


            if (r < n && array[r] > array[largest])
                largest = r;

            if (largest != i)
            {
                Swap(array, array[i], array[largest]);

                heapify(array, n, largest);
            }
        }

        public void Swap(int[] array, int position1, int position2)
        {
            int temp = array[position1];

            array[position1] = array[position2];

            array[position2] = temp;
        }
        public void HeapSort(int[] arr)
        {
            for (int i = array.Length / 2 - 1; i >= 0; i--)



                heapify(array, array.Length, i);




            for (int i = array.Length - 1; i > 0; i--)
            {
                Swap(array, array[0], array[i]);

                heapify(array, i, 0);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many numbers do you want?");
            int elements = int.Parse(Console.ReadLine());
            Console.WriteLine("What seed do you want to use?");
            int seed = int.Parse(Console.ReadLine());
            Random random = new Random(seed);
            ArraySort arraySort = new ArraySort(elements, random);

            //arraySort.Benchmark(arraySort.BubbleSort);
            arraySort.Benchmark(arraySort.BubbleSortEarlyExit);
            arraySort.Benchmark(arraySort.QuickSort);
            arraySort.Benchmark(arraySort.InsertionSort);
            arraySort.Benchmark(arraySort.HeapSort);

        }
    }
}
