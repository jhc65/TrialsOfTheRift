using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class ShaderEffect : MonoBehaviour {
	
	public AudioSource EffectSource;
	public AudioClip EffectSound;
	public Renderer SourceRenderer;
	private Material[] EffectMaterials;	
	public float EffectLength = 0.0f;

    public bool isFinished = false;
    public float currentParamValue;
	
	
	// Start is called just before any of the
	// Update methods is called the first time.
	void Start () {
		EffectMaterials = SourceRenderer.materials;
	}
	
	private void SetMaterialParms(string paramName, float amount)
	{
		foreach(Material m in EffectMaterials)
		{
            m.SetFloat(paramName, amount);
            //currentParamValue = amount;
			//m.SetFloat("_DisintegrateAmount",amount);
		}	
	}
	
	private IEnumerator doParamIncrease(bool Destroy, string paramName)
	{
				
		// if we supply our own sound, use it.
		if (EffectSource != null && EffectSound != null)
		{
			EffectLength = EffectSound.length;
			EffectSource.PlayOneShot(EffectSound);
		}		

		currentParamValue = EffectLength;
		
		while(currentParamValue >= 0.0f)
    	{
			float pos = 1.0f - (currentParamValue / EffectLength);
			SetMaterialParms(paramName,pos);
        	yield return null;
            currentParamValue -= Time.deltaTime;
    	}
		SetMaterialParms(paramName,1.01f);
        isFinished = true;

        if (Destroy)
		{
			GameObject.Destroy(this.gameObject);
		}
	}
	
	private IEnumerator doParamDecrease(bool destroy, string paramName)
	{
		// if we supply our own sound, use it.
		if (EffectSource != null && EffectSound != null)
		{
			EffectLength = EffectSound.length;
			EffectSource.PlayOneShot(EffectSound);
		}		

		currentParamValue = EffectLength;
				
		while(currentParamValue >= 0.0f)
    	{
			float pos =  (currentParamValue / EffectLength);
			SetMaterialParms(paramName,pos);
        	yield return null;
            currentParamValue -= Time.deltaTime;
    	}
		
		SetMaterialParms(paramName,0.0f);
        isFinished = true;

        if (destroy)
        {
            GameObject.Destroy(this.gameObject);
        }
    }	
	
	private void paramIncrease(bool doDestroy, string paramName)
	{
		StartCoroutine(doParamIncrease(doDestroy, paramName));
	}
	
	private void paramDecrease(bool doDestroy, string paramName)
	{
		StartCoroutine(doParamDecrease(doDestroy, paramName));
	}	

	public void paramIncrease(float Length, bool doDestroy, string paramName)
	{
		EffectLength = Length;
        isFinished = false;
        paramIncrease(doDestroy, paramName);
	}
	
	public void paramDecrease(float Length, bool doDestroy, string paramName)
	{
		EffectLength = Length;
        isFinished = false;
		paramDecrease(doDestroy, paramName);
	}	
	
	public void OnDestroy()
	{
		foreach(Material m in EffectMaterials)
		{
			Destroy (m);
		}
	}


}


/*
public class example : MonoBehaviour {
    void Awake() {
      
        renderer.material.color = Color.red;
    }
} */