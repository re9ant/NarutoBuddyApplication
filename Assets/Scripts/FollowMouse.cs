using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector3 mOffset;
    private float zPos;
    bool follow = true;

    private void Start()
    {
        zPos = transform.position.z;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            follow = !follow;
        }
        if (!follow) return;
        zPos = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        transform.position = GetMouseAsWorldPoint()/* + mOffset*/;
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zPos;
        Debug.Log(Camera.main.ScreenToWorldPoint(mousePoint));
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}