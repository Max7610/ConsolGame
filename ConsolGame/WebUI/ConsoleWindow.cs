using ConsolGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsolGame.WebUI
{
    public class ConsoleWindow
    {
        /*окно состоит из 3х модулей статус, характеристики, карта
         * данный класс отвеает только за отрисовку полученных данных
         * отдельно необходимо сформировать класс, в котором будет пакет текущих данных 
         */
        StatusWindows statmodul = new StatusWindows { Top = 1, Left = 1 };
        public void Print(Player player)
        {
            statmodul.player = player;
            statmodul.Print();
        }
        
    }
    abstract class WindowModul
    {
        protected int _top;
        protected int _left;
        protected int line;
        protected Player _player;
        public int Top { set { _top = value; } }
        public int Left { set { _left = value; } }
        public WindowModul(){}
        public Player player { set { _player = value; } }
        public abstract void Print();
        
    }
    class StatusWindows:WindowModul
    {
        
        public override void Print()
        {
            PrintName();
            PrintStatusBar();
            PrintStatus();
        }
        void PrintName()
        {
            line = 0;
            Console.SetCursorPosition(_left, _top + line);
            Console.WriteLine($"***{_player.Name}*** Lvl:{_player.Lvl}");
            line++;
        }
        void PrintStatusBar()
        {
            PrintBar("Hp", _player.Hp, _player.MaxHp, ConsoleColor.Red, ConsoleColor.DarkRed);
            PrintBar("Mp", _player.Mp, _player.MaxMp, ConsoleColor.Blue, ConsoleColor.DarkBlue);
            PrintBar("St", _player.Stamina, _player.MaxStamina, ConsoleColor.Yellow, ConsoleColor.DarkYellow);
            PrintBar("Ex", _player.Exp, _player.ExpForLvlApp, ConsoleColor.Magenta, ConsoleColor.DarkMagenta);
        }
        void PrintStatus()
        {
            line += 2;
            Console.SetCursorPosition(_left, _top + line);
            Console.WriteLine($"  Свободные:{_player.FreeStatsPoints}");
            line += 2;
            Console.SetCursorPosition(_left, _top + line);
            Console.WriteLine($"<P>  Сила {_player.Strength}");
            line += 2;
            Console.SetCursorPosition(_left, _top + line);
            Console.WriteLine($"<L> Ловкость {_player.Agility}");
            line += 2;
            Console.SetCursorPosition(_left, _top + line);
            Console.WriteLine($"<M>  Выносливость {_player.Endurance}");
            line += 2;
            Console.SetCursorPosition(_left, _top + line);
            Console.WriteLine($"<O>  Интеллект {_player.Intelligence}");
            line += 2;
            Console.SetCursorPosition(_left, _top + line);
            Console.WriteLine($"<K>  Мудрость {_player.Wisdom}");
        }

        void PrintBar(string stat,int count,int maxCount,ConsoleColor FullColor,ConsoleColor EmptyColor)
        {
            int width = 6;
            Console.SetCursorPosition(_left, _top + line);
            Console.Write($"{stat} {count}{new string(' ', 6-count.ToString().Length)}/{maxCount}{new string(' ', 6 - maxCount
                .ToString().Length)}:");
            Console.BackgroundColor = FullColor;
            int MaxLenBar = 10;
            int LenBarHp = (int)(((double)count / maxCount) * MaxLenBar);
            string FullBar = new string(' ', LenBarHp);
            string EmptyBar = new string(' ', MaxLenBar - LenBarHp);
            Console.Write($"{FullBar}");
            Console.BackgroundColor = EmptyColor;
            Console.Write($"{EmptyBar}");
            Console.BackgroundColor = ConsoleColor.Black;
            line++;
        }
    }
}
