using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBackgroundScroll : MonoBehaviour
{
    private Material material;

    [SerializeField]
    private Vector2 offset;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
