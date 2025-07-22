using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsolGame.Infrastructure
{
    class LoadMap
    {
        char[,] CharMap;
         public LoadMap() 
        {
            try
            {
                CharMap = ConvertToArrayChar(File.ReadAllLines("map/1.txt"));
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }
       
        char[,] ConvertToArrayChar(string[] st)
        {
            int height =st.Count() ;
            int width = st[0].Length ;
            char[,] resault = new char[height,width];
            for (int i = 0;i<height;i++)
            {
                for(int j = 0; j<width; j++)
                {
                    resault[i, j] = st[i][j];
                    Console.Write(st[i][j]);
                }
                Console.WriteLine();
            }
            return null;
        }
    }
}
