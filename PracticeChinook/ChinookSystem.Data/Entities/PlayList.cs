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
    public class PlayList
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }

        //Tracks may be on one or more PlayList. Each PlayList has one or more Tracks
        //this many to many relationship was normalized using a table called PlaylistTracks
        //We can simplify our model by using navigation properties to directly
        //    represent our many-to-many relationship and thereby omit having to
        //    create a PlaylistTrack entity
        //The navigation property set would be as  "children" 

        //Modeling of this relationship will be done in the context class
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
