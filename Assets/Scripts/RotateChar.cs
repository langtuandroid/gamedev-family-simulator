using UnityEngine;

public class RotateChar : MonoBehaviour
{

    public float speed;

    private void OnMouseDrag()
    {
        float rotx = Input.GetAxis("Mouse X") * speed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up ,-rotx);
    }
}
