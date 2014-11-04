using UnityEngine;
using System.Collections;

[AddComponentMenu("TGP/Item Behaviours/Magnetic Item")]
public class MagneticItem : MonoBehaviour 
{
    public float magneticForce = 5;
    private float _magRange;
    private bool _haveTarget;
    private bool _stuck;
    private Vector3 _target;
    private MagnetHead _magHead;

	// Use this for initialization
	void Start () 
    {
        rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (!_stuck)
        {
            if (_haveTarget)
            {
                Vector3 magField = _target - transform.position;
                float index = (10 - magField.magnitude) / 10;
                rigidbody.AddForce(magneticForce * magField * index);
            }

            float dis = Vector3.Distance(_target, transform.position);

            if(dis < 0.55f)
            {
                _stuck = true;
                rigidbody.isKinematic = true;
                transform.parent = _magHead.transform;
            }
        }
        else
        {
            transform.position = _magHead.transform.position;
        }
	}

    void SetAttractionTarget(MagnetHead mag)
    {
        _haveTarget = true;
        rigidbody.useGravity = false;
        _magRange = mag.Range;
        _magHead = mag;
        _target = mag.transform.position;
    }

    void RemoveAttractionTarget()
    {
        _haveTarget = false;
        transform.parent = null;
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        _stuck = false;
        _magHead = null;
        _target = Vector3.zero;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag.Equals("Player"))
        {
            _magHead.RemoveFromInRangeList(this);
            BroadcastMessage("PlayerHit", SendMessageOptions.RequireReceiver);
        }
    }
}
