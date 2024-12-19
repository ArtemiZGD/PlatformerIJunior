public class PlayerHealth : Health
{
    protected override void OnDeath()
    {
        SceneLoader.ReloadScene();
    }
}
