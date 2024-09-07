using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    /// <summary>
    /// 弾速
    /// </summary>
    [SerializeField]
    protected float _speed = 10;

    /// <summary>
    /// 消滅までの時間
    /// </summary>
    [SerializeField]
    protected float _life_time = 1f;
    /// <summary>
    /// 現在の生存時間
    /// </summary>
    protected float _current_life_time = 0;

    private void Update()
    {
        transform.position += transform.forward * _speed;

        // 自然消滅までの時間
        _current_life_time += Time.deltaTime;
        if (_current_life_time > _life_time)
        {
            Destroy(gameObject);
        }

        _Update();
    }

    protected virtual void _Update(){}
}
