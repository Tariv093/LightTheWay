using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneName;
    Collider col;
    // Start is called before the first frame update
    public void OnClick()
    {
        Debug.Log("1");
        SceneManager.LoadScene(sceneName);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            SceneManager.LoadScene(sceneName);
        }
    }

}
