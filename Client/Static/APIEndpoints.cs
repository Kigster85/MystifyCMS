namespace Client.Static
{
    internal static class APIEndpoints
    {
#if DEBUG
        //DO THING//
        internal const string ServerBaseUrl = "https://localhost:5003";
#else
        //DO PROD THING//
        internal const string ServerBaseUrl = "https://mystifyserver.azurewebsites.net";
#endif

        internal readonly static string s_categories = $"{ServerBaseUrl}/api/categories";
    }
}
