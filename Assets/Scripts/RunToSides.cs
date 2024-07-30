using System;
using UnityEngine;
using CodeMonkey.Utils;

public class RunToSides : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] Transform left;
    [SerializeField] SpriteRenderer spriteRenderer;

    Button_Sprite button;
    Animator animator;

    Vector3 right;
    Vector3 targetPos;

    bool run = false;
    bool isOnRight = true;
    
    void Start()
    {
        button = GetComponent<Button_Sprite>();
        animator = GetComponent<Animator>();
        spriteRenderer = (spriteRenderer == null) ? GetComponent<SpriteRenderer>() : spriteRenderer;
        right = transform.position;
        button.ClickFunc = () => Run();
    }

    void Update()
    {
        if (run)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, targetPos) < 0.15f)
            {
                try
                {
                    animator.SetBool("Run", false);
                }
                catch
                {
                    Debug.LogWarning("No Run Animation");
                }
                transform.position = isOnRight ? left.position : right;
                isOnRight = !isOnRight;
                spriteRenderer.flipX = isOnRight ? false : true;
                run = false;
            }
        }
    }

    void Run()
    {
        if (run) return;
        run = true;
        try
        {
            animator.SetBool("Run", true);
        }
        catch(Exception e)
        {
            Debug.Log("No running ainmation to the current sprite :" + e);
        }
        finally
        {
            targetPos = isOnRight ? left.position : right;
        }
    }
}
