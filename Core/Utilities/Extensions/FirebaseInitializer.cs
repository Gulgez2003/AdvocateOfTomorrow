public static class FirebaseInitializer
{
    public static void Initialize()
    {
        string serviceAccountKeyPath = "C:/Users/User/Downloads/advocateoftomorrow-firebase-adminsdk-tf8jw-08a43c06d8.json";

        if (File.Exists(serviceAccountKeyPath))
        {
            var firebaseCredential = GoogleCredential.FromFile(serviceAccountKeyPath);
            FirebaseApp.Create(new AppOptions
            {
                Credential = firebaseCredential
            });
        }
        else
        {
            Console.WriteLine("JSON file is not found");
        }
    }
}
