using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed=30;
    public float lifeTime = 2;
    static float weaponRecoilDelay = 0.5f;
    bool startedShooting = false;
    public GameObject MachineGun;
    public GameObject Launcher;
    GameObject player;
    public static int weapon = 0;
    Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Coin"))
        {
            weapon = 1;
            MachineGun.SetActive(false);
            Launcher.SetActive(true);
            anim.SetInteger("weapon", 1);
            Destroy(other.gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        weapon = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Lookat.lookAt != null && !startedShooting)
        {
            StartCoroutine(MakeRecoil());
            startedShooting = true;
        }
        else
        {
            StopCoroutine(MakeRecoil());
        }
        if(Lookat.lookAt==null)
        {
            startedShooting = false;
            anim.SetBool("Aiming", false);
        }
        else
        {
            anim.SetBool("Aiming", true);
        }
    }
    void Fire()
    {
        GameObject bullet=GameObject.Find("Bullet");
        if (weapon == 0)
            bullet = Instantiate(bulletPrefab);
        else if (weapon == 1)
            bullet = Instantiate(rocketPrefab);

        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());
        bullet.transform.position = bulletSpawn.position;
        Vector3 rotation = GameObject.Find("Player").transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x,rotation.y,rotation.z);
        player = GameObject.Find("Player");
        bullet.GetComponent<Rigidbody>().AddForce(player.transform.forward * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(DestroyBullet(bullet, lifeTime));
    }
    IEnumerator DestroyBullet(GameObject bullet,float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
    IEnumerator MakeRecoil()
    {
        while(Lookat.lookAt!=null)
        {
            yield return new WaitForSeconds(weaponRecoilDelay);
            Fire();
        }
    }
}
