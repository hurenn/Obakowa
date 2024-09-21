using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKid : CharacterBase
{
    /// <summary>
    /// �e�N���X
    /// </summary>
    [SerializeField]
    private GameObject _shoot_obj;

    /// <summary>
    /// �J�����B�e�͈̓N���X
    /// </summary>
    [SerializeField]
    private GameObject _photograph_obj;

    /// <summary>
    /// �B�e�͈͔�������
    /// </summary>
    [SerializeField]
    private float _photograph_distance = 5;

    public override void Setup()
    {
        base.Setup();

        // ����ǂݍ���
        //_shoot_obj = (GameObject)Resources.Load(GameDifinition.SHOOT_PATH);
        //_photograph_obj = (GameObject)Resources.Load(GameDifinition.PHOTOGRAPH_PATH);
    }

    public override void Shoot()
    {
        base.Shoot();

        if (_shoot_obj == null || enable_shoot > 0)
        {
            return;
        }

        var obj = Instantiate(_shoot_obj, transform.position, transform.rotation);
        obj.GetComponent<Shoot>().Setup(currentColor);
    }

    public override void ActionEx()
    {
        base.ActionEx();
        if(enable_action > 0)
        {
            return;
        }

        var obj = Instantiate(_photograph_obj, transform.position + transform.forward * _photograph_distance, transform.rotation);
    }

    public override void Failed()
    {
        enable_shoot++;
        enable_move++;
        enable_action++;

        var rend = GetComponent<Renderer>();
        rend.material.color = Color.gray;
    }
}
