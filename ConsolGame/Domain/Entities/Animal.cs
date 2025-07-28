using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsolGame.Domain.Entities
{
    internal class Animal:Unit
    {
        public Animal(int id,int matherId) 
        {
            _name = $"#{matherId}_{id}";
            _id = id;
            _endurance = 1;
            _strength = 1;
            _agility = 1;
            _intelligence = 1;
            _wisdom = 1;
            _mp = MaxMp;
            _hp = MaxHp;
            _stamina = MaxStamina;
            _freeStatsPoints = 0;
            _exp = 0;
            _lvl = 1;
        }
        public void StatAdd()
        {
            if (_freeStatsPoints <= 0) return;
            int rnd = RandomNumberGenerator.GetInt32(5);
            switch (rnd)
            {
                case 0: _strength++; break;
                case 1: _agility++; break;
                case 2: _endurance++; break;
                case 3: _intelligence++; break;
                case 4: _wisdom++; break;
                default: return;
            }
            _freeStatsPoints--;
        }
    }
}
