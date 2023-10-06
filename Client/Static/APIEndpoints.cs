namespace Client.Static
{
    internal static class APIEndpoints
    {
#if DEBUG
        //DO THING//
        internal const string ServerBaseUrl = "https://localhost:5003";
#else
        //DO PROD THING//
        internal const string ServerBaseUrl = "https://apiserver.ootb.uk";
#endif

		internal readonly static string s_categories = $"{ServerBaseUrl}/api/categories";
		internal readonly static string s_categoriesWithPosts = $"{ServerBaseUrl}/api/categories/withposts";
		internal readonly static string s_posts = $"{ServerBaseUrl}/api/posts";
		internal readonly static string s_postsDTO = $"{ServerBaseUrl}/api/posts/dto";
		internal readonly static string s_imageUpload = $"{ServerBaseUrl}/api/imageupload";
		internal readonly static string s_signIn = $"{ServerBaseUrl}/api/signin";
	}
}
