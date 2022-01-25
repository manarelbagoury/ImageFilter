using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    class Sorting
    {
        public static byte[] CountingSort(byte[] arr, int n)
        {
            byte[] count = new byte[256];

            for (int i = 0; i < 256; i++)
                count[i] = 0;

            for (int i = 0; i < n; i++)
                count[arr[i]]++;

            int k = 0;

            for (int i = 0; i < n; i++)
            {
                while (count[k] == 0)
                {
                    k++;
                }
                arr[i] = (Byte)k;
                count[k]--;
            }
            return arr;
        }

        public static void Swap(byte[] arr, int i, int j)
        {
            byte temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        public static int Partition(byte[] arr, int left, int right) // Omega (n log n) and O (n^2)
        {
            int pivot = arr[right]; //assume pivot = rightmost
            int i = left - 1; // i = -1

            for (int j = left; j < right; j++) //j=0
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    if (i != j)
                    {
                        Swap(arr, i, j);
                    }

                }
            }

            Swap(arr, i + 1, right); //i represent the last index that owns a value less than pivot
            return i + 1;           // thus swap i+1 with pivot; therefor less<pivot<more

        }
        public static byte[] quicksort(byte[] arr, int left, int right) //Omega (n log n) and O (n^2)
        {
            if (left < right)
            {
                int LastPivot = Partition(arr, left, right);

                quicksort(arr, left, LastPivot - 1);

                quicksort(arr, LastPivot + 1, right);
                
            }
            return arr;
        }

        public static byte KthElement(byte[] arr, int n, int k) //O (n + k log n ) 
        {

            byte temp = 0;
            MaxHeap maxh = new MaxHeap(arr, n); //O(n)
            for (int i = 0; i < k; i++) // K
            {
                temp = maxh.topMax(); // O(1)
                maxh.popMax();   // O (log n)
                arr[n - (i + 1)] = temp;     // O (1)
            }

            return temp;
        }

    }
}
