using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : WeaponBase
{
    /// <summary>
    /// åªç›ÇÃêF
    /// </summary>
    private GameDifinition.eColor _color = GameDifinition.eColor.White;
    public GameDifinition.eColor currentColor
    {
        get { return _color; }
    }

    public void Setup(GameDifinition.eColor set_color)
    {
        _color = set_color;
    }

    private void Start()
    {
        GetComponent<Renderer>().material.color = GameDifinition.GetRGBColor(_color);
    }
}
