using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Domain.GamePlay.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {
        private readonly int _refreshDestionationFrameRate = 50;

        private NavMeshAgent _navMeshAgent;
        private Transform _target;

        private int _frameCounter;

        private void Update()
        {
            if (++_frameCounter > _refreshDestionationFrameRate)
                _navMeshAgent.destination = _target.position;
        }



        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}
