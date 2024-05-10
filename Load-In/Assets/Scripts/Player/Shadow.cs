using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public static Shadow me;
    public GameObject sombra;
    public List <GameObject> pool = new List<GameObject>();
    private float cronometro;
    public float speed;
    public Color _Color;

    public void Awake()
    {
        me = this;
    }

    public GameObject GetShadows()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                pool[i].transform.position = transform.position;
                pool[i].transform.rotation = transform.rotation;
                pool[i].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                pool[i].GetComponent<DashSombra>().mycolor = _Color;    
                return pool[i];
            }
        }
        GameObject obj = Instantiate(sombra, transform.position, transform.rotation) as GameObject;
        obj.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        obj.GetComponent<DashSombra>().mycolor= _Color;
        pool.Add(obj);
        return obj; 
    }

    public void Sombras_Skill()
    {
        cronometro += speed * Time.deltaTime;
        if(cronometro > 1)
        {
            GetShadows();
            cronometro = 0;
        }
    }
}
