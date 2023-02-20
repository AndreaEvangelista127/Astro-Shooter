using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    [Header("UI")] 
    public TextMeshProUGUI messageText;
    //username input, usato durante la registrazione
    public TMP_InputField userNameInput;
    //email e password input
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    [Header("LevelLoader")]
    public LevelLoaderScript lLoader;

    //usato per visualizzare player nella leaderboard
    string loggedInPlayFabId;


    [Header("LeaderBoard")]
    public GameObject rowPrefab;
    public Transform rowParent;

    //usato per il passaggio dal login screen al registration screen
    public void RegisterScreen()
    {
        //ricorda di mettere il nuovo comando al bottone
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackButtonLogin()
    {
        //ricorda di mettere il nuovo comando al bottone
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void BackButtonRegister()
    {
        //ricorda di mettere il nuovo comando al bottone
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BackButtonLeaderBoard()
    {
        //ricorda di mettere il nuovo comando al bottone
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    //pulsante per la registrazione, da usare nel registration screen
    public void RegisterButton() {
        var request = new RegisterPlayFabUserRequest
        {
           /*nuovo*/ Username = userNameInput.text,  //lo scopo è di creare un username per l'utente con il quale si idnetifica nella leaderboard
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterError);
        //AddContactEmailToPlayer();
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        //provo ad aggiungere la contact Email con la registrazione
        AddOrUpdateContactEmail(emailInput.text);  //aggiorna email di contatto con quella inserita
        // SubmitNameDisplayed();  // se metto questo non funziona
        messageText.text = "Email Sent! Confirm registration and log to play!";
    }

    void OnRegisterError(PlayFabError error)
    {
        Debug.Log("Error while login/create account");
        Debug.Log(error.GenerateErrorReport());
        messageText.text = "Username or Mail alredy used";
    }

    public void LoginButton()
    {   
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnErrorLogin);
    }

    void OnErrorLogin(PlayFabError error)
    {
        messageText.text = "Email or Password incorrect";
    }

    public void SubmitNameDisplayed()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userNameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        //Debug.Log("Updated display name!");
        //leaderboardWindow.SetActive(true);   //verrà usata poi per la leaderboard
    }

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "9631A"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);

    }

    void OnPasswordReset (SendAccountRecoveryEmailResult result) {
        messageText.text = "Password reset email send";
        Debug.Log("Reset Password email sent");

    }

    void OnLoginSuccess(LoginResult result)
    {
        loggedInPlayFabId = result.PlayFabId;
        messageText.text = "Logged in!";
        // string name = null;
        Debug.Log("Successful login!");

        //nuovo
        //name = result.InfoResultPayload.PlayerProfile.DisplayName;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    //NOTA QUESTA PARTE SOTTO è USATA PER I TEST PRELIMINARI PER LA LEADRBOARD


    //Start is called before the first frame update
    //void Start()
    //{
    //    Login();
    //}



    //login con ID casuale, può essere cancellato comoresa la parte sotto di Error e Success
    void Login()
    {
        var requset = new LoginWithCustomIDRequest
        {
             CustomId = SystemInfo.deviceUniqueIdentifier,  //usato per idetificare un dispositivo, se riparti il gioco però rimane sempre lo stesso
            //CustomId = "lalalalalala",
            CreateAccount = true,

            //nuovo
            /*
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {

            }
            */

        };

        PlayFabClientAPI.LoginWithCustomID(requset, OnSuccess, OnError);
    }


    ////LOGIN con random Id
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
    }

    //////////////////////////////////////////////////////////////////////////////////////////////


    //usato per errori generici
    void OnError(PlayFabError error)
        {
            Debug.Log("Error while login/create account");
        Debug.Log(error.GenerateErrorReport());
        }


    //PUNTEGGIO LEADERBOARD
    public void SendLeaderBoard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "PlatformScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }


        void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("Successfull leaderBoard sent");
        }

        public void GetLeaderBoard() {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 10,
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    //vecchio
    //void OnLeaderboardGet(GetLeaderboardResult result)
    //{
    //    foreach(var item in result.Leaderboard)
    //    {
    //        Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
    //    }
    //}

    //nuovo
    void OnLeaderboardGet(GetLeaderboardResult result)
    {

        //ogni volta che voglio visualizzare la leadrboard, si somma a quella già esistente, quindi faccio questo passagio
        //per eliminare tutta la laeadrboard precedente e ricrearla con possibii nuovi valori
        foreach (Transform item in rowParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {

            GameObject newGo = Instantiate(rowPrefab, rowParent);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();   //attenzione, è componenentS
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
        }
    }

    public void GetLeaderBoardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "PlatformScore",
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderBoardAroundPlayerGet, OnError);
    }

    void OnLeaderBoardAroundPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        //ogni volta che voglio visualizzare la leadrboard, si somma a quella già esistente, quindi faccio questo passagio
        //per eliminare tutta la laeadrboard precedente e ricrearla con possibii nuovi valori
        foreach (Transform item in rowParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {

            GameObject newGo = Instantiate(rowPrefab, rowParent);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();   //attenzione, è componenentS
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();


            //qua cambia il colore del giocatore loggato
            if(item.PlayFabId == loggedInPlayFabId)
            {
                texts[0].color = Color.cyan;
                texts[1].color = Color.cyan;
                texts[2].color = Color.cyan;
            }

            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
        }
    }



    //aggiunta dell'email di contatto, ricorda di togliere però il login automatico
    //void AddContactEmailToPlayer()
    //{
    //    var loginReq = new LoginWithCustomIDRequest
    //    {
    //        CustomId = userNameInput.text, // replace with your own Custom ID
    //        CreateAccount = true // otherwise this will create an account with that ID
    //    };

    //    var emailAddress = emailInput.text; // Set this to your own email
    //    PlayFabClientAPI.LoginWithCustomID(loginReq, loginRes =>
    //    {
    //        Debug.Log("Successfully logged in player with PlayFabId: " + loginRes.PlayFabId);
    //        AddOrUpdateContactEmail(emailAddress);   //loginRes.PlayFabId
    //    }, FailureCallback);
    //}



    void AddOrUpdateContactEmail(string emailAddress) //string playFabId
    {
        var request = new AddOrUpdateContactEmailRequest
        {
            
            //PlayFabId  = playFabId,  //perchè va disattivato ?
            EmailAddress = emailAddress
        };

        //originale

        //PlayFabClientAPI.AddOrUpdateContactEmail(request, result =>
        //{
        //    Debug.Log("The player's account has been updated with a contact email");
        //}, FailureCallback);

        PlayFabClientAPI.AddOrUpdateContactEmail(request, OnAddOrUpdateContactEmailSuccess, FailureCallback);

    }

    void OnAddOrUpdateContactEmailSuccess(AddOrUpdateContactEmailResult result)
    {
        SubmitNameDisplayed();
        Debug.Log("The player's account has been updated with a contact email");
    }


    void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

}
    

