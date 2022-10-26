using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpened : MonoBehaviour
{


    // Update is called once per frame
    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3);
        DungeonManager.Instance.EnableNextSceneButton();
    }
}
