using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    right = 1,
    none = 0,
    left = -1
}

public class Equipable : Item {
    public BodyPartType type;
    public Direction direction;
    public EquipableBodyPart parent;
}