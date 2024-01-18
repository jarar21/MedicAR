using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

using Firebase.Extensions;
using System.Security.Cryptography;
using System.Text;
using System;

public class AccountManager : MonoBehaviour
{
    //private constants for encryption key generation
    //private const string secretKey = "1+uCIcHrnff6oF9rDjvxoUv0+gP+bJrVcs2RjZDLfFw=";
    //private const int keySize = 256;
    //private const int blockSize = 128;

    public bool deleteUser;

    public static AccountManager instance;

   
    
    
    public GameObject AdminPanel;
    public GameObject Home;

    [Space(10)]
    [Header("Registered users Data")]
    public TMP_Text Users_Count;


   [Space(10)]
    [Header("Firebase Authentication Data")]
    public DependencyStatus _dependencyStatus;
    public FirebaseAuth Auth;
    public FirebaseUser User;
    public DatabaseReference reference;


    public GameObject HomePanel;
    public GameObject CreatePanel;
    public GameObject JoinPanel;
    [Space(10)]
    [Header("Create Account Screen Data")]
    public TMP_InputField Name;
    public TMP_InputField Email;
    public TMP_InputField Password;
    public TMP_InputField confirm_password;
    [Space(10)]
    [Header("Login Account Screen Data")]
    public TMP_InputField LoginEmail;
    public TMP_InputField LoginPassword;
    bool isLoading;

    [Space(10)]
    [Header("ResetPassword Screen Data")]
    public TMP_InputField ResetEmail;

    [Space(10)]
    [Header("Email Verification Data")]
    public GameObject EmailVerifyPanel;
    public TMP_Text EmailMessageText;
    public TMP_Text ErrorMessagePane;
    [Space(10)]
    [Header("Users Data")]
    public GameObject Data;
    public GameObject DataSpawnPos;

    //Initialization of the Firebase

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            _dependencyStatus = task.Result;

