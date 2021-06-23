using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] List<Animator> barriers = new List<Animator>();
    [SerializeField] Material on;
    [SerializeField] Material off;
    [SerializeField] GameObject Off;
    [SerializeField] GameObject On;

    bool switched;
    MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material = off;
        DisableBarriers();
        switched = false;
        Off.SetActive(true);
        On.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ball" && !switched)
        {
            SwitchOn();
        }
    }
    public void SwitchOn()
    {
        SoundManager.instance.PlaySwitchSound();
        mr.material = on;
        switched = true;
        Off.SetActive(false);
        On.SetActive(true);

        foreach(var element in barriers)
        {
            bool b = element.enabled;
            element.enabled = !b;
        }
    }

    public void DisableBarriers()
    {
        foreach (var element in barriers)
        {
            element.enabled = false;
        }
    }
}
