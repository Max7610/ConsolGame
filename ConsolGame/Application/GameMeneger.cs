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
        List<Animal> _animals;
        Dictionary<int, Vector2> _animalsPosition;
        int IdCount = 2;

        public GameMeneger() 
        {
            Initialization();
        }

        void Initialization()
        {
            _map = new LoadMap();
            _player = new Player("Max");
            _window = new ConsoleWindow();
            _animals = new List<Animal>();
            _animalsPosition = new Dictionary<int, Vector2>();
            _animalsPosition.Add(_player.Id, new Vector2(30, 10));
            AddAnimal();
        }
        public void Start()
        {
            
            _window.Print(_player,_map.CharMap,_animalsPosition);
           
            
        }
        public void AddAnimal()
        {
            Random random = new Random();
            while (_animals.Count < 60)
            {
                
                Vector2 pozition = new Vector2(random.Next(0, _map.CharMap.GetLength(0)), random.Next(0, _map.CharMap.GetLength(1)));
                if(_map.CharMap[(int)pozition.X, (int)pozition.Y] == ' ' && !_animalsPosition.ContainsValue(pozition) )
                {
                    var animal = new Animal(IdCount, 0);
                    _animals.Add(animal);
                    _animalsPosition.Add(IdCount++, pozition);
                }
            }
        }
    }
}
