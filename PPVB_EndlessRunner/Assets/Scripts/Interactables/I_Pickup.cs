using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Pickup
{
    void PickedUp(ItemHandler player);
    void Use();
}
