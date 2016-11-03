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
    public class MediaTypeController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ForeignKeyList> MediaTypeList()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.MediaTypes
                              orderby x.Name
                              select new ForeignKeyList()
                              {
                                  PFKeyIdentifier = x.MediaTypeId,
                                  DisplayText = x.Name
                              };
                return results.ToList();
            }
        }
    }
}
