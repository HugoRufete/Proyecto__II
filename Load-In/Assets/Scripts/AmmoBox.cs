using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AmmoBox : MonoBehaviour
{
    private List<IRecargable> _recargables;

    private Animator anim;

    private void Start()
    {
        GameObject targetObject = GameObject.FindWithTag("PopUps");
        anim = targetObject.GetComponent<Animator>();

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.Play("AmmunitionReloadedAnimation");
            FindAndAssignRecargables(collision.gameObject.transform);
            foreach (var recargable in _recargables)
            {
                recargable.RecargarArma();
            }

            Destroy(gameObject);
        }
    }

    private void FindAndAssignRecargables(Transform root)
    {
        List<GameObject> gameObjects = new List<GameObject>(FindObjectsOfType<GameObject>(true));

        _recargables = new List<IRecargable>();

        foreach (var go in gameObjects)
        {
            IRecargable recargable = go.GetComponent<IRecargable>();

            if (recargable != null)
            {
                _recargables.Add(recargable);
            }
        }

        if (_recargables.Count == 0 && root == null)
        {
            Debug.LogError("No se encontraron armas recargables en la escena.");
        }
    }

    private IEnumerator DestroyObjectCoroutine(GameObject objetoADestruir, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoADestruir != null)
        {
            objetoADestruir.SetActive(false);
        }
    }
}