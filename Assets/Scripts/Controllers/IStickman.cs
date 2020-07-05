using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IStickman {
    protected IController controller;
    public abstract void Update ();
    public virtual void LateUpdate () { }
    public bool IsAlive () {
        return controller.IsAlive ();
    }
    public IController getController () {
        return controller;
    }
}
