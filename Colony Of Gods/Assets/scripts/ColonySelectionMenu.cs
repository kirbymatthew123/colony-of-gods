using UnityEngine;
using UnityEngine.SceneManagement;

public class ColonySelectionMenu : MonoBehaviour
{
    public GameObject colonyPanel;   // yung SELECT panel
    public GameObject mapSelection;  // yung MapSelection panel

    // Tatawagin sa colony button (Ant, Beetle, etc.)
    public void SelectColony(string colonyName)
    {
        PlayerPrefs.SetString("SelectedColony", colonyName);
        Debug.Log("Colony Selected: " + colonyName);

        // Disable colony panel
        if (colonyPanel != null)
            colonyPanel.SetActive(false);

        // Enable map selection panel
        if (mapSelection != null)
            mapSelection.SetActive(true);
    }

    // Tatawagin sa map button (Forest, Cave, etc.)
    public void SelectMap(string mapName)
    {
        string colony = PlayerPrefs.GetString("SelectedColony", "Ant");
        Debug.Log("Starting Game → Colony: " + colony + " | Map: " + mapName);

        SceneManager.LoadScene(mapName); 
    }
}
