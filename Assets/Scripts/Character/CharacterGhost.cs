using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGhost : CharacterBase
{
    /// <summary>
    /// ���݂̏��
    /// </summary>
    GameDifinition.eGhostStatus _current_status;

    /// <summary>
    /// �����畜�A����܂ł̎���
    /// </summary>
    [SerializeField]
    float _recover_time = 2.0f;

    /// <summary>
    /// �����e�𓖂Ă��Ă���̌o�ߎ���
    /// </summary>
    float _current_recover_timer = 0f;

    private Renderer rend;

    public override void Setup()
    {
        rend = GetComponent<Renderer>();
        _current_recover_timer = 0;
        _current_status = GameDifinition.eGhostStatus.ShootHit;

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

        // �F���A
        if(_current_recover_timer > 0)
        {
            return;
        }
        _current_recover_timer = 0;
        _current_status = GameDifinition.eGhostStatus.ShootHit;

        enableColorChange = true;
        rend.enabled = true;
        SetColor( (GameDifinition.eColor)Random.Range(1, 4));
    }

    private void _HitShoot(GameDifinition.eColor hit_color)
    {
        // HP���ő�̎��̂݃q�b�g
        if (_current_status != GameDifinition.eGhostStatus.ShootHit)
        {
            return;
        }

        if (currentColor == hit_color)
        {
            _current_status = GameDifinition.eGhostStatus.PhotographHit;
            currentColor = GameDifinition.eColor.White;
            _current_recover_timer = _recover_time;
            enableColorChange = false;
        }
    }

    private void _HitPhotograph()
    {
        // �c��HP��1�̎��̂݃q�b�g
        if(_current_status != GameDifinition.eGhostStatus.PhotographHit)
        {
            return;
        }

        // ���ꂽ
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
