using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbPart
{
    private Transform transform;
    public LimbPart(Transform transform) {
        this.transform = transform;
    }

    public float GetAngleFromDir(Vector2 dir) {
        dir = dir.normalized;
        var angle = Mathf.Acos(dir.x) * Mathf.Rad2Deg;
        return dir.y > 0 ? angle : 360 - angle;
    }
}
