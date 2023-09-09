using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public static event System.Action OnDeathStatic;
    public static event System.Action<Vector3> OnDeathStaticPosition;
    public enum State { Spawning, Idle, Chasing, Attacking, TakeDamage };
    public State currentState { get; private set; }
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private ParticleSystem _takeDamageEffect;

    private LivingEntity _targetEntity;

    [SerializeField] private float _pathRefrashRate = 0.25f;
    public NavMeshAgent _pathfinder { get; private set; }
    private Transform _target;
    //private Material _skinMaterial;
    //private Color _originalColor;

    [SerializeField] private float _attackDistanceThreshold = 0.5f;
    [SerializeField] private float _timeBetweenAttack = 1f;
    [SerializeField] private float damage = 1f;
    private float nextAttackTime;

    private float _enemyCollisionRadius;
    private float _targetCollisionRadius;
    private bool _hasTarget;


    #region MONO
    private void Awake()
    {
        _pathfinder = GetComponent<NavMeshAgent>();

        if (FindObjectOfType<Player>() != null)
        {
            _hasTarget = true;

            _target = FindObjectOfType<Player>().transform;
            _targetEntity = _target.GetComponent<LivingEntity>();

            _enemyCollisionRadius = GetComponent<CapsuleCollider>().radius;
            _targetCollisionRadius = _target.GetComponent<CapsuleCollider>().radius;
        }
    }
    protected override void Start()
    {
        base.Start();

        SetState(State.Spawning);

        if (_hasTarget)
        {
            //currentState = State.Chasing;

            _targetEntity.OnDeath += OnTargetDeath;

            StartCoroutine(UpdatePath());
        }
    }
    private void Update()
    {
        if (_hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDstToTarget = (_target.position - transform.position).sqrMagnitude;

                if (sqrDstToTarget < Mathf.Pow(_attackDistanceThreshold + _enemyCollisionRadius + _targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + _timeBetweenAttack;
                    AudioManager.Instance.PlaySound("Enemy Attack", transform.position);

                    StartCoroutine(Attack());
                }
            }
        }
    }
    #endregion
    private IEnumerator Attack()
    {
        SetState(State.Attacking);
        _pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (_target.position - transform.position).normalized;
        Vector3 attackPosition = _target.position - dirToTarget * (_enemyCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;

        //_skinMaterial.color = Color.red;

        bool hasAppliedDamage = false;

        while (percent <= 1)
        {
            if (percent >= 0.5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                _targetEntity.TakeDamage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            //float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            //transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        //_skinMaterial.color = _originalColor;

        SetState(State.Chasing);
        _pathfinder.enabled = true;
    }
    public void SetState(State state)
    {
        currentState = state;
    }
    public void SetCharacteristics(float moveSpeed, int hitsToKillPlayer, float enemyHealth, Color skinColor, GameObject skin)
    {
        _pathfinder.speed = moveSpeed;

        if (_hasTarget)
        {
            damage = Mathf.Ceil(_targetEntity.startingHealth / hitsToKillPlayer);
        }

        startingHealth = enemyHealth;

        //Set color take damage effect particles.
        ParticleSystem.MainModule particleSystemMainTakeDamageEffect = _takeDamageEffect.main;
        particleSystemMainTakeDamageEffect.startColor = new Color(skinColor.r, skinColor.g, skinColor.b, 1);

        //Set color death effect particles.
        ParticleSystem.MainModule particleSystemMainDeathEffect = _deathEffect.main;
        particleSystemMainDeathEffect.startColor = new Color(skinColor.r, skinColor.g, skinColor.b, 1);

        Instantiate(skin,
        this.transform.position - Vector3.up,
        Quaternion.identity,
        this.transform).AddComponent<EnemyAnimator>();

        // _skinMaterial = GetComponent<Renderer>().material;
        // _skinMaterial.color = skinColor;
        // _originalColor = _skinMaterial.color;

    }
    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (currentState != State.Spawning)
        {
            AudioManager.Instance.PlaySound("Impact", transform.position);

            if (damage >= health && !dead)
            {
                OnDeathStatic?.Invoke();
                OnDeathStaticPosition?.Invoke(transform.position);

                AudioManager.Instance.PlaySound("Enemy Death", transform.position);

                Destroy(Instantiate(_deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)), _deathEffect.main.startLifetime.constant);
            }

            SetState(State.TakeDamage);

            Destroy(Instantiate(_takeDamageEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)), _takeDamageEffect.main.startLifetime.constant);

            base.TakeHit(damage, hitPoint, hitDirection);
        }
    }
    private void OnTargetDeath()
    {
        _hasTarget = false;
        SetState(State.Idle);
    }
    private IEnumerator UpdatePath()
    {
        while (_hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (_target.position - transform.position).normalized;
                Vector3 targetPosition = _target.position - dirToTarget * (_enemyCollisionRadius + _targetCollisionRadius + _attackDistanceThreshold / 2);

                if (!dead)
                {
                    _pathfinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(_pathRefrashRate);
        }
    }
}
