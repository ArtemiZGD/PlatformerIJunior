using System.Collections.Generic;
using UnityEngine;

public class PlayerVampirismAbility : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerVampirismAbilityView _view;
    [SerializeField] private float _vampirismDuration;
    [SerializeField] private float _vampirismReloadTime;
    [SerializeField] private float _vampirismRadius;
    [SerializeField] private float _vampirismAmountInSecond;
    [SerializeField] private KeyCode _vampirismKey;

    private float _vampirismTimer = 0;
    private bool _isVampirismActive = false;
    private bool _isVampirismReloading = false;

    private void Start()
    {
        _view.Initialize(_vampirismRadius);
    }

    private void Update()
    {
        if (_isVampirismReloading)
        {
            Reload();
        }
        else if (_isVampirismActive == false)
        {
            if (Input.GetKeyDown(_vampirismKey))
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
        _view.BarAmountChanged?.Invoke(_vampirismTimer / _vampirismReloadTime);

        if (_vampirismTimer >= _vampirismReloadTime)
        {
            _isVampirismReloading = false;
        }
    }

    private void ActivateVampirism()
    {
        _isVampirismActive = true;
        _vampirismTimer = _vampirismDuration;
        _view.SetActive(true);
    }

    private void ProcessVampirism()
    {
        _vampirismTimer -= Time.deltaTime;
        _view.BarAmountChanged?.Invoke(_vampirismTimer / _vampirismDuration);

        if (_vampirismTimer <= 0)
        {
            _isVampirismActive = false;
            _isVampirismReloading = true;
            _view.SetActive(false);
            return;
        }

        if (TryGetEnemiesInRange(out var enemies))
        {
            float vampirismAmount = _vampirismAmountInSecond * Time.fixedDeltaTime;

            foreach (var enemy in enemies)
            {
                enemy.TakeDamage(vampirismAmount);
                _playerHealth.Heal(vampirismAmount);
                _view.SpawnParticles(enemy.transform, Time.fixedDeltaTime);
            }
        }
    }

    private bool TryGetEnemiesInRange(out List<EnemyHealth> enemies)
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _vampirismRadius);

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
