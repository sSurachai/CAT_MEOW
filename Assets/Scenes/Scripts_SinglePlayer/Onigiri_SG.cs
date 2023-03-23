using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onigiri_SG : NormalFood
{
    protected override void Eat()
    {
        FindObjectOfType<GameManager_SG>().OnigiriEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cat"))
        {
            Eat();
        }
    }
}
