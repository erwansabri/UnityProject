using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    public Animator anim;
    public int idle;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;
    float m_CurrentClipLength;

    public Light2D plight;
    float smooth = 30.0f;

    public GameObject firePoint;
    private Vector3 firePointpos;

    public bool debugs;

    public bool isDiscret = false;

    public bool hasObjectif = false;
    public bool isLevelDone = false;


    // Update is called once per frame
    void Update()
    {
        //Hiding
        if (Input.GetButton("Fire2"))
        {
            isDiscret = true;
            //A changer
            //globalLight.enabled = false;
        }
        else
        {
            //A changer
            //globalLight.enabled = true;
            isDiscret = false;
        }
        PlayAnimationDiscret();

        //Movement + Animations
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        m_ClipName = m_CurrentClipInfo[0].clip.name;

        if (m_ClipName == "bot")
        {
            plight.transform.localPosition = new Vector3(0, 0.3f, 0);
            firePoint.transform.localPosition = new Vector3(0, 0.3f, 0);
            LightRotation(-180);
            idle = 1;
        }
        if (m_ClipName == "top")
        {
            plight.transform.localPosition = new Vector3(0, 0.4f, 0);
            firePoint.transform.localPosition = new Vector3(0, 0.4f, 0);
            LightRotation(0);
            idle = 2;
        }
        if (m_ClipName == "left")
        {
            plight.transform.localPosition = new Vector3(-0.1f, 0.3f, 0);
            firePoint.transform.localPosition = new Vector3(-0.1f, 0.3f, 0);
            LightRotation(90);
            idle = 3;
        }
        if (m_ClipName == "right")
        {
            plight.transform.localPosition = new Vector3(0.1f, 0.3f, 0);
            firePoint.transform.localPosition = new Vector3(0.1f, 0.3f, 0);
            LightRotation(-90);
            idle = 4;
        }
        anim.SetInteger("Idle", idle);
        anim.SetFloat("Speed", movement.sqrMagnitude);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Horizontal", movement.x);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void LightRotation(int angle)
    {
        Quaternion target = Quaternion.Euler(0, 0, angle);
        plight.transform.rotation = Quaternion.Slerp(plight.transform.rotation, target, Time.deltaTime * smooth);
    }

    private void PlayAnimationDiscret()
    {
        plight.enabled = !isDiscret;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Objectif"))
        {
            Destroy(collision.gameObject);
            hasObjectif = true;
        }
        else if (collision.gameObject.CompareTag("Objectifreturn") && hasObjectif == true)
        {
            isLevelDone = true;
            Debug.Log("Fini");
        }
    }
}
