using UnityEngine;
using UnityEngine.SceneManagement;

public class ColonySelectionMenu : MonoBehaviour
{
    public void GoToMap()
    {
        SceneManager.LoadScene("map");
    }
}
