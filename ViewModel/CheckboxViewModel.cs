using System.ComponentModel.DataAnnotations;

namespace AssetsPro.ViewModel
{
    public class CheckboxViewModel
    {
        [Required, MaxLength(255)]
        public string DisplayVlue { get; set; }
        public bool IsSelected {  get; set; }
    }
}
