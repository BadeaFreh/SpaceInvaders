using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer mesh_renderer;
    
    private float scroll_speed = 0.1f;
    private float x_scroll; // x axis scroll

    void Awake()
    {
        mesh_renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        scroll(); // adding 0.1 to the offset every second (more less)
    }

    void scroll()
    {
        x_scroll = Time.time * scroll_speed; // time is in seconds
        Vector2 offset = new Vector2(x_scroll, 0f); // zero for the y axis
        mesh_renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

}
