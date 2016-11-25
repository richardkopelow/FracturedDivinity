using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{

    public string Level;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>()!=null)
        {
            SceneManager.LoadScene(Level);
        }
    }
    public void SetLevel(string level)
    {
        Level = level;
    }
}
