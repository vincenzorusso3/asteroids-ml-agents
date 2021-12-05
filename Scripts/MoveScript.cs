using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}