using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
public static class FirebaseHelper
{
    private static bool _isInitialized = false;
    public static void InitializeFirebase(string credentialPath)
    {
        if (!_isInitialized)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(credentialPath)
            });
            _isInitialized = true;
        }
    }

    public static async Task<string> SendFCM(string deviceToken, string title, string body)
    {
        var message = new Message()
        {
            Token = deviceToken,
            Notification = new Notification
            {
                Title = title,
                Body = body
            }
        };

        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        return response;
    }
}