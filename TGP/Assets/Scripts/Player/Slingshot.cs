using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour 
{
    private bool _drawn = false;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public float bulletLifeTime;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        if (_drawn && PlayerInventory.CanFireSlingshot)
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                        Input.mousePosition.y,
                                                                        0 - Camera.main.transform.position.z));
            // TODO Shouldn't be so simple.
            transform.LookAt(target);
        }
	}

    private void SlingshotDrawn(bool drawn)
    {
        _drawn = drawn;
    }

    private void Shoot()
    {
        GameObject bul = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation) as GameObject;
        bul.rigidbody.AddForce(transform.forward * PlayerAttributes.SlingshotForce, ForceMode.Impulse);
        Destroy(bul, 1.5f);
    }
}