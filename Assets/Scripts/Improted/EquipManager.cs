using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    private Dictionary<BodyPartType, BodyPart> bodyParts = new Dictionary<BodyPartType, BodyPart>();
    public List<Item> inventory = new List<Item>();

    public Equipable starting_item;

    public float strength = 10;
    public float mana = 10;

    // Start is called before the first frame update
    void Start()
    {
        BodyPart[] get_parts = transform.GetComponentsInChildren<BodyPart>();
        foreach(var part in get_parts) {
            bodyParts.Add(part.type, part);
            part.manager = this;
        }

        EquipItem(starting_item);
    }

    void Pickup(Pickable pick) {
        if (!pick)
            return;
        Debug.Log(pick.name);
        var item = pick.item;
        if(item.GetType() == typeof(Equipable)) {
            EquipItem((Equipable)item);
        } else {
            inventory.Add(item);
        }
        pick.PickUp();
        // pick an item
    }

    void EquipItem(Equipable item) {
        if (!item)
            return;
        if(bodyParts.TryGetValue(item.type, out var part)) {
            part.Equip(item);
        }
        // equip the item
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            Pickup(Pickable.GetItem(transform.position, 10));
            // pickup items!
        }
    }
}
