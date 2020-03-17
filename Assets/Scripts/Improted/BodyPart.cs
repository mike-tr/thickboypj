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

    public bool flip = false;
    public Transform origin;

    public void Start() {
        ignore = GetComponentInParent<IgnoreCollision>();
        if (flip) {
            StartCoroutine(Flipper());
        }
    }

    IEnumerator Flipper() {
        var flipper = new Vector2(1, -1);
        void flipr() {
            var rot = item.transform.localEulerAngles;
            rot.x += 180;
            item.transform.localEulerAngles = rot;
            item.transform.localPosition *= flipper;
        }

        while (true) {
            yield return new WaitForSeconds(0.1f);
            if (item && item.direction != Direction.none) {
                if ((transform.position.x - origin.position.x) < 0) {
                    if (item.direction == Direction.right) {
                        flipr();
                        item.direction = Direction.left;
                    }
                } else if (item.direction == Direction.left) {
                    flipr();
                    item.direction = Direction.right;
                }
            }
        }
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
