using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;
    public Slider ammoSlider;

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }


    public void ModifHealth(int amount)
    {
        healthSlider.value += amount;
    }

    public void ModifAmmo(int amount)
    {
        ammoSlider.value += amount;
    }

    public void SetAmmo(int ammo)
    {
        ammoSlider.value = ammo;
    }


}
