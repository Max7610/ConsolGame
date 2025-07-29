using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsolGame.Infrastructure
{
    class LoadMap
    {
        char[,] _charMap;
        public char[,] CharMap { get { return _charMap; } }
         public LoadMap() 
        {
            try
            {
                _charMap = ConvertToArrayChar(File.ReadAllLines("map/1.txt"));
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error {ex.Message}");
                Console.ReadKey();
            }
        }
       
        char[,] ConvertToArrayChar(string[] st)
        {
            if (st == null || st.Length == 0)
            {
                return new char[0, 0];
            }
            int height = st.Count();
            int width = st[0].Length;
            char[,] resault = new char[width,height];
            for (int i = 0;i<height;i++)
            {
                for(int j = 0; j<width; j++)
                {
                    try { resault[j, i] = st[i][j]; }
                    catch (Exception ex) 
                    {
                        Console.WriteLine($"{ex}");
                        Console.ReadKey();
                    }   
                }              
            }
            return resault;
        }
        public List<Vector2> SearchForAvailableMovePositions(Vector2 Unit)
        {
            List<Vector2> resault = new List<Vector2>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (_charMap[(int)Unit.X + x, (int)Unit.Y + y] != '#')
                    {
                        resault.Add(new Vector2((int)Unit.X + x, (int)Unit.Y + y));
                    }
                }
            }
            return resault;
        }
        public bool ChekPlayerGo(Vector2 pos)
        {
            return _charMap[(int)pos.X,(int)pos.Y]!='#';
        }

    }
}
