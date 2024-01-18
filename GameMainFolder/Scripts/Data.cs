using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Data : MonoBehaviour
{
    public int index;
    public TMP_Text nameText;
    public TMP_Text EmailText;
    public string password;
    public string key;


    public void DeleteUser()
    {
        StartCoroutine(DeleteUserAsync());
    }

    IEnumerator DeleteUserAsync()
    {
        AccountManager.instance.deleteUser = true;

        StartCoroutine(AccountManager.instance.FirebaseLoginAsync(EmailText.text, password));

        yield return new WaitForSeconds(2f);

        AccountManager.instance.deleteUserFromFirebase();


        Destroy(this.gameObject, 2f);
    }

}
