namespace AssetsPro.ViewModel
{
    public class PermissionFormViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<CheckboxViewModel> RoleClaims { get; set; }
    }
}
