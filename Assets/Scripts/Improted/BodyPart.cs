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
    public BodyPartType type;

    [HideInInspector]
    public EquipManager manager;
    private Equipable item;

    public void Equip(Equipable new_item) {
        // equip the item
        Debug.Log("equiping - " + new_item.name);

        if (item) {
            manager.strength -= item.strength;
            manager.mana -= item.mana;
            Destroy(item.gameObject);
        }

        item = Instantiate(new_item);
        item.transform.SetParent(transform);
        item.transform.localPosition = item.equip_position;
        item.transform.localEulerAngles = Vector3.zero;

        manager.strength += item.strength;
        manager.mana += item.mana;
    }
}
