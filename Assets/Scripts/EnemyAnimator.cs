using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemyScript;

    private void Awake()
    {
        _enemyScript = GetComponentInParent<Enemy>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Animation();
    }
    private void Animation()
    {
        if (_animator != null)
        {
            if (_enemyScript.CurrentState == Enemy.State.Spawning)
            {
                _enemyScript._pathfinder.enabled = false;
            }
            else
            {
                _enemyScript._pathfinder.enabled = true;
            }

            if (_enemyScript.CurrentState == Enemy.State.Chasing)
            {
                _animator.SetFloat("speed_f", _enemyScript._pathfinder.speed);
            }

            if (_enemyScript.CurrentState == Enemy.State.Attacking)
            {
                _animator.SetBool("isAttack_b", true);
                _animator.SetInteger("attack_int", Random.Range(0, 2));
            }
            else
            {
                _animator.SetBool("isAttack_b", false);
                _animator.SetInteger("attack_int", -1);
            }

            if (_enemyScript.CurrentState == Enemy.State.Idle)
            {
                _animator.SetFloat("speed_f", 0);
            }

            if (_enemyScript.CurrentState == Enemy.State.TakeDamage)
            {
                _animator.SetBool("isTakeDamage", true);
            }
            else
            {
                _animator.SetBool("isTakeDamage", false);
            }
        }
    }
}
