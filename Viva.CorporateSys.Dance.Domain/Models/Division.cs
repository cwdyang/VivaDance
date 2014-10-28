using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    /// <summary>
    /// http://www.humanworkflow.net/post/2014/01/30/Managing-Hierarchal-ParentChild-Data-With-DatabaseCode-First-Entity-Framework-And-Recursion.aspx
    /// http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/implementing-inheritance-with-the-entity-framework-in-an-asp-net-mvc-application
    /// </summary>
    public class Division: BaseObject
    { 

        public Division ()
        {
            this.Categories = new HashSet<Category>();
        }

        //one to many Division>Category
        public virtual ICollection<Category> Categories { get; set; }

    }


}
