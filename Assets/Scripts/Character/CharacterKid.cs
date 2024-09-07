using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKid : CharacterBase
{
    /// <summary>
    /// ’eƒNƒ‰ƒX
    /// </summary>
    private GameObject _shoot_obj;

    /// <summary>
    /// ƒJƒƒ‰B‰e”ÍˆÍƒNƒ‰ƒX
    /// </summary>
    private GameObject _photograph_obj;

    /// <summary>
    /// B‰e”ÍˆÍ”­¶‹——£
    /// </summary>
    [SerializeField]
    private float _photograph_distance = 5;

    public override void Setup()
    {
        base.Setup();

        // •Ší“Ç‚İ‚İ
        _shoot_obj = (GameObject)Resources.Load(GameDifinition.SHOOT_PATH);
        _photograph_obj = (GameObject)Resources.Load(GameDifinition.PHOTOGRAPH_PATH);
    }

    public override void Shoot()
    {
        base.Shoot();

        if (_shoot_obj == null)
        {
            return;
        }

        var obj = Instantiate(_shoot_obj, transform.position, transform.rotation);
        obj.GetComponent<Shoot>().Setup(currentColor);
    }

    public override void ActionEx()
    {
        base.ActionEx();

        var obj = Instantiate(_photograph_obj, transform.position + transform.forward * _photograph_distance, transform.rotation);
    }
}
