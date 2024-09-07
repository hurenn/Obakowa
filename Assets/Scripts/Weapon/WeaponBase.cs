using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    /// <summary>
    /// íeë¨
    /// </summary>
    [SerializeField]
    protected float _speed = 10;

    /// <summary>
    /// è¡ñ≈Ç‹Ç≈ÇÃéûä‘
    /// </summary>
    [SerializeField]
    protected float _life_time = 1f;
    /// <summary>
    /// åªç›ÇÃê∂ë∂éûä‘
    /// </summary>
    protected float _current_life_time = 0;

    private void Update()
    {
        transform.position += transform.forward * _speed;

        // é©ëRè¡ñ≈Ç‹Ç≈ÇÃéûä‘
        _current_life_time += Time.deltaTime;
        if (_current_life_time > _life_time)
        {
            Destroy(gameObject);
        }

        _Update();
    }

    protected virtual void _Update(){}
}
