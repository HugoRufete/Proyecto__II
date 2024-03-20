using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    void Update()
    {
        Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionRaton.z = 0; 

        ApuntarAlRaton(posicionRaton);
    }

    void ApuntarAlRaton(Vector3 objetivo)
    {
        Vector3 direccion = objetivo - transform.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
    }
}
