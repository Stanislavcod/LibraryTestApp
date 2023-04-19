namespace Library.Common.ModelsDto
{
    public class EditPasswordDto
    {
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
    }
}
