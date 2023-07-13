using UnityEngine;
using UnityEngine.UI;
public class ItemPanel : MonoBehaviour
{
    public int currentItemIndex;

    public void SelectItem()
    {
        ItemPool.sharedInstance.spawnItem(currentItemIndex);
    }

    public void HighlightItem()
    {
        UIController.sharedInstance.displayItemDescription(currentItemIndex);
    }

    public void UnhighlightItem()
    {
        UIController.sharedInstance.hideItemDescription();
    }
}
