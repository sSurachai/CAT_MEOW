using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFood_SG : MonoBehaviour
{
    public int points = 10;

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager_SG>().NormalFoodEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cat"))
        {
            Eat();
        }
    }
}
