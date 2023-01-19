using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

using UnityEngine.SceneManagement;

public class CardDescButton : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isActive;

    public GameObject description;

    private void Start()
    {
        isActive = description.activeInHierarchy;
    }

    public void Use()
    {
        if (isActive)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }

    public void Activate()
    {
        description.SetActive(true);
        isActive = true;
    }

    public void Deactivate()
    {
        description.SetActive(false);
        isActive = false;
    }
}