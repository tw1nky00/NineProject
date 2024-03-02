using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenu = 0,
        Loading = 1,
        Game = 2
    }


    private static Scene _targetScene;


    public static void Load(Scene targetScene)
    {
        _targetScene = targetScene;

        SceneManager.LoadScene((int)Scene.Loading);
    }

    public static void LoadingCallback()
    {
        SceneManager.LoadScene((int)_targetScene);
    }
}
