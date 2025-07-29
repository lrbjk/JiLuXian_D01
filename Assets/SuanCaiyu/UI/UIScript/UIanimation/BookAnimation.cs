using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class BookAnimation : MonoBehaviour
{
    //public BookPro BookPro;

    public GameObject GameObject;

    private GameObject currentPage_1;
    private GameObject currentPage_2;



    [SerializeField]
    int currentPage_num = 0;

    private bool ison = false;

    public Animator animator;

  

    private void Update()
    {
        //currentPage_num = BookPro.gameObject.GetComponent<BookPro>().currentPaper;
        isLeaving();
    }


    public void isLeaving()
    {
        ison = gameObject.GetComponent<Toggle>().isOn;

        if (!ison)
        {
            animator.SetBool("ison" , false);
            
        }
        else
        {
            animator.SetBool("ison", true);
            animator.SetTrigger("Selected");
        }
           


    }







}
