using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerVampirismAbilityView : BarView
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _particlesRate;
    [SerializeField] private float _minParticleSpeed;
    [SerializeField] private float _maxParticleSpeed;
    [SerializeField] private float _randomSpawnRadius;

    private SpriteRenderer _spriteRenderer;
    private ParticleSystem.Particle[] _particles;
    private List<Transform> _isEmitionActive = new();

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        UpdateParticleVelocities();
    }

    public override float GetStartValue()
    {
        return 1;
    }

    public void Initialize(float radius)
    {
        transform.localScale = radius * 2 * Vector2.one;
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        _spriteRenderer.enabled = active;
    }

    public void SpawnParticles(Transform from, float duration)
    {
        if (_isEmitionActive.Contains(from)) return;

        StartCoroutine(SpawnParticlesCoroutine(from, duration));
    }

    private IEnumerator SpawnParticlesCoroutine(Transform from, float duration)
    {
        float elapsedTime = 0f;
        _isEmitionActive.Add(from);

        while (elapsedTime < duration)
        {
            ParticleSystem.EmitParams emitParams = new();
            Vector3 randomOffset = Random.insideUnitCircle * _randomSpawnRadius;
            emitParams.position = from.position + randomOffset;
            Vector3 direction = (transform.position - emitParams.position).normalized;
            emitParams.velocity = direction * Random.Range(_minParticleSpeed, _maxParticleSpeed);
            _particleSystem.Emit(emitParams, 1);

            elapsedTime += 1f / _particlesRate;
            yield return new WaitForSeconds(1f / _particlesRate);
        }

        _isEmitionActive.Remove(from);
    }

    private void UpdateParticleVelocities()
    {
        if (_particles == null || _particles.Length < _particleSystem.particleCount)
        {
            _particles = new ParticleSystem.Particle[_particleSystem.particleCount];
        }

        int numParticlesAlive = _particleSystem.GetParticles(_particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 direction = transform.position - _particles[i].position;

            if (direction.magnitude < 0.1f)
            {
               _particles[i].remainingLifetime = 0;
            }

            _particles[i].velocity = direction.normalized * _particles[i].velocity.magnitude;
        }

        _particleSystem.SetParticles(_particles, numParticlesAlive);
    }
}
