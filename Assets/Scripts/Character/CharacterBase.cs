using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    /// <summary>
    /// �������Z
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// �ړ��X�s�[�h
    /// </summary>
    public float velocity = 5f;

    /// <summary>
    /// �ړ��\�t���O
    /// </summary>
    public int enable_move = 0;

    /// <summary>
    /// �V���b�g�\�t���O
    /// </summary>
    public int enable_shoot = 0;

    /// <summary>
    /// �A�N�V�����\�t���O
    /// </summary>
    public int enable_action = 0;

    /// <summary>
    /// �ړ�����
    /// </summary>
    private Vector3 _move_direction;

    /// <summary>
    /// �F�؂�ւ��\��
    /// </summary>
    protected bool _enable_color_change = true;
    public bool enableColorChange
    {
        get { return _enable_color_change; }
        set { _enable_color_change = value; }
    }

    /// <summary>
    /// �F�؂�ւ����̃A�N�V����
    /// </summary>
    public System.Action<GameDifinition.eColor> color_change_action = null;

    /// <summary>
    /// ���݂̐F
    /// </summary>
    private GameDifinition.eColor _current_color;
    public GameDifinition.eColor currentColor
    {
        get { return _current_color; }
        set {
            _current_color = value;

            // �F�؂�ւ���̃R�[���o�b�N
            color_change_action?.Invoke(currentColor);
        }
    }

    /// <summary>
    /// ���݂̕���
    /// </summary>
    private GameDifinition.eAction _current_weapon;
    public GameDifinition.eAction currentWeapon
    {
        get
        {
            return _current_weapon;
        }
        set
        {
            _current_weapon = value;
        }
    }

    public void Start()
    {
        Setup();
    }

    public virtual void Setup()
    {
        rb = GetComponent<Rigidbody>();
        SetColor(GameDifinition.eColor.Cyan);
    }

    public void Update()
    {
        _Update();
    }

    protected virtual void _Update(){ }

    /// <summary>
    /// �ړ�
    /// </summary>
    public void Move(Vector2 direction_2d)
    {
        // �ړ��s�t���O
        if(enable_move > 0)
        {
            return;
        }

        // �ړ�����
        _move_direction = new Vector3(direction_2d.x, 0, direction_2d.y);
        rb.velocity = _move_direction * velocity;

        if(direction_2d.magnitude < 0.1f)
        {
            return;
        }

        // ��]����
        var tmp_dir = (direction_2d.y >= 0 ? 
             Mathf.Lerp(-90, 90, (direction_2d.x + 1.0f) / 2.0f) :
             direction_2d.x >= 0 ?
             Mathf.Lerp(90, 180, 1 - (direction_2d.x)) :
             Mathf.Lerp(-90, -180, -(direction_2d.x)));
        
        transform.rotation = Quaternion.Euler(new Vector3(0, tmp_dir, 0));
    }

    /// <summary>
    /// ���ڐF�w��
    /// </summary>
    /// <param name="set_color"></param>
    public void SetColor(GameDifinition.eColor set_color)
    {
        _current_color = set_color;
        color_change_action?.Invoke(_current_color);
    }

    /// <summary>
    /// �F�`�F���W
    /// </summary>
    /// <param name="value"></param>
    public void ChangeColor(bool value)
    {
        // �؂�ւ���̐F���擾
        var after_color = GameDifinition.GetChangeColor(currentColor);
        currentColor = after_color;

        // �F�؂�ւ���̃R�[���o�b�N
        color_change_action?.Invoke(currentColor);
    }

    /// <summary>
    /// �����e����
    /// </summary>
    public virtual void Shoot(){ if (enable_shoot > 0) return; }

    /// <summary>
    /// ����A�N�V�������s
    /// </summary>
    public virtual void ActionEx() { if (enable_action > 0) return; }

    /// <summary>
    /// ���ꂽ
    /// </summary>
    public virtual void Failed() {}
}
