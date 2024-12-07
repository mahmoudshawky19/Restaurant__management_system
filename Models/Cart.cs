using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
  namespace  Models
{   [PrimaryKey("MenuItemId", "ApplicationUserId")]


public class Cart
    {
         public int MenuItemId { get; set; }
        [ForeignKey(nameof(MenuItemId))]
        [ValidateNever]
        public MenuItem MenuItem { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public int count { get; set; }

    }
}
