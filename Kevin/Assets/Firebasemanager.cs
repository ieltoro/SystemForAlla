using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;

public class Firebasemanager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public FirebaseFirestore db;

    [Header("Användare")]
    public InputField Epost, password;



    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("noppsi to firebase " + dependencyStatus);
            }
        });
    }
    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;
        print("Connected");
        
    }
    public void RegisterPressed()
    {

        auth.CreateUserWithEmailAndPasswordAsync(Epost.text, password.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            user = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                user.DisplayName, user.UserId);
            Setupdatabas();
        });
    }
    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(Epost.text,password.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);
            FindObjectOfType<CanvasManager>().ChangeUI(1);
        });
    }
    public void Setupdatabas()
    {
        DocumentReference docRef = db.Collection("kund").Document();
        Info info = new Info
        {
            Namn = "Hanna",

            Adress = "kundvagen 21",

            Ort = "Partille",

        };
        docRef.SetAsync(info);

        DocumentReference docRef2 = db.Collection("anställd").Document(user.UserId);
        Anstalld anställd = new Anstalld
        {
            Anstallningsnr = "453213",

            Semesterdagar = "42",

            Kundansvar = "Elektrobygg AB",

        };
        docRef2.SetAsync(anställd).ContinueWithOnMainThread(task =>
        {
            Ansvar ansvar = new Ansvar
            {
                Kund = "Bygg AB"


            };
            docRef2.Collection("ansvar").Document("kunder").SetAsync(ansvar);

        });
        FindObjectOfType<CanvasManager>().ChangeUI(1);
    }

    public void SearchUser(string user)
    {
        DocumentReference docRef = db.Collection("kund").Document(user);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }

    #region Update

    public void UpdateKund()
    {
        print("update kund pressed");
        DocumentReference docRef2 = db.Collection("anställd").Document(user.UserId);

        
            Ansvar ansvar = new Ansvar
            {
                Kund = "Entrepenad"


            };
            docRef2.Collection("ansvar").Document("kunder").SetAsync(ansvar);
        

    }
    #endregion

}






