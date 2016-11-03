using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities; //entity classes
using ChinookSystem.Data.POCOs;     //POCOs classes
using ChinookSystem.DAL;            //context class
using System.ComponentModel;        //ODS
#endregion
namespace ChinookSystem.BLL
{
    [DataObject]
    public class ArtistController
    {
        //dump the entire artist table
        //this will use EntityFramework access
        //Entity classes will be used to define the data

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Artist> Artist_ListAll()
        {
            //set up the transaction area
            using (var context = new ChinookContext())
            {
                return context.Artists.ToList();
            }
        }

        //return a list of artists and all their albums
        //this will use Linq to Entity data acess
        //POCO classes will be used to define the data
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<ArtistAlbums> ArtistAlbums_Get(int releaseyear)
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Albums
                              where x.ReleaseYear == releaseyear
                              orderby x.Artist.Name, x.Title
                              select new ArtistAlbums
                              {
                                  Name = x.Artist.Name,
                                  Title = x.Title
                              };
                //the .ToList will actually cause the query
                //to execute
                return results.ToList();
            }
        }
    }
}
