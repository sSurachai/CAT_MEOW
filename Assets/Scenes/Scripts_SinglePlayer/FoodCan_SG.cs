using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCan_SG : NormalFood
{
    protected override void Eat()
    {
        FindObjectOfType<GameManager_SG>().FoodCanEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cat"))
        {
            Eat();
        }
    }
}
