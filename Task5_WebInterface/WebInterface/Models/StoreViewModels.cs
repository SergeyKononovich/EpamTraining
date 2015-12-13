using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebInterface.Models
{
    public class JQueryDataTableParamViewModel
    {
        // Request sequence number sent by DataTable, 
        // same value must be returned in response
        public string sEcho { get; set; }
        // First record that should be shown(used for paging)
        public int iDisplayStart { get; set; }
        // Number of records that should be shown in table
        public int iDisplayLength { get; set; }
    }

    public class SaleFilterViewModel
    {
        [MaxLength(40)]
        public string sSearch { get; set; }
        
        public int FromId { get; set; } = int.MinValue;

        public int ToId { get; set; } = int.MaxValue;

        public DateTime FromDate { get; set; } = DateTime.MinValue;

        public DateTime ToDate { get; set; } = DateTime.MaxValue;

        [MaxLength(20)]
        [DisplayName("GoodsNamePart")]
        public string GoodsNamePart { get; set; }

        [MaxLength(20)]
        public string ManagerSecondNamePart { get; set; }

        [MaxLength(20)]
        public string ClientFullNamePart { get; set; }
        
        public int FromPrice { get; set; } = int.MinValue;

        public int ToPrice { get; set; } = int.MaxValue;
    }

    public class AddSaleViewModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Length must be in range [3,30]")]
        public string Manager { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Length must be in range [3,30]")]
        public string Client { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Length must be in range [3,30]")]
        public string Goods { get; set; }

        [Required]
        [DisplayName("Selling price")]
        public int SellingPrice { get; set; }
    }
}