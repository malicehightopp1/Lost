using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SGameplayWidget : MonoBehaviour
{
    [SerializeField] private Image mTransitionImage;
    [SerializeField] private SChildSwitcher mChildSwitcher;
    [SerializeField] private GameObject mRoamingWidget;
    [SerializeField] private SBattleWidget mBattleWidget;
    private void Awake()
    {
        mTransitionImage.gameObject.SetActive(false);
    }
    public void DipToBlack(float DipDuration, float DipStayDuration, Action DippedToBlackCallBack) //its callable so we can circle back - callable to doing something **action**
    {
        StartCoroutine(StartDipToBlack(DipDuration, DipStayDuration, DippedToBlackCallBack)); //internally ienumerator is a whille loop
    }
    public void SetFocusedCharacterInBattle(SBattleCharacter battleCharacter)
    {
        mBattleWidget.SetCharacterContolTarget(battleCharacter);
    }
    IEnumerator StartDipToBlack(float DipDuration, float DipStayDuration, Action DippedToBlackCallBack) //allows a function to be used as its a container
    {
        float timerCounter = 0;
        mTransitionImage.gameObject.SetActive(true);
        Color TransitionColor = Color.black; //setting color to black
        TransitionColor.a = 0; //setting alpha to 0 **transparent**
        while (timerCounter < DipStayDuration)
        {
            TransitionColor.a = timerCounter / DipStayDuration; //equalling the alpha to equal the time counter so its appears to fade
            mTransitionImage.color = TransitionColor;

            timerCounter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        TransitionColor.a = 1;
        mTransitionImage.color = TransitionColor;
        DippedToBlackCallBack(); // **action** can be used to call to other function from other scripts **not hard on memory** VERY IMPORTANT
        yield return new WaitForSeconds(DipStayDuration);

        while(TransitionColor.a > 0) //while the alpha of the ui is above 0 reduce the alpha
        {
            TransitionColor.a = timerCounter / DipStayDuration;
            mTransitionImage.color = TransitionColor;

            timerCounter -= Time.deltaTime;
            yield return new WaitForEndOfFrame();  
        }
        mTransitionImage.gameObject.SetActive(false);
    }
    internal void SwitchToBattle()
    {
        mChildSwitcher.SetActiveChild(mBattleWidget.gameObject);
    }
    internal void SwitchToRoaming()
    {
        mChildSwitcher.SetActiveChild(mRoamingWidget);
    }
}
