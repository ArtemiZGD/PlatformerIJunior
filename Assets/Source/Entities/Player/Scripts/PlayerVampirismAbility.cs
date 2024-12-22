using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVampirismAbility : MonoBehaviour
{
    private const int FullCharge = 1;

    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private float _duration;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _radius;
    [SerializeField] private float _amountInSecond;
    [SerializeField] private HotKeys _hotKeys;

    public Action<bool> ChangeActivation;
    public Action<Transform, float> SpawnParticles;
    public Action<float> ChangeBarAmount;

    private float _vampirismTimer = 0;
    private bool _isVampirismActive = false;
    private bool _isVampirismReloading = false;

    public float StartAmount => FullCharge;
    public float VampirismRadius => _radius;

    private void Update()
    {
        if (_isVampirismReloading)
        {
            Reload();
        }
        else if (_isVampirismActive == false)
        {
            if (Input.GetKeyDown(_hotKeys.VampirismKey))
            {
                ActivateVampirism();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isVampirismActive)
        {
            ProcessVampirism();
        }
    }

    private void Reload()
    {
        _vampirismTimer += Time.deltaTime;
        ChangeBarAmount?.Invoke(_vampirismTimer / _reloadTime);

        if (_vampirismTimer >= _reloadTime)
        {
            _isVampirismReloading = false;
        }
    }

    private void ActivateVampirism()
    {
        _isVampirismActive = true;
        _vampirismTimer = _duration;
        ChangeActivation?.Invoke(true);
    }

    private void ProcessVampirism()
    {
        _vampirismTimer -= Time.deltaTime;
        ChangeBarAmount?.Invoke(_vampirismTimer / _duration);

        if (_vampirismTimer <= 0)
        {
            _isVampirismActive = false;
            _isVampirismReloading = true;
            ChangeActivation?.Invoke(false);
            return;
        }

        if (TryGetEnemiesInRange(out var enemies))
        {
            float vampirismAmount = _amountInSecond * Time.fixedDeltaTime;

            foreach (var enemy in enemies)
            {
                enemy.TakeDamage(vampirismAmount);
                _playerHealth.Heal(vampirismAmount);
                SpawnParticles?.Invoke(enemy.transform, Time.fixedDeltaTime);
            }
        }
    }

    private bool TryGetEnemiesInRange(out List<EnemyHealth> enemies)
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _radius);

        enemies = new List<EnemyHealth>();

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out EnemyHealth enemy))
            {
                enemies.Add(enemy);
            }
        }

        return enemies.Count > 0;
    }
}
