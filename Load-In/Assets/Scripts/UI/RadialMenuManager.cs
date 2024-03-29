using UnityEngine;

public class RadialMenuManager : MonoBehaviour
{
    public GameObject radialMenuRoot;

    bool isRadialMenuActive;

    void Start()
    {
        isRadialMenuActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRadialMenuActive = !isRadialMenuActive;
            radialMenuRoot.SetActive(isRadialMenuActive);
            Time.timeScale = 0f;
        }
    }
}
