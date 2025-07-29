using ConsolGame.Domain.Entities;
using ConsolGame.Infrastructure;
using ConsolGame.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsolGame.Domain.Entities
{
    public class Player:Unit
    {
        public Player(string name)
        {
            _name = name;
            _id = 1;
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
            BonusStat();
            BonusStat();
            BonusStat();
            BonusStat();

        }
        public void StatAdd(char Key)
        {
            if (_freeStatsPoints <= 0) return;
            switch (Key)
            {
                case 'P': _strength++; break;
                case 'L': _agility++; break;
                case 'M': _endurance++; break;
                case 'O': _intelligence++; break;
                case 'K': _wisdom++; break;
                default: return;
            }
            _freeStatsPoints--;
        }
        
    }
}
