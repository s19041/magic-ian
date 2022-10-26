using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject text;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void ChestOpened()
    {
        animator.SetBool("Over", true);
    }

    public void AnimateChest()
    {
        animator.SetBool("Clicked", true);
    }

    public void ShowContent()
    {
        text.SetActive(true);
    }

    public void OnUnlockNextUnlockableButton()
    {
        PlayerManager.Instance.UnlockNextUnlockables();
    }
}