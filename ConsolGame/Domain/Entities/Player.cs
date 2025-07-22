using System;
using System.Collections.Generic;
using System.Linq;
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

        }
        public void StatAdd(char Key)
        {
            switch (Key)
            {
                case 'P':
                    if (_freeStatsPoints > 0)
                    {
                        _freeStatsPoints--;
                        _strength++;
                    }
                    break;
                case 'L':
                    if (_freeStatsPoints > 0)
                    {
                        _freeStatsPoints--;
                        _agility++;
                    }
                    break;
                case 'M':
                    if (_freeStatsPoints > 0)
                    {
                        _freeStatsPoints--;
                        _endurance++;
                    }
                    break;
                case 'O':
                    if (_freeStatsPoints > 0)
                    {
                        _freeStatsPoints--;
                        _intelligence++;
                    }
                    break;
                case 'K':
                    if (_freeStatsPoints > 0)
                    {
                        _freeStatsPoints--;
                        _wisdom++;
                    }
                    break;
            }
        }
    }
}
