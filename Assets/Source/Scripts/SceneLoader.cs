using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
