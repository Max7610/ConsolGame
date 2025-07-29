using ConsolGame.Domain.Entities;
using ConsolGame.Infrastructure;
using ConsolGame.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsolGame.Application
{
    public class GameMeneger
    {
        LoadMap _map;
        Player _player;
        ConsoleWindow _window;
        Dictionary<int, Animal> _animals;
        Dictionary<int, Vector2> _animalsPosition;
        int IdCount = 2;
        int timeStep = 1;

        public GameMeneger() 
        {
            Initialization();
        }

        void Initialization()
        {
            _map = new LoadMap();
            _player = new Player("Max");
            _window = new ConsoleWindow();
            _animals = new Dictionary<int, Animal>();
            _animalsPosition = new Dictionary<int, Vector2>();
            _animalsPosition.Add(_player.Id, new Vector2(30, 10));
            AddAnimal();
        }
        public void Start()
        {            
            
            while (_player.Life)
            {
                _window.Print(_player, CopyArray(_map.CharMap), _animalsPosition);
                Temp();  
            }
        }
        char[,] CopyArray(char[,] source)
        {
            int rows = source.GetLength(0);
            int cols = source.GetLength(1);
            char[,] copy = new char[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    copy[i, j] = source[i, j];
            return copy;
        }
        void AddAnimal()
        {
            Random random = new Random();
            while (_animals.Count < 60)
            {
                
                Vector2 pozition = new Vector2(random.Next(0, _map.CharMap.GetLength(0)), random.Next(0, _map.CharMap.GetLength(1)));
                if(_map.CharMap[(int)pozition.X, (int)pozition.Y] == ' ' && !_animalsPosition.ContainsValue(pozition) )
                {
                    var animal = new Animal(IdCount, 0);
                    _animals.Add(IdCount, animal);
                    _animalsPosition.Add(IdCount++, pozition);
                }
            }
        }
        void Temp()
        {
            //темп состоит из прибавки времени, шаг тех кто может ходить
            //удар одновременно получение опыта, чистка мёртвых
            TimeAdd();
            PlayerPhase();
            AnimalGo();
            BatlePhaseAnimals();
            Funeral();
        }

        void PlayerPhase()
        {
            PlayerGo();
            PlayerLvlApp();
            PlayerBatle();
        }
        void PlayerGo()
        {
            if (_player.ChekTempPoints)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("KeyPositon:");
                char a = char.ToUpper(Console.ReadKey().KeyChar);
                _animalsPosition[_player.Id] = PlayerGoController(a, _animalsPosition[_player.Id], _map);
            }
        }
        void PlayerLvlApp()
        {
            if (_player.FreeStatsPoints)
            {
                _player.StatAdd(char.ToUpper(Console.ReadKey().KeyChar));
            }
        }
        void PlayerBatle()
        {
            var a = _animalsPosition.Where(x => x.Value == _animalsPosition[_player.Id] && x.Key != _player.Id).Select(y => y.Key).ToList();
            if (a.Count > 0) 
                foreach (var i in a)
                {
                    _player.TakingDamage(_animals[i].Damage);
                    _animals[i].TakingDamage(_player.Damage);
                    if (_player.Life && !_animals[i].Life)
                    {
                        _player.AddExp(_animals[i].Lvl);
                    }
                    if (_animals[i].Life && !_player.Life)
                    {
                        _animals[i].AddExp(_player.Lvl);
                    }
                }
            }
        
        Vector2 PlayerGoController(char a, Vector2 pos, LoadMap map)
        {
            switch (a)
            {
                case 'W':
                    if (map.ChekPlayerGo(new Vector2(pos.X, pos.Y - 1)))
                    {
                        pos = new Vector2(pos.X, pos.Y - 1);
                    }
                    break;
                case 'S':
                    if (map.ChekPlayerGo(new Vector2(pos.X, pos.Y + 1)))
                    {
                        pos = new Vector2(pos.X, pos.Y + 1);
                    }
                    break;
                case 'A':
                    if (map.ChekPlayerGo(new Vector2(pos.X - 1, pos.Y)))
                    {
                        pos = new Vector2(pos.X - 1, pos.Y);
                    }
                    break;
                case 'D':
                    if (map.ChekPlayerGo(new Vector2(pos.X + 1, pos.Y)))
                    {
                        pos = new Vector2(pos.X + 1, pos.Y);
                    }
                    break;
                default: return pos;
            }
            return pos;
        }

        void TimeAdd()
        {
            _player.AddTime(timeStep);
            foreach(var i in _animals)
            {
                i.Value.AddTime(timeStep);
            }
        }
        void AnimalGo() 
        {
            Random random = new Random();
            foreach(var i in _animals)
            {
                if(i.Value.ChekTempPoints)
                {
                    var a=_map.SearchForAvailableMovePositions(_animalsPosition[i.Value.Id]);
                    if (a.Count>0)
                    {
                        _animalsPosition[i.Value.Id] = a[random.Next(a.Count)];
                    }
                }
            }
        }
        void BatlePhaseAnimals()
        {
            var groupToPosition = _animalsPosition.GroupBy(x => x.Value).Where(y =>y.Count()>1);
            foreach (var i in groupToPosition)
            {
                foreach(var j in i)
                {
                    foreach(var g in i)
                    {
                        if (j.Key != g.Key && j.Key!=1 && g.Key != 1)
                        {
                            Batle(j.Key, g.Key);
                            break;
                        }
                    }    
                }
            }
            
        }

        void Batle(int g,int j)
        {
            _animals[j].TakingDamage(_animals[g].Damage);
            _animals[g].TakingDamage(_animals[j].Damage);
            if (_animals[j].Life && !_animals[g].Life)
            {
                _animals[j].AddExp(_animals[g].Lvl);
            }
            if (_animals[g].Life && !_animals[j].Life)
            {
                _animals[g].AddExp(_animals[j].Lvl);
            }
        }
        void Funeral()
        {
            List<int> deadList = new List<int>();
            deadList = _animals.Where(x => !x.Value.Life).Select(x => x.Key).ToList();
            foreach (var dead in deadList)
            {
                _animals.Remove(dead);
                _animalsPosition.Remove(dead);
            }
            
        }
    }
}
