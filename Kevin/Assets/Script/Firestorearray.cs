using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Firestore;



[FirestoreData]
public class Info
{
    [FirestoreProperty]
    public string Namn { get; set; }

    [FirestoreProperty]
    public string Adress { get; set; }

    [FirestoreProperty]
    public string Ort { get; set; }


}


[FirestoreData]
public class Kundbehov
{
    [FirestoreProperty]
    public string Onskemal { get; set; }

    [FirestoreProperty]
    public string Material { get; set; }

    [FirestoreProperty]
    public string Deadline { get; set; }


}




[FirestoreData]
public class Anstalld
{
    [FirestoreProperty]
    public string Anstallningsnr { get; set; }

    [FirestoreProperty]
    public string Semesterdagar { get; set; }

    [FirestoreProperty]
    public string Kundansvar { get; set; }


}



[FirestoreData]
public class Ansvar
{
    [FirestoreProperty]
    public string Kund { get; set; }

  

}