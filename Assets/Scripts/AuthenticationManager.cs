using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AuthenticationManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button registerButton;
    public Button loginButton;
    public TMP_Text feedbackText;

    private List<User> users = new List<User>();

    void Start()
    {
        registerButton.onClick.AddListener(Register);
        loginButton.onClick.AddListener(Login);
    }

    void Register()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Nickname and Password are required.";
            return;
        }

        // Check for uniqueness
        foreach (var user in users)
        {
            if (user.username == username)
            {
                feedbackText.text = "Nickname already exists.";
                return;
            }
            if (user.password == password)
            {
                feedbackText.text = "Password already used.";
                return;
            }
        }

        // Create new user
        users.Add(new User(username, password));
        feedbackText.text = $"User '{username}' registered successfully.";
        ClearInputs();
    }

    void Login()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text.Trim();

        foreach (var user in users)
        {
            if (user.username == username)
            {
                if (user.password == password)
                {
                    feedbackText.text = $"Login successful. Welcome, {username}!";
                    SessionManager.Instance.currentUser = user;
                    ClearInputs();
                    SceneManager.LoadScene("WorldSelection");
                    return;
                }
                else
                {
                    feedbackText.text = "Incorrect password.";
                    return;
                }
            }
        }

        feedbackText.text = "User does not exist.";
    }

    void ClearInputs()
    {
        usernameInput.text = "";
        passwordInput.text = "";
    }
}
