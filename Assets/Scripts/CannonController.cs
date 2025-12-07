using System.Threading;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject leftFireball;
    public GameObject rightFireball;
    public GameObject upFireball;
    public GameObject downFireball;
    public float rotation;
    public float timerCount = 1.0f;
    public float timer = 5.0f;
    public Animator anim;
    public float animSpeed = 1.0f;
    public int initialShotsBeforeDelay = 1;
    public int noShotsBeforeDelay = 1;
    public float delayCount = 0f;
    public float delay = 0f;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = animSpeed;
        rotation = transform.eulerAngles.z;
    }

    void Update()
    {
        if (delay > 0)
        {
            delay -= 1*Time.deltaTime;
        }
        else {
            timer -= 1*Time.deltaTime;

            if (timer <= 0)
            {
                anim.SetBool("Active",true);
            }
        }
    }

    public void spawnFireBall()
    {
        noShotsBeforeDelay--;
        if (noShotsBeforeDelay <= 0)
        {
            noShotsBeforeDelay = initialShotsBeforeDelay;
            delay = delayCount;
        }

        Vector3 spawnPos = transform.position + transform.up;
        if (rotation == 90)
        {
            Instantiate(leftFireball, spawnPos, leftFireball.transform.rotation);
        }
        else if (rotation == 0)
        {
            Instantiate(upFireball, spawnPos, upFireball.transform.rotation);
        }
        else if (rotation == 180)
        {
            Instantiate(downFireball, spawnPos, downFireball.transform.rotation);
        }
        else
        {
            Instantiate(rightFireball, spawnPos, rightFireball.transform.rotation);
        }
        timer = timerCount;
        anim.SetBool("Active", false);
        
    }
}
