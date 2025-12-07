using UnityEngine;

public class VerticalFireballController : MonoBehaviour
{
    Animator fireballAnimator;
    public bool hitWall = false;
    public int direction = -1;
    public float speed = 5f;
    
    void Start()
    {
        fireballAnimator = GetComponent<Animator>();
        if (direction < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    void Update()
    {
        if (!hitWall)
        {
            transform.position += Vector3.down * direction * speed * Time.deltaTime;
        }
        
    }

    public void destroySelf()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireCollider"))
        {
            hitWall = true;
            fireballAnimator.SetBool("HitWall",true);
            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
        }
    }
}

