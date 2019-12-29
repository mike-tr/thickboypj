using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPartType {
    body,
    hand_l,
    hand_r,
}

public class BodyPart : MonoBehaviour
{
    public IgnoreCollision ignore;

    public BodyPartType type;

    [HideInInspector]
    public EquipManager manager;
    private Equipable item;

    public void Start() {
        ignore = GetComponentInParent<IgnoreCollision>();
    }

    public void Equip(Equipable new_item) {
        // equip the item
        Debug.Log("equiping - " + new_item.name);

        if (item) {
            manager.strength -= item.strength;
            manager.mana -= item.mana;
            Destroy(item.gameObject);
        }

        item = Instantiate(new_item);
        Vector3 rot = item.transform.localEulerAngles;
        item.transform.SetParent(transform);
        item.transform.localPosition = item.transform.position;
        item.transform.localEulerAngles = rot;

        foreach (var coll in item.GetComponentsInChildren<Collider2D>()) {
            ignore.IgnoreCollider(coll);
            
        }
        var collider = item.GetComponent<Collider2D>();
        if(collider)
            ignore.IgnoreCollider(collider);

        manager.strength += item.strength;
        manager.mana += item.mana;
    }
}
