using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGhost : CharacterBase
{
    /// <summary>
    /// 現在の状態
    /// </summary>
    public GameDifinition.eGhostStatus _current_status;

    /// <summary>
    /// 通常透明度
    /// </summary>
    private float _def_alpha = 0;

    /// <summary>
    /// 白から復帰するまでの時間
    /// </summary>
    [SerializeField]
    float _recover_time = 2.0f;

    /// <summary>
    /// 光線銃を当てられてからの経過時間
    /// </summary>
    float _current_recover_timer = 0f;

    /// <summary>
    /// レンダラ―
    /// </summary>
    private Renderer rend;

    public override void Setup()
    {
        rend = GetComponent<Renderer>();
        _current_recover_timer = 0;
        _current_status = GameDifinition.eGhostStatus.ShootHit;
        _def_alpha = rend.material.color.a;
        StartCoroutine("PlayAlphaAnimation", GameDifinition.eColor.Cyan);

        color_change_action += (set_color) =>
        {
            if (!_enable_color_change)
            {
                return;
            }

            var rgb = GameDifinition.GetRGBColor(set_color);
            rgb.a = rend.material.color.a;
            rend.material.color = rgb;
        };

        base.Setup();
    }

    /// <summary>
    /// 透明化アニメ再生
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayAlphaAnimation(GameDifinition.eColor set_color)
    {
        var rgb = GameDifinition.GetRGBColor(set_color);
        rgb.a = _def_alpha;
        rend.material.color = rgb;

        while(rend.material.color.a > 0)
        {
            rgb.a = Mathf.Clamp(rgb.a - 0.002f, 0, 1.0f);
            rend.material.color = rgb;
            yield return null;
        }

        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        var shoot = other.GetComponent<Shoot>();
        if (shoot)
        {
            _HitShoot(shoot.currentColor);
        }

        var photograph = other.GetComponent<Photograph>();
        if (photograph)
        {
            _HitPhotograph();
        }
    }

    protected override void _Update()
    {
        base._Update();

        if(_current_recover_timer <= 0)
        {
            return;
        }
        _current_recover_timer -= Time.deltaTime;

        // 色復帰
        if(_current_recover_timer > 0)
        {
            return;
        }
        _current_recover_timer = 0;
        _current_status = GameDifinition.eGhostStatus.ShootHit;

        enableColorChange = true;
        rend.enabled = true;
        var color = (GameDifinition.eColor)Random.Range(1, 4);
        SetColor( color );

        StartCoroutine("PlayAlphaAnimation", color);
    }

    private void _HitShoot(GameDifinition.eColor hit_color)
    {
        // HPが最大の時のみヒット
        if (_current_status != GameDifinition.eGhostStatus.ShootHit)
        {
            return;
        }

        if (currentColor == hit_color)
        {
            _current_status = GameDifinition.eGhostStatus.PhotographHit;
            currentColor = GameDifinition.eColor.White;

            // 透明度1の白色に切り替え
            var rgb = GameDifinition.GetRGBColor(GameDifinition.eColor.White);
            rgb.a = 1.0f;
            rend.material.color = rgb;

            _current_recover_timer = _recover_time;
            enableColorChange = false;
        }
    }

    private void _HitPhotograph()
    {
        // 残りHPが1の時のみヒット
        if(_current_status != GameDifinition.eGhostStatus.PhotographHit)
        {
            return;
        }

        // やられた
        Failed();
    }

    public override void Failed()
    {
        base.Failed();

        _current_status = GameDifinition.eGhostStatus.Failed;

        _current_recover_timer = _recover_time;
        enableColorChange = false;
        rend.enabled = false;
    }
}
