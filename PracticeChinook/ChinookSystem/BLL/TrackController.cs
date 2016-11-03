using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces
using System.ComponentModel; //ODS
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        //Select that will return the entire collection
        //of records for the Entity, all attributes

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Track> ListTracks()
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.ToList();
            }
        }
        
        //Add which will insert a new instance into the table collection
        [DataObjectMethod(DataObjectMethodType.Insert,true)]
        public void AddTrack(Track trackinfo)
        {
            using (var context = new ChinookContext())
            {
                
                //review the iif
                trackinfo.Composer = string.IsNullOrEmpty(trackinfo.Composer) ? 
                    null : trackinfo.Composer;

                //add
                context.Tracks.Add(trackinfo);

                //save the changes
                context.SaveChanges();
            }
        }

        //Update which will update an existing instance in the table collection
        [DataObjectMethod(DataObjectMethodType.Update,true)]
        public void UpdateTrack(Track trackinfo)
        {
            using (var context = new ChinookContext())
            {
              

                //review the iif
                trackinfo.Composer = string.IsNullOrEmpty(trackinfo.Composer) ?
                    null : trackinfo.Composer;

                //update
               context.Entry(trackinfo).State =
                    System.Data.Entity.EntityState.Modified;

                //save the changes
                context.SaveChanges();
            }
        }
        //Delete which will remove an existing instance from the table collection
        [DataObjectMethod(DataObjectMethodType.Delete,true)]
        public void DeleteTrack(Track trackinfo)
        {
            DeleteTrack(trackinfo.TrackId);
        }

        public void DeleteTrack(int trackid)
        {
            using (var context = new ChinookContext())
            {
                //find the existing instance from the table
                var existing = context.Tracks.Find(trackid);

                //reemove the instance
                context.Tracks.Remove(existing);

                //save
                context.SaveChanges();
            }
        }
    }
}
