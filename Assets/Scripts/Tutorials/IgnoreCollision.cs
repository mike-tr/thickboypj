using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {
    // Start is called before the first frame update

    Dictionary<int, List<Collider2D>> igList = new Dictionary<int, List<Collider2D>> ();

    List<Collider2D> colliders = new List<Collider2D> ();
    void Start () {
        foreach (var coll in GetComponentsInChildren<Collider2D> ()) {
            colliders.Add (coll);
        }
        for (int i = 0; i < colliders.Count; i++) {
            for (int k = i + 1; k < colliders.Count; k++) {
                Physics2D.IgnoreCollision (colliders[i], colliders[k]);
            }
        }
    }

    public void AddIgnoreList (int id, Collider2D[] colliders) {
        foreach (var collider in colliders) {
            AddToIgnoreList (id, collider);
        }
    }

    public void AddIgnoreList (int id, List<Collider2D> colliders) {
        foreach (var collider in colliders) {
            AddToIgnoreList (id, collider);
        }
    }

    public void AddToIgnoreList (int id, Collider2D collider) {
        if (igList.TryGetValue (id, out var list)) {
            list.Add (collider);
            IgnoreForigineCollider (collider);
        } else {
            list = new List<Collider2D> ();
            list.Add (collider);
            igList.Add (id, list);
            IgnoreForigineCollider (collider);
        }
    }

    public void EnableColliders (int id) {
        StartCoroutine (EnableDelay (id, 0.5f));
    }

    private void FinalEnable (int id) {
        if (igList.TryGetValue (id, out var list)) {
            foreach (var coll in list) {
                IgnoreForigineCollider (coll, false);
            }
            igList.Remove (id);
        }
    }

    public void IgnoreForigineCollider (Collider2D collider, bool ignore = true) {
        for (int i = 0; i < colliders.Count; i++) {
            Physics2D.IgnoreCollision (colliders[i], collider, ignore);
        }
    }

    public void IgnoreCollider (Collider2D collider, bool ignore = true) {
        IgnoreForigineCollider (collider, ignore);
        if (colliders.Contains (collider)) {
            if (!ignore)
                colliders.Remove (collider);
        } else if (ignore)
            colliders.Add (collider);
    }

    IEnumerator EnableDelay (int id, float time) {
        yield return new WaitForSeconds (time);
        FinalEnable (id);
    }

}