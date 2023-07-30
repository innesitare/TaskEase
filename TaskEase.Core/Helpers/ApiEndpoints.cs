namespace TaskEase.Core.Helpers;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Authentication
    {
        private const string Base = $"{ApiBase}/auth";

        public const string Register = $"{Base}/register";
        public const string Login = $"{Base}/login";
    }
    
    public static class User
    {
        private const string Base = $"{ApiBase}/users";
        
        public const string GetAll = $"{Base}";
        public const string GetByEmail = $"{Base}/{{email}}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class BoardTask
    {
        private const string Base = $"{ApiBase}/board-tasks";
        
        public const string GetAll = $"{Base}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{taskId:guid}}/{{userId:guid?}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
}