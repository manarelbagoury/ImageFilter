using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    class Test
    {
        public static byte[,] alphaTrimCounting(byte[,] ImageMatrix, int Ws)
        {
            byte Zmed;
            byte Zmin;
            byte Zmax;
            byte A1;
            byte A2;
            byte B1;
            byte B2;
            //_______CREATING A PADDED VERSION OF IMAGE MATRIX________

            int rows = ImageOperations.GetHeight(ImageMatrix) + 2;
            int col = ImageOperations.GetWidth(ImageMatrix) + 2;
            byte[,] ImageMatrix_padded = new byte[rows, col];
            for (int i = 0; i < rows - 2; i++)
                for (int j = 0; j < col - 2; j++)
                    ImageMatrix_padded[i + 1, j + 1] = ImageMatrix[i, j];

            //______________________________
            byte[,] result = ImageMatrix;
            int windowsize = 3;
            while(windowsize < Ws)
            {
                int limit = windowsize / 2;
                for (int i = limit; i < rows - limit; i++)
                {
                    for (int j = limit; j < col - limit; j++)
                    {
                        //__________GETTING WINDOW____________
                        byte[] window = new byte[windowsize * windowsize];
                        int window_index = 0;
                        for (int rowInner = i - limit; rowInner <= i + limit; rowInner++)
                        {
                            for (int colInner = j - limit; colInner <= j + limit; colInner++)
                            {

                                window[window_index] = ImageMatrix_padded[rowInner, colInner];
                                window_index++;
                            }
                        }

                        //________SORTING WINDOW USING COUNT SORT_________

                        window = Sorting.CountingSort(window, window.Length);

                        if (window.Length % 2 == 0)
                        {
                            int x = (window[(window.Length / 2) - 1] + window[(window.Length / 2)]) / 2;
                            Zmed = (byte)x;
                        }
                        else
                        {
                            Zmed = window[(window.Length / 2)];
                        }

                        Zmin = window[0];
                        Zmax = window[window.Length - 1];
                        A1 = (byte)(Zmed - Zmin);
                        A2 = (byte)(Zmax - Zmed);

                        if (A1 <= 0 || A2 <= 0)
                        {
                            windowsize += 2;
                            if (windowsize > Ws)
                            {
                                ImageMatrix_padded[i, j] = Zmed;
                                ImageMatrix[i - 1, j - 1] = Zmed;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            B1 = (byte)(ImageMatrix_padded[i, j] - Zmin);
                            B2 = (byte)(Zmax - ImageMatrix_padded[i, j]);
                            if (B1 <= 0 || B2 <= 0)
                            {
                                ImageMatrix_padded[i, j] = Zmed;
                                ImageMatrix[i - 1, j - 1] = Zmed;
                            }
                            break;
                        }
                    }
                }
                windowsize += 2;
            }
            return ImageMatrix;
        }
    }
}