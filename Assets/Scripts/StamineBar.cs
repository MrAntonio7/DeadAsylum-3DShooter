using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StamineBar : MonoBehaviour
{
    public Slider stamineBar;
    public float maxStamine = 100;
    private float currentStamine;
    private float stamineRegenerateStamineTime = 0.1f;
    private float regeneateAmount = 2f;
    private float losingStamineTime = 0.1f;

    private Coroutine mCoroutineLosing;
    private Coroutine mCoroutineRegenerate;
    // Start is called before the first frame update
    void Start()
    {
        currentStamine = maxStamine;
        stamineBar.maxValue = maxStamine;
        stamineBar.value = maxStamine;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UseStamine (float amount)
    {
        if(currentStamine-amount > 0)
        {
            if (mCoroutineLosing != null)
            {
                StopCoroutine(mCoroutineLosing);
            }
            mCoroutineLosing = StartCoroutine(LosingStamineCoroutine(amount));
            if (mCoroutineRegenerate != null)
            {
                StopCoroutine(mCoroutineRegenerate);
            }
            mCoroutineRegenerate = StartCoroutine(RegenerateStamineCoroutine());

        }
        else
        {
            FindAnyObjectByType<PlayerController>().isSrinting = false;
        }
       
    }

    private IEnumerator LosingStamineCoroutine(float amount)
    {
        while (currentStamine >= 0)
        {
            currentStamine -= amount;
            stamineBar.value = currentStamine;
            yield return  new WaitForSeconds(losingStamineTime);
        }
        mCoroutineLosing=null;
        FindAnyObjectByType<PlayerController>().isSrinting = false;
    }

    private IEnumerator RegenerateStamineCoroutine()
    {
        yield return new WaitForSeconds(1);
        while (currentStamine < maxStamine)
        {
            currentStamine += regeneateAmount;
            stamineBar.value=currentStamine;
            yield return new WaitForSeconds(stamineRegenerateStamineTime);
        }
        mCoroutineRegenerate = null;
    }
}
