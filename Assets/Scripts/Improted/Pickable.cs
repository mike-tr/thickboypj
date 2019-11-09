using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private static List<Pickable> items = new List<Pickable>();

    private void Start() {
        items.Add(this);
    }

    public void PickUp() {
        items.Remove(this);
        Destroy(this.gameObject);
    }

    public static Pickable GetItem(Vector2 origin ,float distance) {
        foreach(var item in items) {
            if (Vector2.Distance(item.transform.position, origin) < distance)
                return item;
        }
        return null;
    }

    public Item item;
}
