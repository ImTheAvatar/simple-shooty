using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DropDown : MonoBehaviour
{
    List<GameObject> currentCollisions = new List<GameObject>();
    bool tmp = false;
    int hp = 2;
    public GameObject lost;
    Animator anim;

    public TextMeshProUGUI myTextElement;
    void OnTriggerEnter(Collider col)
    {

        // Add the GameObject collided with to the list.
        currentCollisions.Add(col.gameObject);

    }

    void OnTriggerExit(Collider col)
    {

        // Remove the GameObject collided with from the list.
        currentCollisions.Remove(col.gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(pause());
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool tmp2 = false;
        foreach (GameObject gObject in currentCollisions)
        {
            if (gObject != null)
            {
                if (gObject.name.Equals("Walkway") || gObject.name.Equals("Circle2") || gObject.name.Equals("Circle") || gObject.name.Equals("FinishArea"))
                {
                    tmp2 = true;
                }
            }
        }
        if(!tmp2&&tmp)
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            anim.SetBool("isFalling", true);
            StartCoroutine(Lost());
        }
        if (Lookat.lookAt != null)
        {
            if (Vector3.Distance(Lookat.lookAt.transform.position, transform.position) < 1f&& Vector3.Distance(Lookat.lookAt.transform.position, transform.position) >0.1f)
            {
                StartCoroutine(Lost());
            }
        }
    }
    IEnumerator pause()
    {
        yield return new WaitForSeconds(1);
        tmp  = true;
    }
    IEnumerator Lost()
    {
        if (gameObject.tag.Equals("Enemy"))
            yield return new WaitForEndOfFrame();
        else {
            if (hp > 0)
            {
                hp--;
                if(myTextElement!=null) myTextElement.text = hp.ToString();
                yield break;
            }
            lost.SetActive(true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
