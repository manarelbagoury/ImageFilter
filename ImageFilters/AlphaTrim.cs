using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    class AlphaTrim
    {
        public static byte[,] alphaTrim_Counting(byte[,] ImageMatrix, int windowSize, int trimValue)
        {

            byte[,] result = new byte[ImageMatrix.GetLength(0), ImageMatrix.GetLength(1)];
            int limit = windowSize / 2;
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)
                {
                    //__________GETTING WINDOW____________
                    byte[] window = new byte[windowSize * windowSize];
                    int window_index = 0;

                    for (int ii = i - limit; ii <= i + limit; ii++)
                    {

                        if (ii < 0 || ii >= ImageMatrix.GetLength(0))
                        {

                            continue;
                        }

                        for (int jj = j - limit; jj <= j + limit; jj++)
                        {
                            if (jj < 0 || jj >= ImageMatrix.GetLength(1))
                            {

                                continue;
                            }

                            window[window_index] = ImageMatrix[ii, jj];
                            window_index++;
                        }
                    }

                    //________SORTING WINDOW USING COUNT SORT_________

                    Sorting.CountingSort(window, window.Length);

                    //___________TRIM & CALC AVERAGE__________
                    int sum = 0;
                    for (int x = trimValue; x < window.Length - trimValue; x++)
                    {
                        sum += window[x];
                    }
                    byte avg = (byte)(sum / (window.Length - (trimValue * 2)));
                    //_________UPDATE RESULT MATRIX___________
                    result[i, j] = avg;
                }
            }
            return result;
        }


        public static byte[,] alphaTrim_KTH(byte[,] ImageMatrix, int windowSize, int trimValue)
        {

            // byte[,] result = ImageMatrix;
            int k = trimValue;
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)
                {

                    int Sum = 0;
                    int counter = 0;
                    byte[] window = new byte[windowSize * windowSize];
                    int window_index = 0;

                    for (int ii = i - (windowSize / 2); ii <= i + (windowSize / 2); ii++)
                    {

                        if (ii < 0 || ii >= ImageMatrix.GetLength(0))
                        {

                            continue;
                        }

                        for (int jj = j - (windowSize / 2); jj <= j + (windowSize / 2); jj++)
                        {
                            if (jj < 0 || jj >= ImageMatrix.GetLength(1))
                            {

                                continue;
                            }

                            window[window_index] = ImageMatrix[ii, jj];
                            window_index++;
                        }
                    }

                    //___Kth element____


                    int Largest = Sorting.KthElement(window, window.Length, k);
                    int kk = window.Length - (k - 1);
                    int Smallest = Sorting.KthElement(window, window.Length, kk);


                    for (int y = 0; y < window.Length; y++)
                    {
                        if (window[y] < Largest && window[y] > Smallest)
                        {

                            Sum += window[y];
                            counter++;

                        }

                    }
                    if (counter == 0)
                        counter = 1;
                    //____UPDATE RESULT MATRIX___
                    byte mean = (byte)(Sum / counter);
                    ImageMatrix[i, j] = mean;
                }
            }

            return ImageMatrix;
        }

    }
}