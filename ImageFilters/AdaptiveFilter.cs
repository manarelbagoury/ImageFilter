using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    class AdaptiveFilter
    {
        public static byte[,] filter(byte[,] ImageMatrix, int Ws, string c)
        {
            byte[] a;
            byte Zmed;
            byte Zmin;
            byte Zmax;
            byte A1;
            byte A2;
            byte B1;
            byte B2;
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)
                {
                    int windowsize = 3;

                    while (true)
                    {
                        var arlist1 = new ArrayList();
                        for (int ii = i - (windowsize / 2); ii <= i + (windowsize / 2); ii++)
                        {

                            if (ii < 0 || ii >= ImageMatrix.GetLength(0))
                            {

                                continue;
                            }

                            for (int jj = j - (windowsize / 2); jj <= j + (windowsize / 2); jj++)
                            {
                                if (jj < 0 || jj >= ImageMatrix.GetLength(1))
                                {

                                    continue;
                                }

                                arlist1.Add(ImageMatrix[ii, jj]);

                            }

                        }
                        a = new byte[arlist1.Count];
                        for (int k = 0; k < arlist1.Count; k++)
                        {
                            a[k] = (Byte)arlist1[k];
                        }

                        if(c == "Counting Sort")
                        {
                            a = Sorting.CountingSort(a, a.Length);
                        }
                        else if (c == "Quick Sort")
                        {
                            a = Sorting.quicksort(a, 0, a.Length - 1);
                        }

                        if(a.Length % 2 == 0)
                        {
                            int x = (a[(a.Length / 2) - 1] + a[(a.Length / 2)]) / 2;
                            Zmed = (byte)x;
                        }
                        else
                        {
                            Zmed = a[(a.Length / 2)];
                        }

                        Zmin = a[0];
                        Zmax = a[a.Length - 1];
                        A1 = (byte)(Zmed - Zmin);
                        A2 = (byte)(Zmax - Zmed);

                        if (A1 <= 0 || A2 <= 0)
                        {
                            windowsize += 2;
                            if (windowsize > Ws)
                            {
                                ImageMatrix[i, j] = Zmed;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            B1 = (byte)(ImageMatrix[i, j] - Zmin);
                            B2 = (byte)(Zmax - ImageMatrix[i, j]);
                            if (B1 <= 0 || B2 <= 0)
                            {
                                ImageMatrix[i, j] = Zmed;
                            }
                            break;
                        }

                    }

                }

            }
            return ImageMatrix;
        }
    }
}
