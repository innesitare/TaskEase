namespace TaskEase.Core.Helpers;

public static class SendEndpoints
{
    private const string EndpointBase = "amqp://localhost:5672";

    public static class Authentication
    {
        public static readonly Uri Create = new($"{EndpointBase}/create-user");
    }
    
    public static class User
    {
        public static readonly Uri Update = new($"{EndpointBase}/update-application-user");
        public static readonly Uri Delete = new($"{EndpointBase}/delete-application-user");
    }
}