using UnityEngine;
using UnityEngine.SceneManagement;
public class Resetter : MonoBehaviour
{
    PlayerControls pc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        pc = new PlayerControls();
        pc.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(pc.Player.Restart.triggered)
        {
            PlayerPrefs.DeleteAll();
            DestroyAllDontDestroyOnLoadObjects();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public static void DestroyAllDontDestroyOnLoadObjects()
    {
        Scene ddolScene = SceneManager.GetSceneByName("DontDestroyOnLoad");

        if (!ddolScene.IsValid()) return;

        GameObject[] rootObjects = ddolScene.GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            Object.Destroy(obj);
        }
    }
}
