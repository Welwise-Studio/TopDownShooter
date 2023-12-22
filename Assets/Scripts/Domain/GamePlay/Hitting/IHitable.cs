using System;
using UnityEngine;

namespace Domain.GamePlay.Hitting
{
    public interface IHitable
    {
        public void Hit(Vector3 point, Vector3 direction);
        public event Action<Vector3, Vector3> OnHit;
    }
}
