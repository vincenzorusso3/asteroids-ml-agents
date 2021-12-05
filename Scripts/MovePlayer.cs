﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float RotateSpeed = 150f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.down * RotateSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
    }
}
