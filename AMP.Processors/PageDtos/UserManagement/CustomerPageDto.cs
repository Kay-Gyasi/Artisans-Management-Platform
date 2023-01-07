namespace AMP.Processors.PageDtos.UserManagement
{
    public class CustomerPageDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserPageDto User { get; set; }
    }
}