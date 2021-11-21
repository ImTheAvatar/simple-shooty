using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovmentScript : MonoBehaviour
{
    protected Joystick joystick;
    public float sensitivity;
    public static Vector3 movementDirection;
    public static bool won = false;
    float countDown = 0;
    public GameObject MachineGun;
    public GameObject Launcher;
    Animator anim;
    Vector3 tmp;
    public GameObject completeLevelUI;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        anim = GetComponentInChildren<Animator>();
    }
    void lookAt()
    {
        movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        movementDirection.Normalize();
        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }
        tmp = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(joystick.Horizontal * sensitivity/100,0,joystick.Vertical * sensitivity/100);
        if (Vector3.Distance(tmp, transform.position) > 0)
        {
            anim.SetBool("isRunning", true);
            tmp = transform.position;
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if(Lookat.lookAt==null)
        {
            if (countDown <= 0)
                lookAt();
            else
                countDown -= Time.deltaTime;
        }
        else
        {
            countDown = 1;
        }
        if(transform.position.z>41.1f)
        {
            MachineGun.SetActive(false);
            Launcher.SetActive(false);
            anim.SetBool("haveWon", true);
            completeLevelUI.SetActive(true);
            StartCoroutine(start());
        }
    }
    IEnumerator start()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
