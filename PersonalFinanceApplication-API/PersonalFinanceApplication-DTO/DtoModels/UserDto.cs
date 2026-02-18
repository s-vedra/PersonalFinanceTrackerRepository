namespace PersonalFinanceApplication_DTO.DtoModels
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<UserContractDto> UserContracts { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }
}
