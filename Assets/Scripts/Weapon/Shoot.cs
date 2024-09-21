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
        var pos = transform.position;
        pos.z += 3;
        transform.position = pos;
    }

    private void Start()
    {
        var rend = GetComponent<Renderer>();
        if (rend)
        {
            rend.material.color = GameDifinition.GetRGBColor(_color);
        }

        var sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite)
        {
            sprite.color = GameDifinition.GetRGBColor(_color);
        }
    }
}
