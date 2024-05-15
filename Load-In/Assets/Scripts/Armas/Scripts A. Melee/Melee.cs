using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;

    private bool isAttacking;

    private GameObject alabarda;

    public AudioClip hachazoo;

    private float escalaOriginalX;
    public string NombreAnimación;
    void Start()
    {
        alabarda = GameObject.Find("Alabarda");
        escalaOriginalX = transform.localScale.x;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (isAttacking == false)
            {
             isAttacking = true;
             anim.Play(NombreAnimación);
                ControladorSonido.Instance.EjecutarSonido(hachazoo);
            }
        }

        if(this.gameObject != alabarda)
        {
            float angulo = transform.rotation.eulerAngles.z;
            if (angulo < 180f)
            {
                transform.localScale = new Vector3(escalaOriginalX * -1f, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(escalaOriginalX, transform.localScale.y, transform.localScale.z);
            }

        }
    }

    public void isAttackingFalse()
    {
        isAttacking = false;
    }
}
