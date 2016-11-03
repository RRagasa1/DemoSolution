using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace ChinookSystem.Data.Entities
{
    //point to the sql table that this file maps
    [Table("Tracks")]
    public class Track
    {
        //Key notations is optional if the sql pkey
        //ends in ID or Id
        //required if default of entity is NOT Identity
        //required if pkey is compound

        //properties can be fully implemented or
        //auto implemented
        //property names should use sql attribute name
        //properties should be listed in the same order
        //     as sql table attributes for easy of maintenance

        //validation annotation can go on each field in your
        //entity
        //validation of Entity kicks in on the .SaveChanges() statement
        //these validation annotations generate an Exception

        //[Required(ErrorMessage="xxx")]
        //[StringLength(intmax,[intmin,]ErrorMessage="xxxxx")]
        //[Range(doublemin, doublemax,ErrorMessage="xxxxxx")]
        //[RegularExpression("expression",ErrorMessage="xxxx")]
        [Key]
        public int TrackId { get; set; }
        [Required(ErrorMessage ="Name is a required field.")]
        [StringLength(200,ErrorMessage ="Name is too long. Max length is 200 characters")]
        public string Name { get; set; }
        [Range(1,double.MaxValue,ErrorMessage ="Album is invalid. Attempt selection again.")]
        public int? AlbumId { get; set; }
        [Required(ErrorMessage = "Media Type is a required field.")]
        [Range(1, double.MaxValue, ErrorMessage = "Media Type is invalid. Attempt selection again.")]
        public int MediaTypeId { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Genre is invalid. Attempt selection again.")]
        public int? GenreId { get; set; }
        [StringLength(220, ErrorMessage = "Composer is too long. Max length is 220 characters")]
        public string Composer { get; set; }
        [Required(ErrorMessage = "MSec is a required field.")]
        [Range(1, double.MaxValue, ErrorMessage = "MSec is invalid. Must be greater than 0.")]
        public int Milliseconds { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Bytes is invalid. Must be greater than 0.")]
        public int? Bytes { get; set; }
        [Required(ErrorMessage = "Price is a required field.")]
        [Range(1, double.MaxValue, ErrorMessage = "Price is invalid. Must be 0 or greater.")]
        public decimal UnitPrice { get; set; }

        //navigation properties for use by Linq
        //these properties will be of type vitural
        //there are two types of navigation properties
        //properties that point to "children" use ICollection<T>
        //properties that point to "Parent" use ParentName as the datatype
        public virtual MediaType MediaType { get; set; }
        public virtual Album Album { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }

        //Tracks may be on one or more PlayList. Each PlayList has one or more Tracks
        //this many to many relationship was normalized using a table called PlaylistTracks
        //We can simplify our model by using navigation properties to directly
        //    represent our many-to-many relationship and thereby omit having to
        //    create a PlaylistTrack entity
        //The navigation property set would be as  "children" 

        //Modeling of this relationship will be done in the context class
        public virtual ICollection<PlayList> PlayLists { get; set; }
    }
}