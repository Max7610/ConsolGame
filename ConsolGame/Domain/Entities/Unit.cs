using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace ConsolGame.Domain.Entities
{
    public abstract class Unit
    {
        protected string _name;
        public string Name { get { return _name; } }
        protected int _lvl;
        public int Lvl { get { return _lvl; } }
        protected int _exp;
        public int Exp { get { return _exp; } }
        public int ExpForLvlApp { get { return (int)(Lvl * Math.Sqrt(Lvl) * 10); } }
        protected int _id;
        public int Id { get { return _id; } }
        
        protected int _endurance;
        public int Endurance { get { return _endurance; } }
        protected int _strength;
        public int Strength { get { return _strength; } }
        protected int _agility;
        public int Agility { get { return _agility; } }
        protected int _intelligence;
        public int Intelligence { get { return _intelligence; } }
        protected int _wisdom;
        public int Wisdom { get { return _wisdom; } }
        protected int _mp;
        public int Mp { get { return _mp; } }
        protected int _hp;
        public int Hp { get { return _hp; } }
        public int MaxHp { get { return Endurance * 10 + Strength * 3; } }
        public int MaxMp { get { return Intelligence * 8 + Wisdom * 2; } }
        public int Speed { get { return Agility * 10 + Intelligence * 5 + Wisdom * 5; } }
        public int MpForSkill { get { return (Wisdom > 0) ? (Intelligence / Wisdom * 10) : (Intelligence * 5); } }
        public int Damage
        {
            get
            {
                if (Mp > MpForSkill)
                {
                    _mp -= MpForSkill;
                    return Lvl + Strength * 6 + Agility * 3 + Wisdom * MpForSkill / 2;
                }
                else
                {
                    return Lvl + Strength * 6 + Agility * 3;
                }
            }
        }
        protected int _stamina;
        public int Stamina { get { return _stamina; } }
        public int MaxStamina { get { return _endurance * 10 + _strength * 5 + _agility * 5; } }
        protected int StForSkill { get {return _strength*3+_agility*2;} }
        protected int _freeStatsPoints;
        public int FreeStatsPoints { get { return _freeStatsPoints;} }

        public void AddExp(int lvl)
        {
            _exp += lvl * (int)Math.Sqrt(lvl)+2;
            while (_exp>=ExpForLvlApp)
            {
                _exp -= ExpForLvlApp;
                _lvl++;
                _freeStatsPoints++;
                _mp = MaxMp;
                _hp = MaxHp;
                _stamina = MaxStamina;
            }
        }
        
    }
}
