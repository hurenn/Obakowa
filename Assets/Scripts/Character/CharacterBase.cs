using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    /// <summary>
    /// 物理演算
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// 移動スピード
    /// </summary>
    public float velocity = 5f;

    /// <summary>
    /// 移動可能フラグ
    /// </summary>
    public int enable_move = 0;

    /// <summary>
    /// ショット可能フラグ
    /// </summary>
    public int enable_shoot = 0;

    /// <summary>
    /// アクション可能フラグ
    /// </summary>
    public int enable_action = 0;

    /// <summary>
    /// 移動方向
    /// </summary>
    private Vector3 _move_direction;

    /// <summary>
    /// 色切り替え可能か
    /// </summary>
    protected bool _enable_color_change = true;
    public bool enableColorChange
    {
        get { return _enable_color_change; }
        set { _enable_color_change = value; }
    }

    /// <summary>
    /// 色切り替え時のアクション
    /// </summary>
    public System.Action<GameDifinition.eColor> color_change_action = null;

    /// <summary>
    /// 現在の色
    /// </summary>
    private GameDifinition.eColor _current_color;
    public GameDifinition.eColor currentColor
    {
        get { return _current_color; }
        set {
            _current_color = value;

            // 色切り替え後のコールバック
            color_change_action?.Invoke(currentColor);
        }
    }

    /// <summary>
    /// 現在の武器
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
    /// 移動
    /// </summary>
    public void Move(Vector2 direction_2d)
    {
        // 移動不可フラグ
        if(enable_move > 0)
        {
            return;
        }

        // 移動制御
        _move_direction = new Vector3(direction_2d.x, 0, direction_2d.y);
        rb.velocity = _move_direction * velocity;

        if(direction_2d.magnitude < 0.1f)
        {
            return;
        }

        // 回転制御
        var tmp_dir = (direction_2d.y >= 0 ? 
             Mathf.Lerp(-90, 90, (direction_2d.x + 1.0f) / 2.0f) :
             direction_2d.x >= 0 ?
             Mathf.Lerp(90, 180, 1 - (direction_2d.x)) :
             Mathf.Lerp(-90, -180, -(direction_2d.x)));
        
        transform.rotation = Quaternion.Euler(new Vector3(0, tmp_dir, 0));
    }

    /// <summary>
    /// 直接色指定
    /// </summary>
    /// <param name="set_color"></param>
    public void SetColor(GameDifinition.eColor set_color)
    {
        _current_color = set_color;
        color_change_action?.Invoke(_current_color);
    }

    /// <summary>
    /// 色チェンジ
    /// </summary>
    /// <param name="value"></param>
    public void ChangeColor(bool value)
    {
        // 切り替え後の色を取得
        var after_color = GameDifinition.GetChangeColor(currentColor);
        currentColor = after_color;

        // 色切り替え後のコールバック
        color_change_action?.Invoke(currentColor);
    }

    /// <summary>
    /// 光線銃発射
    /// </summary>
    public virtual void Shoot(){ if (enable_shoot > 0) return; }

    /// <summary>
    /// 特殊アクション実行
    /// </summary>
    public virtual void ActionEx() { if (enable_action > 0) return; }

    /// <summary>
    /// やられた
    /// </summary>
    public virtual void Failed() {}
}
