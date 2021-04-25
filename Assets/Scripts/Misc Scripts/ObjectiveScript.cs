using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveScript : MonoBehaviour
{
    [SerializeField] GameObject go, shower;
    public bool lit, wet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lit == true)
        {
            wet = false;
            go.GetComponent<Animator>().Play("DoorMove");
        }
        if(wet == true)
        {
            lit = false;
           
        }
        if(shower != null)
        shower.SetActive(wet);
        
    }
    
    public void SetBool()
    {
        lit = true;
    }
    public void SetWet()
    {
        wet = true;
    }
}
