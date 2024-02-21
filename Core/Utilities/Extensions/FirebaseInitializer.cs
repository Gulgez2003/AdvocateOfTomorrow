public static class FirebaseInitializer
{
    public static void Initialize()
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        string relativeFilePath = "C:/Users/User/Downloads/advocateoftomorrow-firebase-adminsdk-tf8jw-08a43c06d8";

        string absoluteFilePath = Path.Combine(currentDirectory, relativeFilePath);

        if (File.Exists(absoluteFilePath))
        {
            var firebaseCredential = GoogleCredential.FromFile("C:/Users/User/Downloads/advocateoftomorrow-firebase-adminsdk-tf8jw-08a43c06d8.json");
             FirebaseApp.Create(new AppOptions
            {
                Credential = firebaseCredential
            });
        }
    }
}