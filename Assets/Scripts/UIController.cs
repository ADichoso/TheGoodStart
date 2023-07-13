using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    #region Singleton
    public static UIController sharedInstance;

    void Awake()
    {
        sharedInstance = this;
        if (this != sharedInstance)
        {
            Debug.Log("Warning! More than 1 instance of UIController has been detected");
        }
    }
    #endregion


    bool isCerealPanelShown, isMilkPanelShown;

    public Animator UIAnimator;
    public TextMeshProUGUI cerealText, milkText, endMessageText;
    public TextMeshProUGUI foodNameText, foodDescText;
    public GameObject highlightPanel;
    public GameObject spoonObject;
    public Texture2D CursorImg;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(CursorImg, new Vector2(9, 2), CursorMode.Auto);
    }
    public void OnCerealPanelToggle()
    {
        SoundController.sharedInstance.OnButtonClick();
        if(isCerealPanelShown){
            UIAnimator.Play("HideCerealPanel");
            cerealText.text = "Show Cereal";
        }
        else{
            if(isMilkPanelShown) OnMilkPanelToggle();
            UIAnimator.Play("ShowCerealPanel");
            cerealText.text = "Hide Cereal";
        }

        isCerealPanelShown = !(isCerealPanelShown);
    }

    public void OnMilkPanelToggle()
    {
        SoundController.sharedInstance.OnButtonClick();
        if(isMilkPanelShown){
            UIAnimator.Play("HideMilkPanel");
            milkText.text = "Show Milk";
        }
        else{
            if(isCerealPanelShown) OnCerealPanelToggle();
            UIAnimator.Play("ShowMilkPanel");
            milkText.text = "Hide Milk";
        }

        isMilkPanelShown = !(isMilkPanelShown);
    }

    public void OnResetCerealButton()
    {
        SoundController.sharedInstance.OnButtonClick();
        ItemPool.sharedInstance.clearAllItems();
    }


    public void OnEatCerealButton()
    {
        StartCoroutine(EatCereal());
    }

    IEnumerator EatCereal()
    {
        SoundController.sharedInstance.playSFX(SoundController.sharedInstance.eatButtonSound, false);

        //Hide all UI first
        UIAnimator.Play("HideAllUI");

        //Wait for a bit
        yield return new WaitForSeconds(0.33f);

        //Le Spoon layer
        spoonObject.layer = LayerMask.NameToLayer("Spoon");
        //Eat the cerealll
        GameController.sharedInstance.PlayerAnimator.Play("EatCereal");
        SoundController.sharedInstance.playVFX(SoundController.sharedInstance.eatingCerealSound, false);

        spoonObject.layer = LayerMask.NameToLayer("Default");
        
        yield return new WaitForSeconds(3f);
        GameController.sharedInstance.EndGame();
    }

    public void showEndMessage(string endmessage)
    {
        UIAnimator.Play("ShowEndMessage");
        endMessageText.text = endmessage;
    }

    public void OnRestartButton()
    {
        SoundController.sharedInstance.OnButtonClick();
        SceneManager.LoadScene("GameScene");
    }

    public void OnMenuButton()
    {
        SoundController.sharedInstance.OnButtonClick();
        SceneManager.LoadScene("TitleScene");
    }

    public void displayItemDescription(int item_index)
    {
        foodNameText.text = ItemPool.sharedInstance.itemTypes[item_index].item_name;
        foodDescText.text = ItemPool.sharedInstance.itemTypes[item_index].item_description;
        
        highlightPanel.SetActive(true);     
    }

    public void hideItemDescription()
    {
        foodNameText.text = "";
        foodDescText.text = "";
        
        highlightPanel.SetActive(false);   
    }
}
