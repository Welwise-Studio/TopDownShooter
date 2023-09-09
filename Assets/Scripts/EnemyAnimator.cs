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
        Animation(_enemyScript.currentState);
    }
    private void Animation(Enemy.State state)
    {
        if (_animator != null)
        {
            _animator.SetBool("isAttack_b", false);
            _animator.SetInteger("attack_int", -1);

            switch (state)
            {
                case Enemy.State.Spawning:
                    _enemyScript._pathfinder.enabled = false;
                    break;

                case Enemy.State.Idle:
                    _animator.SetFloat("speed_f", 0);
                    _enemyScript._pathfinder.enabled = false;
                    break;

                case Enemy.State.Chasing:
                    _animator.SetFloat("speed_f", _enemyScript._pathfinder.speed);
                    _enemyScript._pathfinder.enabled = true;
                    break;

                case Enemy.State.Attacking:
                    _animator.SetBool("isAttack_b", true);
                    _animator.SetInteger("attack_int", Random.Range(0, 2));
                    break;

                case Enemy.State.TakeDamage:
                    _animator.SetBool("isTakeDamage", false);
                    _animator.SetBool("isTakeDamage", true);
                    _enemyScript.SetState(Enemy.State.Idle);
                    break;
            }
        }
    }
    private void Handle_AnimationOver(string animationName)
    {
        switch (animationName)
        {
            case "spawning":
                _animator.SetBool("isSpawning_b", false);
                _enemyScript._pathfinder.enabled = true;
                _enemyScript.SetState(Enemy.State.Chasing);
                break;

            case "takeDamage":
                _animator.SetBool("isTakeDamage", false);
                _animator.SetFloat("speed_f", _enemyScript._pathfinder.speed);
                _enemyScript.SetState(Enemy.State.Chasing);
                break;
        }
    }
}
