using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public Rigidbody2D PlayerRigidbody;
    public float jumpforce = 10f;
    public float speed = 100f;
    public bool isjump = false;


    [SerializeField]
    private LayerMask groundlayer;
    private bool isgrounded;
    private CapsuleCollider2D boxcollider2D;
    private Vector3 footPosition;
    grapling grap;


    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        boxcollider2D = GetComponent < CapsuleCollider2D>();
        grap = GetComponent<grapling>();

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (isgrounded == true)
        //    {
        //        //점프
        //        PlayerRigidbody.velocity = Vector2.up * jumpforce;
        //    }
        //}
        Jump();
        walk();
    }
    private void FixedUpdate()
    {
       

        // 점프중인지 확인
        Bounds bounds = boxcollider2D.bounds;

        footPosition = new Vector2(bounds.center.x, bounds.min.y-0.15f);

        isjump = Physics2D.OverlapCircle(footPosition, 0.2f, groundlayer);
        
    }

    void walk()
    {
        //이동
        float hor = Input.GetAxis("Horizontal");
        if (hor != 0)
        {
            if (grap.isAttach)
            {
                PlayerRigidbody.AddForce(new Vector2((hor) * speed * Time.deltaTime, 0));
            }
            else
            {
                PlayerRigidbody.velocity = (new Vector2((hor) * speed * Time.deltaTime, PlayerRigidbody.velocity.y));
            }

        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            if (isgrounded)
            {
                PlayerRigidbody.AddForce(Vector3.up * jumpforce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(footPosition, 0.2f);
    }

}
