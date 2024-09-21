using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGhost : CharacterBase
{
    /// <summary>
    /// ���݂̏��
    /// </summary>
    public GameDifinition.eGhostStatus _current_status;

    /// <summary>
    /// �ʏ퓧���x
    /// </summary>
    private float _def_alpha = 0;

    /// <summary>
    /// �����畜�A����܂ł̎���
    /// </summary>
    [SerializeField]
    float _recover_time = 2.0f;

    /// <summary>
    /// �����e�𓖂Ă��Ă���̌o�ߎ���
    /// </summary>
    float _current_recover_timer = 0f;

    /// <summary>
    /// �����_���\
    /// </summary>
    private Renderer rend;

    /// <summary>
    /// �X�v���C�g�����_���\
    /// </summary>
    private SpriteRenderer rend2D;

    /// <summary>
    /// �������U��
    /// </summary>
    [SerializeField]
    private Scare _scare;

    private void RendAction(System.Action<Renderer> rendAct, System.Action<SpriteRenderer> spriteAct)
    {
        if (rend)
        {
            rendAct(rend);
        }
        if (rend2D)
        {
            spriteAct(rend2D);
        }
    }

    public override void Setup()
    {
        _scare.gameObject.SetActive(false);
        rend = GetComponent<Renderer>();
        rend2D = GetComponentInChildren<SpriteRenderer>();
        _current_recover_timer = 0;
        _current_status = GameDifinition.eGhostStatus.ShootHit;
        RendAction(
            (r) => { _def_alpha = r.material.color.a; },
            (r) => { _def_alpha = r.material.color.a; });
        StartCoroutine("PlayAlphaAnimation", GameDifinition.eColor.Cyan);

        color_change_action += (set_color) =>
        {
            if (!_enable_color_change)
            {
                return;
            }

            if(set_color == GameDifinition.eColor.Yellow)
            {
                _scare.gameObject.SetActive(true);
            }
            else
            {
                _scare.gameObject.SetActive(false);
            }

            var rgb = GameDifinition.GetRGBColor(set_color);

            RendAction(
                (r) => {
                    rgb.a = r.material.color.a;
                    r.material.color = rgb;
                },
                (r) => {
                    rgb.a = r.material.color.a;
                    r.material.color = rgb;
                });
            
        };

        base.Setup();
    }

    /// <summary>
    /// �������A�j���Đ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayAlphaAnimation(GameDifinition.eColor set_color)
    {
        var rgb = GameDifinition.GetRGBColor(set_color);
        rgb.a = _def_alpha;
        RendAction(
            (r) => {
                r.material.color = rgb;
            },
            (r) => {
                r.material.color = rgb;
            });

        if (rend)
        {
            while (rend.material.color.a > 0)
            {
                rgb.a = Mathf.Clamp(rgb.a - 0.01f, 0, 1.0f);
                rend.material.color = rgb;
                yield return null;
            }
        }
        if (rend2D)
        {
            while (rend2D.material.color.a > 0)
            {
                rgb.a = Mathf.Clamp(rgb.a - 0.01f, 0, 1.0f);
                rend2D.material.color = rgb;
                yield return null;
            }
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

        // �F���A
        if(_current_recover_timer > 0)
        {
            return;
        }
        _current_recover_timer = 0;
        _current_status = GameDifinition.eGhostStatus.ShootHit;

        enableColorChange = true;
        RendAction(
            (r) => {
                r.enabled = true;
            },
            (r) => {
                r.enabled = true;
            });
        var color = (GameDifinition.eColor)Random.Range(1, 4);
        SetColor( color );

        StartCoroutine("PlayAlphaAnimation", color);
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

            // �����x1�̔��F�ɐ؂�ւ�
            var rgb = GameDifinition.GetRGBColor(GameDifinition.eColor.White);
            rgb.a = 1.0f;
            RendAction(
                (r) => {
                    r.material.color = rgb;
                },
                (r) => {
                    r.material.color = rgb;
                });

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
        RendAction(
            (r) => {
                r.enabled = false;
            },
            (r) => {
                r.enabled = false;
            });
    }
}
