using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LaserScript : MonoBehaviour
{

    public float mFireRate = .5f;
    public float mFireRange = 50f;
    public float mHitForce = 100f;
    public int mLaserDamage = 100;
    public int mObjDestroyed;
    public Text mScoreText;

    private LineRenderer mLaserLine;

    private bool mLaserLineEnabled;

    private WaitForSeconds mLaserDuration = new WaitForSeconds(0.05f);

    private float mNextFire;

    // Use this for initialization
    void Start()
    {
        mObjDestroyed = 0;
        mLaserLine = GetComponent<LineRenderer>();
    }

    private void Fire()
    {
        Transform cam = Camera.main.transform;

        mNextFire = Time.time + mFireRate;

        Vector3 rayOrigin = cam.position;

        // Set the origin position of the Laser Line
        // It will always 10 units down from the ARCamera
        // We adopted this logic for simplicity
        mLaserLine.SetPosition(0, transform.up * -10f);

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, cam.forward, out hit, mFireRange))
        {
            mObjDestroyed = mObjDestroyed + 1;
            Debug.Log(mObjDestroyed);
            
            mLaserLine.SetPosition(1, hit.point);

            CubeBehaviourScript cubeCtr = hit.collider.GetComponent<CubeBehaviourScript>();
            if (cubeCtr != null)
            {
                if (hit.rigidbody != null)
                {
                    
                    hit.rigidbody.AddForce(-hit.normal * mHitForce);
                    
                    cubeCtr.Hit(mLaserDamage);
                }
            }
            mScoreText.text = mObjDestroyed.ToString();
        }
        else
        {
           
            mLaserLine.SetPosition(1, cam.forward * mFireRange);
        }

       
        StartCoroutine(LaserFx());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > mNextFire)
        {
            Fire();
        }
    }

    
    private IEnumerator LaserFx()
    {
        mLaserLine.enabled = true;
        yield return mLaserDuration;
        mLaserLine.enabled = false;
    }

}