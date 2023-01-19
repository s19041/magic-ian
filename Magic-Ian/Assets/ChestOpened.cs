using System.Collections;
using UnityEngine;

public class ChestOpened : MonoBehaviour
{
    // Update is called once per frame
    public void NextScene()
    {
        StartCoroutine(NextScene(3f));
    }

    public IEnumerator NextScene(float delayInSecs)
    {
        yield return new WaitForSeconds(delayInSecs);
        DungeonManager.Instance.EnableNextSceneButton();
    }
}