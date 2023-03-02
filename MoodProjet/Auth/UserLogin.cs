namespace MoodProjet.Auth
{
    public record UserLogin(string Login, string Password);

    public record UserLoginResult(int Id, string Login, bool IsLoginOK, bool CanAdminDevices, bool CanAdminMoodFaces, bool CanAdminMoodEntries, bool CanSeeCharts);
}