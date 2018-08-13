using UnityEngine;

public class TargetController : MonoBehaviour {

    Animator bearAnim;
	// Use this for initialization
	void Start () {
        bearAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bearAnim.SetTrigger("foodNear");
    }
}
