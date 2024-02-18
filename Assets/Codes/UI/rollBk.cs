using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class rollBk : MonoBehaviour
{
    private Material mat;
    [Header("參數")]
    public Vector2 moveSpd;
    private Vector2 offset;
    private void Awake()
    {
        mat = GetComponent<TilemapRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = moveSpd * Time.deltaTime;
        mat.mainTextureOffset += offset;
    }
}

