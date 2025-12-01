using NUnit.Framework;
using UnityEngine;
using System.Linq;
public class Interactor : MonoBehaviour
{
    [SerializeField]
    Transform[] interactableTransforms;
    
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interactWithTarget(Transform target)
    {
        if (interactableTransforms.Contains(target))
        {
            Interactable interactable = target.GetComponent<Interactable>();
            interactable.Interact(transform);
        }

    }
}