            if (_dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.Log("Missing dependencies: " + _dependencyStatus);
            }

        });

        Debug.Log(reference);
    }


    void InitializeFirebase()
    {
        Auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Auth.StateChanged += AuthStateChanged;
        
        AuthStateChanged(this, null);
        
    }


    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        bool signedIn = User != Auth.CurrentUser && Auth.CurrentUser != null;

        if (!signedIn && User != null) {
        }
        {
            Debug.Log("Signed out:" + User.UserId);
        }
        
        User = Auth.CurrentUser;

        if (signedIn)
        {
            Debug.Log("Signed in:" + User.UserId);
        }
        
    }
    
    public void Register()
    {

        StartCoroutine(FirebaseRegisterAsync(Name.text, Email.text, Password.text, confirm_password.text));
    }

    IEnumerator FirebaseRegisterAsync(string name, string email, string password, string confirm_password)
    {
      

        if ( name == null ||  email == null || password == null || confirm_password == null)
        {
            ErrorMessagePane.text = "Please fill all the fields";

            Debug.Log("Please fill all the fields");

        }

        if( password != confirm_password)
        {

            ErrorMessagePane.text = "Passwords do not match";

            Debug.Log("Passwords do not match");
        }
        
        else
        {
            var RegisterTask = Auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(()=> RegisterTask.IsCompleted);

            if(RegisterTask.Exception != null)
            {
                FirebaseException firebaseException = RegisterTask.Exception.GetBaseException() as FirebaseException;

                AuthError authError = (AuthError)firebaseException.ErrorCode;


                string failMessage = "Registeration could not complete because ";
                
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failMessage += "email is invalid";
                        ErrorMessagePane.text = failMessage;
                        break;

                    case AuthError.WrongPassword:
                        failMessage += "wrong password";
                        ErrorMessagePane.text = failMessage;
                        break;

                    case AuthError.MissingPassword:
                        failMessage += "password is missing";
                        ErrorMessagePane.text = failMessage;
                        break;

                    case AuthError.MissingEmail:
                        failMessage += "email is missing";
                        ErrorMessagePane.text = failMessage;
                        break;
                    
                    case AuthError.EmailAlreadyInUse:
                        failMessage += "email already in use";
                        ErrorMessagePane.text = failMessage;
                        break;

                    default:
                        failMessage += "registeration failed";
                        ErrorMessagePane.text = failMessage;
                        break;
                }

                Debug.Log(failMessage+" "+authError);
            }

            else
            {
                ErrorMessagePane.text = "";
                User = RegisterTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = name };

                var updateProfileTask = User.UpdateUserProfileAsync(userProfile);


                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                
                if(updateProfileTask.Exception != null)
                {
                    //Delete the user if an exception occurs
                    User.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;

                    AuthError authError = (AuthError)firebaseException.ErrorCode;

                    string failMessage = "Account updation failed because ";

                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failMessage += "because email is invalid";
                            ErrorMessagePane.text = failMessage;
                            break;

                        case AuthError.WrongPassword:
                            failMessage += "because of wrong password";
                            ErrorMessagePane.text = failMessage;
                            break;

                        case AuthError.MissingEmail:
                            failMessage += "because of email is missing";
                            ErrorMessagePane.text = failMessage;
                            break;

                        case AuthError.MissingPassword:
                            failMessage += "because of password is missing";
                            ErrorMessagePane.text = failMessage;
                            break;

                        default:
                            failMessage += "server error, please try again";
                            ErrorMessagePane.text = failMessage;
                            break;

                    }

                    Debug.Log(authError+" "+ failMessage);
                }

                else
                {
                    if (!User.IsEmailVerified)
                    {

                        Debug.LogError("HAshiarf 1122");
                        string userId = User.UserId;
                        Debug.LogError("HAshiarf 1122 Id = "+ userId);
                        Debug.Log(userId + "UserID");
                        //reference = FirebaseDatabase.DefaultInstance.RootReference.Child(userId);
                        // You can use a unique identifier for each user as the key
                        var usernameDBTask = reference.Child("users").Child(userId).Child("username").SetValueAsync(name);
                        Debug.LogError("HAshiarf 1122 Username = " + usernameDBTask);
                        var emailDBTask = reference.Child("users").Child(userId).Child("email").SetValueAsync(email);
                        Debug.LogError("HAshiarf 1122 Email = " + emailDBTask);
                        var passwordDBTask = reference.Child("users").Child(userId).Child("password").SetValueAsync(password);

                        yield return new WaitUntil(predicate: () => usernameDBTask.IsCompleted && emailDBTask.IsCompleted && passwordDBTask.IsCompleted);
                        Debug.LogError("Hashir after yeild");
                        if (usernameDBTask.Exception != null && emailDBTask.Exception != null && passwordDBTask != null)
                        {
                            Debug.LogWarning(message: $"failed to register task with {usernameDBTask.Exception} {emailDBTask.Exception} {passwordDBTask.Exception}");

                        }
                        else
                        {
                            Debug.Log("Database updated!");
                        }

                        Debug.LogError("Hashir after After user texftedatfdsgdsf");


                        SendVerificationEmail();


                    }
                    else
                    {
                        ErrorMessagePane.text = "";
                        Debug.Log("Account Registered Successfully " + User.DisplayName);
                    }

                   
                    
                    //Debug.Log(userId + "User ID");
                    //reference.Child(userId).Child("name").SetValueAsync(name);
                    //reference.Child(userId).Child("email").SetValueAsync(email);
                }

            }
        }
    }
    /*
    public string EncryptString(string plainText)
    {
        
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(secretKey);
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.BlockSize = blockSize;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            byte[] encryptedBytes = null;
            using (var msEncrypt = new System.IO.MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                    csEncrypt.FlushFinalBlock();
                    encryptedBytes = msEncrypt.ToArray();
                }
            }

            byte[] combinedBytes = new byte[aesAlg.IV.Length + encryptedBytes.Length];
            Array.Copy(aesAlg.IV, 0, combinedBytes, 0, aesAlg.IV.Length);
            Array.Copy(encryptedBytes, 0, combinedBytes, aesAlg.IV.Length, encryptedBytes.Length);

           

            return Convert.ToBase64String(combinedBytes);
        }
    }

    public string DecryptString(string secretKey, string cipherText)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(secretKey);
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.BlockSize = blockSize;
            aesAlg.Padding = PaddingMode.PKCS7;

            byte[] iv = new byte[aesAlg.IV.Length];
            byte[] cipherBytes = new byte[cipherTextBytes.Length - aesAlg.IV.Length];

            Array.Copy(cipherTextBytes, iv, aesAlg.IV.Length);
            Array.Copy(cipherTextBytes, aesAlg.IV.Length, cipherBytes, 0, cipherBytes.Length);

            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new System.IO.MemoryStream(cipherBytes))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }*/

    public void GetAllUsers()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("users");

        //reference.GetValueAsync().contin


        reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Database fetch encountered an error: " + task.Exception);
                return;
            }
            if (task.IsCompleted)
            {

                DataSnapshot snapshot = task.Result;

                Users_Count.text = snapshot.ChildrenCount.ToString();

                foreach (var child in snapshot.Children)
                {
                    Debug.Log(child.Child("email").Value.ToString());
                    string userId = child.Key;
                    Debug.Log(userId);
                    string name = child.Child("username").Value.ToString();
                    string email = child.Child("email").Value.ToString();
                    string password = child.Child("password").Value.ToString();
                    GameObject obj = Instantiate(Data, DataSpawnPos.transform);
                    Data data= obj.GetComponent<Data>();
                    data.nameText.text = name;
                    data.EmailText.text = email;
                    data.password = password;
                    data.key = userId;

                    // ... handle user data as needed
                }
                
            }
            
        });
    }
    public void AutoLogin()
    {
        if(User != null && User.IsEmailVerified)
        {
            SceneManager.LoadScene("Menu");
        }

        else
        {
            HomePanel.SetActive(false);
            CreatePanel.SetActive(false);
            JoinPanel.SetActive(true);
        }
    }

    //Delete user from firebase and authentication


    public void Login()
    {
        if(LoginEmail.text == "admin" && LoginPassword.text == "0000")
        {
            JoinPanel.SetActive(false);
            HomePanel.SetActive(true);
            AdminPanel.SetActive(true);
            GetAllUsers();

            
        }


        else
        {
            StartCoroutine(FirebaseLoginAsync(LoginEmail.text, LoginPassword.text));
        }
    }
    
    public void deleteUserFromFirebase()
    {
        StartCoroutine(DeleteUserFromFirebaseAsync());
    }

    IEnumerator DeleteUserFromFirebaseAsync()
    {
        string userId = User.UserId.ToString();

        var usernameDBTask = reference.Child("users").Child(userId).Child("username").SetValueAsync(null);
        var emailDBTask = reference.Child("users").Child(userId).Child("email").SetValueAsync(null);
        var passwordDBTask = reference.Child("users").Child(userId).Child("password").SetValueAsync(null);

        yield return new WaitUntil(predicate: () => usernameDBTask.IsCompleted && emailDBTask.IsCompleted && passwordDBTask.IsCompleted);

        Users_Count.text = (int.Parse(Users_Count.text) - 1).ToString();

        var DeleteUserTask = User.DeleteAsync();

        yield return new WaitUntil(() => DeleteUserTask.IsCompleted);

        deleteUser = false;
    }
    public IEnumerator FirebaseLoginAsync(string email, string password)
    {

        var LoginTask = Auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => LoginTask.IsCompleted);

        if(LoginTask.Exception != null)
        {
            Debug.LogError(LoginTask.Exception);

            FirebaseException firebaseException = LoginTask.Exception.GetBaseException() as FirebaseException;

            AuthError authError = (AuthError)firebaseException.ErrorCode;

            string failedMessage = "Your Authentication failed because ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "your email is invalid";
                    ErrorMessagePane.text = failedMessage;
                    break;

                case AuthError.WrongPassword:
                    failedMessage += "your password is invalid";
                    ErrorMessagePane.text = failedMessage;
                    break;

                case AuthError.MissingEmail:
                    failedMessage += "your email is missing";
                    ErrorMessagePane.text = failedMessage;
                    break;

                case AuthError.MissingPassword:
                    failedMessage += "your password is missing";
                    ErrorMessagePane.text = failedMessage;
                    break;

                default:
                    failedMessage += "we could not found user";
                    ErrorMessagePane.text = failedMessage;
                    break;
                    
            }

            
            Debug.Log(failedMessage);
        }

        else
        {
            if(!deleteUser)
            {
                if (Auth.CurrentUser.IsEmailVerified)
                {
                    User = LoginTask.Result.User;

                    Debug.Log("{0} You are successfully logged In: " + User.DisplayName);


                    if (!isLoading)
                    {
                        isLoading = true;
                        LoadScene("Menu");

                    }

                }
                else
                {
                    SendVerificationEmail();
                }
                
            }


            else
            {
                User = LoginTask.Result.User;

                Debug.Log("{0} You are successfully logged In: " + User.DisplayName);
            }


        }
    }


    //Send verification
   void SendVerificationEmail()
    {
        StartCoroutine(SendVerificationEmailAsync());
    }


    IEnumerator SendVerificationEmailAsync()
    {
        if(User != null)
        {
            var emailVerifyTask = User.SendEmailVerificationAsync();

            yield return new WaitUntil(() => emailVerifyTask.IsCompleted);

            if (emailVerifyTask.Exception != null)
            {
                FirebaseException firebaseException = emailVerifyTask.Exception.GetBaseException() as FirebaseException;

                AuthError authError = (AuthError)firebaseException.ErrorCode;

                string failMessage = "Failure! Verification could not complete, Please try again.";

                switch (authError)
                {
                    case AuthError.Cancelled:
                        failMessage = "Failure! verification cancelled";
                        ErrorMessagePane.text = failMessage;
                        break;

                    case AuthError.TooManyRequests:
                        failMessage = "Failure! too many messages";
                        ErrorMessagePane.text = failMessage;
                        break;

                    case AuthError.InvalidRecipientEmail:
                        failMessage = "Failure! verification mail could not be sent because email is invalid";
                        ErrorMessagePane.text = failMessage;
                        break;
                }

                Debug.Log(failMessage + " " + authError);
               
                updateUIForEmailVerification(false, null, failMessage);
            }

            else
            {
                updateUIForEmailVerification(true, User.Email, null);
            }
        }
    }

    void updateUIForEmailVerification(bool isEmailSent, string emailId, string errorMessage)
    {
        EmailVerifyPanel.SetActive(true);

        if (isEmailSent)
        {
            EmailMessageText.text = "Email sent successfully! please log into your and click the verification link at " + emailId;
        }

        else
        {
            EmailMessageText.text = "Email sending failed! please try again " + errorMessage;
        }
    }


    //Delete the user

    /*public void DeleteUser()
    {
        StartCoroutine(DeleteUserAsync());
    }

    IEnumerator DeleteUserAsync()
    {
       // User = User.
    }
    */


    //Reset Password

    /*  void ResetPassword()
      {
          StartCoroutine(PasswordReset(ResetEmail.text));
      }

      IEnumerator PasswordReset(string email)
      {
          var PasswordResetTask = User.UpdatePasswordAsync()
      }
    */

    // Start is called before the first frame update
    void Start()
    {
       
        //Debug.Log(User.DisplayName);
    }

    

    // Update is called once per frame
    void Update()
    {
        //Log(User);
    }
/*
    public void CreateAccount()
    {
        PlayerPrefs.SetString("Username", Email.text);
        PlayerPrefs.SetString("Password", Password.text);
        CreatePanel.SetActive(false);
        HomePanel.SetActive(true);
    }
*/
    /*
    public void Login()
    {

        if (!isLoading)
        {
            if (LoginEmail.text == PlayerPrefs.GetString("Username") && LoginPassword.text == PlayerPrefs.GetString("Password"))
            {
                isLoading = true;
                LoadScene("Menu");
            }

        }
    }
    */
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
