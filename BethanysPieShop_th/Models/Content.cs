using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop_th.Models
{
    [Table("New")]
    public class Content
    {
        [Key]
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your Titale")]
        [Display(Name = "Titale")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter your ShortDescription")]
        [Display(Name = "ShortDescription")]
        [StringLength(100)]
        public String ShortDescription { get; set; }
        [Required(ErrorMessage = "Please enter your Description")]
        [Display(Name = "Description")]
        [StringLength(100)]
        public string Description { get; set; }
        
        public string UrlImage { get; set; }

        //public FileStyleUriParser fileUpload { get; set; }

        public DateTime CreatedDate { get; set; }
      
    }
}
