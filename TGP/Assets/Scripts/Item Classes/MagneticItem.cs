using UnityEngine;
using System.Collections;

[AddComponentMenu("TGP/Item Behaviours/Magnetic Item")]
public class MagneticItem : MonoBehaviour 
{
    public float magneticForce = 5;
    private float _indexModifier;
    private float _magRange;
    [SerializeField]
    private bool _haveTarget;
    private bool _stuck;
    private Vector3 _targetPos;
    private MagnetHead _magHead;

	// Use this for initialization
	void Start () 
    {
        rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
        _indexModifier = 10;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        //if (!_stuck)
        //{
            if (_haveTarget)
            {
                Vector3 magField = _targetPos - transform.position;
                //float index = (_indexModifier - magField.magnitude) / _indexModifier;
                //Vector3 vec = magneticForce * magField * index;
                //Log.BLUE(transform.name + " force vector: " + vec);
                rigidbody.AddForce(magneticForce * magField.normalized);
            }

            //float dis = Vector3.Distance(_target, transform.position);

            //if(dis < 0.55f)
            //{
            //    _stuck = true;
            //    rigidbody.isKinematic = true;
            //    transform.parent = _magHead.transform;
            //}
        //}
        //else
        //{
        //    transform.position = _magHead.transform.position;
        //}
	}

    void SetAttractionTarget(MagnetHead mag)
    {
        _haveTarget = true;
        rigidbody.useGravity = false;
        _magRange = mag.Range;
        _magHead = mag;
        _targetPos = mag.transform.position;
    }

    void RemoveAttractionTarget()
    {
        _haveTarget = false;
        transform.parent = null;
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        _stuck = false;
        _magHead = null;
        _targetPos = Vector3.zero;
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
