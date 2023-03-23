using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dango_SG : NormalFood_SG
{
    public int duration = 3;

    protected override void Eat()
    {
        FindObjectOfType<GameManager_SG>().DangoEaten(this);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cat"))
        {
            Eat();
        }
    }
}
