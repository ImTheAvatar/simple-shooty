using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    private Rigidbody rb;
    private Vector3 movement;
    bool stop = true;
    public float moveSpeed=5f;
    public float hp=6;
    public int coinChanceOutOfOne = 5;
    Animator anim;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag.Equals("Projectile"))
        {
            if(PlayerWeapon.weapon==0)
            {
                hp -= 2;
            }
            if (PlayerWeapon.weapon == 1)
            {
                hp -= 3;
            }
            if (PlayerWeapon.weapon == 2)
            {
                hp -= 1;
            }
            Destroy(collision.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
        transform.LookAt(player);

    }
    private void FixedUpdate()
    {

        if (MovmentScript.won)
            return;
        if (!stop)
        {
            moveCharacter(movement);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
            if (Vector3.Distance(player.position, this.transform.position) < 10)
            {
                stop = false;
            }
        }
        if(hp<=0)
        {
            Destroy(gameObject);
            int tmp = Random.Range(1, coinChanceOutOfOne);
            print(tmp);
            if(tmp==2)
                Instantiate(GameObject.Find("Coin"),new Vector3(transform.position.x,transform.position.y+1,transform.position.z),transform.rotation);
        }
    }
    void moveCharacter(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
