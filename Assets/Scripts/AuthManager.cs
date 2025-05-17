using UnityEngine;

public class AuthManager : MonoBehaviour
{
    public async void SignUp(string email, string password)
    {
        try
        {
            var userCredential = await FirebaseInit.auth.CreateUserWithEmailAndPasswordAsync(email, password);
            FirebaseInit.user = userCredential.User;
            Debug.Log("ȸ������ ����");
        }
        catch (System.Exception e)
        {
            Debug.LogError("ȸ������ ����: " + e.Message);
        }
    }

    public async void SignIn(string email, string password)
    {
        try
        {
            var userCredential = await FirebaseInit.auth.SignInWithEmailAndPasswordAsync(email, password);
            FirebaseInit.user = userCredential.User;
            Debug.Log("�α��� ����");
        }
        catch (System.Exception e)
        {
            Debug.LogError("�α��� ����: " + e.Message);
        }
    }

    public void SignOut()
    {
        FirebaseInit.auth.SignOut();
        FirebaseInit.user = null;
        Debug.Log("�α׾ƿ� �Ϸ�");
    }
}
