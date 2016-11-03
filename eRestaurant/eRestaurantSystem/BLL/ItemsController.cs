using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel;
using eRestaurantSystem.DAL;
using eRestaurantSystem.Data.Entities;
using eRestaurantSystem.Data.POCOs;
using eRestaurantSystem.Data.DTOs;
#endregion

namespace eRestaurantSystem.BLL
{
    [DataObject]
    public class ItemsController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<MenuCategoryFoodItemDTO> MenuCategoryFoodItemsDTO_Get()
        {
            using (var context = new eRestaurantContext())
            {
                var results = from food in context.Items
                              group food by new { food.MenuCategory.Description } into tempdataset
                              select new MenuCategoryFoodItemDTO
                              {
                                  MenuCategoryDescription = tempdataset.Key.Description,

                                  FoodItems = (from x in tempdataset
                                               select new FoodItemCounts
                                               {
                                                   ItemID = x.ItemID,
                                                   FoodDescription = x.Description,
                                                   CurrentPrice = x.CurrentPrice,
                                                   TimesServed = x.BillItems.Count()
                                               }).ToList()
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategoryFoodItemPOCO> MenuCategoryFoodItemsPOCO_Get()
        {
            using (var context = new eRestaurantContext())
            {
                var results = from food in context.Items
                              orderby food.MenuCategory.Description
                              select new MenuCategoryFoodItemPOCO
                              {

                                  MenuCategoryDescription = food.MenuCategory.Description,
                                  ItemID = food.ItemID,
                                  FoodDescription = food.Description,
                                  CurrentPrice = food.CurrentPrice,
                                  TimesServed = food.BillItems.Count()
                              };
                return results.ToList();
            }
        }
    }
}
