using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosTest : MonoBehaviour
{
    public GizmosSetting cube, sphere, capsule;
    public LayerMask layer;
    //public bool onHitbox;
    private event Action hitboxEvent;
    // Start is called before the first frame update
    void Start()
    {
        cube.openGizmos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponent<Animator>().CrossFadeInFixedTime("OH_Light_Attack_01", 0.25f);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<Animator>().CrossFadeInFixedTime("Shoot", 0.25f);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            GetComponent<Animator>().CrossFadeInFixedTime("Loading", 0.25f);
        }
        hitboxEvent?.Invoke();
    }
    public virtual void OpenHit()
    {
        cube.openGizmos = true;
        hitboxEvent += hitEvent;
    }
    public virtual void CloseHit()
    {
        cube.openGizmos = false;
        hitboxEvent -= hitEvent;
    }
    private void hitEvent()
    {
        List<Collider> enemies = cube.GetCollInRange(LayerMask.NameToLayer(layer.ToString()));
        foreach (Collider enemy in enemies)
        {
            if(enemy != null)
            {
                Debug.Log("Hit Enemy" + enemy.ToString());
                StartCoroutine(FreezingAnimation(0.05f));
            }
        }
        //cube.ClearDetectedColliders();
        
    }

    private IEnumerator FreezingAnimation(float time)
    {
        GetComponent<Animator>().speed = 0f;
        yield return new WaitForSecondsRealtime(time);
        GetComponent<Animator>().speed = 1f;
    }
}
