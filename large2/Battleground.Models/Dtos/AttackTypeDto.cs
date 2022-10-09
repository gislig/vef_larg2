using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleground.Models.Dtos
{
    public class AttackTypeDto
    {
        public int DamageDealt {get; set;}
        public int CriticalHit {get; set;}
        public bool SuccessHit {get; set;}
        public PokemonDto AttackedBy {get; set;}

    }
}