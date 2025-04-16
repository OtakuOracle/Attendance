namespace presence_api.Response
{
    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserResponse>? Users { get; set; } = null;
    }
}
