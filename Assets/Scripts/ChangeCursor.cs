using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] Transform cursor;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 curPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = curPos;
    }
}
