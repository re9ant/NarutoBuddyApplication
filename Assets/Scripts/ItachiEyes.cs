using System.Collections;
using UnityEngine;

public class ItachiEyes : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    Animator animator;
    int openHash;

    private void Start()
    {
        animator = GetComponent<Animator>();
        openHash = Animator.StringToHash("Open");
        OpenEyes();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                OpenEyes();
            }
        }
    }

    void OpenEyes()
    {
        animator.SetTrigger(openHash);
        StartCoroutine(resetTrigger());
    }

    IEnumerator resetTrigger()
    {
        yield return new WaitForSeconds(1.0f);
        animator.ResetTrigger(openHash);
    }

    public void playAudio()
    {
        audioSource.Play();
    }
}
