using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    // Start is called before the first frame update

    Dictionary<int, List<Collider2D>> igList = new Dictionary<int, List<Collider2D>>();

    Collider2D[] colliders;
    void Start()
    {
        colliders = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++) {
            for (int k = i+1; k < colliders.Length; k++) {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }
    }

    public void AddIgnoreList(int id, Collider2D[] colliders) {
        foreach (var collider in colliders) {
            AddToIgnoreList(id, collider);
        }
    }

    public void AddIgnoreList(int id,List<Collider2D> colliders) {
        foreach (var collider in colliders) {
            AddToIgnoreList(id, collider);
        }
    }

    public void AddToIgnoreList(int id, Collider2D collider) {
        if(igList.TryGetValue(id, out var list)) {
            list.Add(collider);
            IgnoreCollider(collider);
        } else {
            list = new List<Collider2D>();
            list.Add(collider);
            igList.Add(id, list);
            IgnoreCollider(collider);
        }
    }

    public void EnableColliders(int id) {
        StartCoroutine(EnableDelay(id, 1f));
    }

    private void FinalEnable(int id) {
        if (igList.TryGetValue(id, out var list)) {
            foreach (var coll in list) {
                IgnoreCollider(coll, false);
            }
            igList.Remove(id);
        }
    }

    public void IgnoreCollider(Collider2D collider, bool ignore = true) {
        for (int i = 0; i < colliders.Length; i++) {
            Physics2D.IgnoreCollision(colliders[i], collider, ignore);
        }
    }

    IEnumerator EnableDelay(int id, float time) {
        yield return new WaitForSeconds(time);
        FinalEnable(id);
    }


}
