using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// ����ΏۃL�����N�^�[
    /// </summary>
    public CharacterBase character;

    /// <summary>
    /// �ړ�����
    /// </summary>
    [SerializeField]
    private InputAction _move;
    /// <summary>
    /// �F�؂�ւ�����
    /// </summary>
    [SerializeField]
    private InputAction _switch;
    /// <summary>
    /// �����e���˓���
    /// </summary>
    [SerializeField]
    private InputAction _shoot;
    /// <summary>
    /// ����A�N�V����
    /// </summary>
    [SerializeField]
    private InputAction _action;
    /// <summary>
    /// �I�v�V����
    /// </summary>
    [SerializeField]
    private InputAction _option;

    /// <summary>
    /// ���݂̈ړ�����
    /// </summary>
    public Vector2 current_move;   
    /// <summary>
    /// ���݂̐؂�ւ�����
    /// </summary>
    public bool current_switch;
    /// <summary>
    /// ���݂̌����e����
    /// </summary>
    public bool current_shoot;
    /// <summary>
    /// ���݂̓���A�N�V��������
    /// </summary>
    public bool current_action;
    /// <summary>
    /// ���݂̃I�v�V��������
    /// </summary>
    public bool current_option;

    private void Start()
    {
        _Setup();
    }

    private void _Setup()
    {
        // NOTE:���̎��_�ŃL�����N�^�[���A�^�b�`����Ă��邱�Ƃ�O��
        if(character != null)
        {
            return;
        }

        character.currentColor = GameDifinition.eColor.Cyan;
        character.currentWeapon = GameDifinition.eAction.Camera;
    }

    private void OnEnable()
    {
        // �L����
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
        // ���݂̃L�[�{�[�h���
        var current = Keyboard.current;

        // �L�[�{�[�h�ڑ��`�F�b�N
        if (current == null)
        {
            Debug.LogError("�L�[�{�[�h���ڑ�����Ă��܂���");
            return;
        }

        // ��������
        if (_move != null)
        {
            current_move = _move.ReadValue<Vector2>();
        }

        // �I�v�V��������
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
        // ������
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
