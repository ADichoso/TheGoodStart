using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    public Texture2D CursorImg;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(CursorImg, new Vector2(9, 2), CursorMode.Auto);
    }

    public void OnPlayButton()
    {
        SoundController.sharedInstance.OnButtonClick();
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitButton()
    {
        SoundController.sharedInstance.OnButtonClick();
        Application.Quit();
    }
}
