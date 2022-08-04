using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackScroller : MonoBehaviour
{
    private float scrollSpeed = 0.05f;
    public bool invert;
    public List<MeshRenderer> renderers;
    public Rigidbody rb;
    
    [Header("Read-Only")]
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 forward;
    private float prevX;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        forward = transform.forward;
        
        if ((forward.x >= 0 && velocity.x >= 0) || (forward.x < 0 && velocity.x < 0))
        {
            prevX = 1;
        }else if ((forward.x < 0 && velocity.x >= 0) || (forward.x >= 0 && velocity.x < 0))
        {
            prevX = -1;
        }

        if (invert)
        {
            offset = (offset + (velocity.magnitude * -prevX) * scrollSpeed) % 1f;
        }
        else
        {
            offset = (offset + (velocity.magnitude * prevX) * scrollSpeed) % 1f;
        }

        foreach (var r in renderers)
        {
            r.materials[0].mainTextureOffset = new Vector2(offset, 0f);
        }
    }
}
