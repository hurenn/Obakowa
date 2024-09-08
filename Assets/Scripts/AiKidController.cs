using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiKidController : AiController
{
    /// <summary>
    /// ショット間隔
    /// </summary>
    [SerializeField]
    float _shot_time = 1.0f;

    /// <summary>
    /// 現在のショットタイマー
    /// </summary>
    float _current_shot_timer = 0;

    protected override void _Update()
    {
        transform.Rotate(Vector3.up * 5.0f);

        _current_shot_timer += Time.deltaTime;
        if(_current_shot_timer > _shot_time)
        {
            _current_shot_timer = 0;

            _character.Shoot();
        }
    }
}
