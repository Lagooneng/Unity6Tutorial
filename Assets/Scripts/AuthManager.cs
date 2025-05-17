using UnityEngine;

public class AuthManager : MonoBehaviour
{
    public async void SignUp(string email, string password)
    {
        try
        {
            var userCredential = await FirebaseInit.auth.CreateUserWithEmailAndPasswordAsync(email, password);
            FirebaseInit.user = userCredential.User;
            Debug.Log("회원가입 성공");
        }
        catch (System.Exception e)
        {
            Debug.LogError("회원가입 실패: " + e.Message);
        }
    }

    public async void SignIn(string email, string password)
    {
        try
        {
            var userCredential = await FirebaseInit.auth.SignInWithEmailAndPasswordAsync(email, password);
            FirebaseInit.user = userCredential.User;
            Debug.Log("로그인 성공");
        }
        catch (System.Exception e)
        {
            Debug.LogError("로그인 실패: " + e.Message);
        }
    }

    public void SignOut()
    {
        FirebaseInit.auth.SignOut();
        FirebaseInit.user = null;
        Debug.Log("로그아웃 완료");
    }
}
