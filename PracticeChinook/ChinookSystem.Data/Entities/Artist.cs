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
    //[Table("sqltablename")]
    [Table("Artists")]
    public class Artist
    {
        //pkey annotation is optional
        //default assumes identity sql table
        //default assumes property name ends with ID or Id
        //   and sql pkey ends the same
        //otherwise [Key] is need
        //non-identity pkeys, compound pkeys, pkeys not
        // ending in ID or Id

        //class properties map to sql table attributes
        //properties should be named the same as table attributes
        //properties should be in the same order as
        //    table attributes for ease of maintenance
        [Key]
        public int ArtistId { get; set; }
        public string Name { get; set; }

        //navigation properties for use by Linq
        //use the DB ERD to determine the relationships

        //these properties wqill be of type  virtual
        //there are two types of navigation properties
        //properties that point to "children" 
        //     use ICollection<T> as the datatype
        //properties that point to "parent"
        //     use ParentClassName as the datatype

        public virtual ICollection<Album> Albums { get; set; }

    }
}
