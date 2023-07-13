using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;
public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController sharedInstance;
    
    void Awake()
    {
        sharedInstance = this;
        if (this != sharedInstance)
        {
            Debug.Log("Warning! More than 1 instance of GameController has been detected");
        }
    }
    #endregion

    public Volume CerealVolume, MilkVolume;
    public Animator PlayerAnimator;
    public EndingEffect[] endingEffects;
    public GameObject EndingParticleParent;
    bool hasCereal, hasMilk;
    
    public void EndGame()
    {
        StartCoroutine(playEndingAnimation());
    }

    IEnumerator playEndingAnimation()
    {
        //wait for a bit before playing the animation
        string EndMessage = "";
        int cerealIndex = ItemPool.sharedInstance.getItemTypeIndex
                            (ItemPool.sharedInstance.selectedCerealType.item_name);
        int milkIndex = ItemPool.sharedInstance.getItemTypeIndex
                            (ItemPool.sharedInstance.selectedMilkType.item_name);

        //Check if you have cereal and milk
        if(cerealIndex == -1)
            hasCereal = false;
        else
            hasCereal = true;

        if(milkIndex == -1)
            hasMilk = false;
        else
            hasMilk = true;


        //ENDING MESSAGE PART
        if(hasMilk && hasCereal)
        {
            EndMessage = "That was some nice cereal.";
        } else if(!hasMilk && hasCereal)
        {
            EndMessage = "A bit dry, but it's fine.";
        } else if(hasMilk && !hasCereal)
        {
            EndMessage = "Wha- That's just milk. Why?";
        } else
        {
            EndMessage = "Why did you even bother...";
        }

        //DELAY IN BETWEEN ANIMATIONS
        if(!hasCereal && !hasMilk)
            yield return new WaitForSeconds(2f);
        else
            yield return new WaitForSeconds(1f);
        
        //0 - 8: Cereal
        //9 - 15: Milk

        //Apply the post process volume first
        if(hasCereal)
        {
            switch(cerealIndex)
            {
                case 1:
                case 2:
                case 5:
                    CerealVolume.profile = endingEffects[cerealIndex].VolumeEffect;
                    break;
            }
        }

        if(hasMilk)
        {
            switch(milkIndex)
            {
                case 10:
                case 11:
                case 13:
                    MilkVolume.profile = endingEffects[milkIndex].VolumeEffect;
                    break;
            }
        }

        if(CerealVolume.profile != null && MilkVolume.profile != null)
            PlayerAnimator.Play("ShowMilkCerealVolume");
        else if(CerealVolume.profile == null && MilkVolume.profile != null)
            PlayerAnimator.Play("ShowMilkVolume");
        else if(CerealVolume.profile != null && MilkVolume.profile == null)
            PlayerAnimator.Play("ShowCerealVolume");


        //PARTICLE SYSTEMS PART
        if(hasCereal)
        {
            switch(cerealIndex)
            {
                case 2:
                case 3:
                case 6:
                case 7:
                case 8:
                    Instantiate(endingEffects[cerealIndex].ParticlesEffectPrefab, EndingParticleParent.transform);
                    break;
            }
        }

        if(hasMilk)
        {
            switch(milkIndex)
            {
                case 10:
                case 11:
                    Instantiate(endingEffects[milkIndex].ParticlesEffectPrefab, EndingParticleParent.transform);
                    break;
            }
        }

        if(hasMilk)
        {
            switch(milkIndex)
            {
                case 12:
                case 13:
                case 14:
                    SoundController.sharedInstance.playVFX(endingEffects[milkIndex].SoundEffect, false);
                    break;
            }
        }

        if(hasCereal)
        {
            switch(cerealIndex)
            {
                case 4:
                    SoundController.sharedInstance.playVFX(endingEffects[cerealIndex].SoundEffect, false);
                    break;
            }
        }

        if(hasCereal)
        {
            switch(cerealIndex)
            {
                case 4:
                    ItemPool.sharedInstance.ExplosionEffect();
                    break;
            }
        }

        if(hasMilk)
        {
            switch(milkIndex)
            {
                case 12:
                    PlayerAnimator.Play("GhostFloat");
                    yield return new WaitForSeconds(10f);
                    break;
                case 13:
                    PlayerAnimator.Play("BananaMonkey");
                    yield return new WaitForSeconds(20f);
                    break;
                case 14:
                    PlayerAnimator.Play("StaticElectricity");
                    yield return new WaitForSeconds(5.5f);
                    break;
            }
        }

        UIController.sharedInstance.showEndMessage(EndMessage);
    }
}

[System.Serializable]
public struct EndingEffect
{
    public VolumeProfile VolumeEffect;
    public GameObject ParticlesEffectPrefab;
    public AudioClip SoundEffect;
    //Transform PlaceEffect;
}