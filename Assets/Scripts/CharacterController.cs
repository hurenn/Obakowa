using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// 操作対象キャラクター
    /// </summary>
    public CharacterBase character;

    /// <summary>
    /// 移動入力
    /// </summary>
    [SerializeField]
    private InputAction _move;
    /// <summary>
    /// 色切り替え入力
    /// </summary>
    [SerializeField]
    private InputAction _switch;
    /// <summary>
    /// 光線銃発射入力
    /// </summary>
    [SerializeField]
    private InputAction _shoot;
    /// <summary>
    /// 特殊アクション
    /// </summary>
    [SerializeField]
    private InputAction _action;
    /// <summary>
    /// オプション
    /// </summary>
    [SerializeField]
    private InputAction _option;

    /// <summary>
    /// 現在の移動入力
    /// </summary>
    public Vector2 current_move;   
    /// <summary>
    /// 現在の切り替え入力
    /// </summary>
    public bool current_switch;
    /// <summary>
    /// 現在の光線銃入力
    /// </summary>
    public bool current_shoot;
    /// <summary>
    /// 現在の特殊アクション入力
    /// </summary>
    public bool current_action;
    /// <summary>
    /// 現在のオプション入力
    /// </summary>
    public bool current_option;

    private void Start()
    {
        _Setup();
    }

    private void _Setup()
    {
        // NOTE:この時点でキャラクターがアタッチされていることを前提
        if(character != null)
        {
            return;
        }

        character.currentColor = GameDifinition.eColor.Cyan;
        character.currentWeapon = GameDifinition.eAction.Camera;
    }

    private void OnEnable()
    {
        // 有効化
        _move?.Enable();
        _switch?.Enable();
        _shoot?.Enable();
        _action?.Enable();
        _option?.Enable();

        _switch.performed += OnColorChange;
        _shoot.performed += OnShoot;
        _action.performed += OnActionEx;
    }

    public void Update()
    {
        InputController();

        if(character == null)
        {
            return;
        }

        character.Move(current_move);
    }

    public void InputController()
    {
        // 現在のキーボード情報
        var current = Keyboard.current;

        // キーボード接続チェック
        if (current == null)
        {
            Debug.LogError("キーボードが接続されていません");
            return;
        }

        // 方向入力
        if (_move != null)
        {
            current_move = _move.ReadValue<Vector2>();
        }

        // オプション入力
        if(_option != null)
        {
            current_option = _option.ReadValue<float>() > 0.5f ? true : false;
        }
    }

    private void OnColorChange(InputAction.CallbackContext context)
    {
        current_switch = context.ReadValue<float>() > 0.5f ? true : false;
        character.ChangeColor(current_switch);
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        current_shoot = _shoot.ReadValue<float>() > 0.5f ? true : false;
        if (!current_shoot)
        {
            return;
        }

        character.Shoot();
    }

    private void OnActionEx(InputAction.CallbackContext context)
    {
        current_action = _action.ReadValue<float>() > 0.5f ? true : false;
        if (!current_action)
        {
            return;
        }

        character.ActionEx();
    }

    private void OnDisable()
    {
        // 無効化
        _move?.Disable();
        _switch?.Disable();
        _shoot?.Disable();
        _action?.Disable();
        _option?.Disable();

        _switch.performed -= OnColorChange;
        _shoot.performed -= OnShoot;
        _action.performed -= OnActionEx;
    }
}
