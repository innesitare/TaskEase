namespace TaskEase.Core.Helpers;

public static class CacheKeys
{
    public static class User
    {
        public static string GetAll => "users-all";
        public static string Get(string id) => $"users-{id}";
        public static string GetByEmail(string email) => $"users-email-{email}";
    }

    public static class BoardTask
    {
        public static string GetAll => "board-tasks-all";
        public static string Get(string id) => $"board-tasks-{id}";
    }
}