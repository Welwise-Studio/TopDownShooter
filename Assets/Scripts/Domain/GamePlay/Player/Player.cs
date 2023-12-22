using Domain.GamePlay.Units;
using System;
using UnityEngine;

namespace Domain.GamePlay.Player
{
    public class Player : Unit
    {
        [SerializeField]
        private PlayerStats Stats;
        public override void Hit(Vector3 point, Vector3 direction)
        {
            throw new NotImplementedException();
        }

        protected override void Die()
        {
            throw new NotImplementedException();
        }
    }
}
