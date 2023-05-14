using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Core.Entities
{
    public abstract class EntityBase
    {
        /// <summary>
        /// Inentyfikator encji
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }


        /// <summary>
        /// Data utworzenia encji
        /// </summary>
        [HiddenInput]
        [Required]
        public DateTime CreationDate { get; set; }

        protected EntityBase()
            => CreationDate = DateTime.Now;
    }
}
