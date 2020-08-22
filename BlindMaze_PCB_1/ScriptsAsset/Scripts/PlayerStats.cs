using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public HealthBar hpBar;
    public bool isAlive = true;

    void Update()
    {
        if (hpBar.healthSlider.value == 0)
            isAlive = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hpBar.ModifHealth(-10);
        }
    }
}
