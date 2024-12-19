public class PlayerHealth : Health
{
    override protected void OnDeath()
    {
        SceneLoader.ReloadScene();
    }
}
