using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;


    private KitchenObject KitchenObject;

    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if(KitchenObject!= null)
            {
                KitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }
    public void Interact()
    {
        if (KitchenObject == null)
        {
            Debug.Log("Clear Counter Interacted");
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(KitchenObject.GetClearCounter());
        }

    }

    public Transform GetKitchenObjectFollowTranform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.KitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return KitchenObject;
    }

    public void ClearKitchenObject()
    {
        KitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return KitchenObject != null;
    }
}
