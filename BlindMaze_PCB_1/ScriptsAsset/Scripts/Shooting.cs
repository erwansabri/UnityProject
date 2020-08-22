using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Shooting : MonoBehaviour
{
    public GameObject firePoint;
    public LineRenderer lineRenderer;
    public Light2D pointLight;
    public HealthBar hpBar;
    public GameObject ammo;


    private void Start()
    {
        lineRenderer.enabled = false;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (hpBar.ammoSlider.value != 0)
            {
                StartCoroutine(Shoot());
                hpBar.ModifAmmo(-1);
            }
            else
            {
                Debug.Log("No ammo");
            }
        }
        
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo;
        Vector3 side;
        if(firePoint.transform.localPosition.x < 0)
        {
            hitInfo = Physics2D.Raycast(firePoint.transform.position, firePoint.transform.right*-1);
            side = Vector3.left;
        }
        else if(firePoint.transform.localPosition.x > 0)
        {
            hitInfo = Physics2D.Raycast(firePoint.transform.position, firePoint.transform.right);
            side = Vector3.right;

        }
        else if(firePoint.transform.localPosition.y == 0.4f)
        {
            hitInfo = Physics2D.Raycast(firePoint.transform.position, firePoint.transform.up);
            side = Vector3.up;

        }
        else
        {
            hitInfo = Physics2D.Raycast(firePoint.transform.position, firePoint.transform.up*-1);
            side = Vector3.down;

        }
        pointLight.pointLightInnerAngle = 0;
        if (hitInfo)
        {
            Debug.Log("Truc touché :" + hitInfo.transform.tag);
            if(hitInfo.transform.tag == "Enemy")
            {
                Destroy(hitInfo.transform.gameObject);
                if(Random.value < .4f) Instantiate(ammo, hitInfo.transform.position, Quaternion.identity);
            }
            lineRenderer.SetPosition(0, firePoint.transform.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.transform.position);
            lineRenderer.SetPosition(1, firePoint.transform.position + side * 100);

        }
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.05f);
        lineRenderer.enabled = false;

        pointLight.pointLightInnerAngle = 96;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo"))
        {
            hpBar.SetAmmo(3);
            Destroy(collision.gameObject);
        }
    }
}
