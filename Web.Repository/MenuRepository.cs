using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnF;

namespace Web.Repository
{
  public  class MenuRepository
    {
        private static Sim4GEntities  db = new Sim4GEntities();

        public static List<Menu> GetAllMenu( )
        {       
          
            var query = db.Menus.ToList();

            if (query.Count() > 0)
                return query.ToList();
            return new List<Menu>();
        }
        public static List<Menu> GetSubMenu(Int32 id)
        {

            var query = db.Menus.Where(p=>p.ParentId==id).ToList();

            if (query.Count() > 0)
                return query.ToList();
            return new List<Menu>();
        }
        public static List<Menu> RetrieveParentMenuForCreate(int id)
        {                     //// 
            List<Menu> childMenu = RetrieveChildMenu(id, new List<Menu>());

            var query = db.Menus.ToList().Where(p => !childMenu.Any(p2 => p2.MenuId  == p.MenuId));

            if (query.Count() > 0)
                return query.ToList();
            return null;
        }

        public static List<Menu> RetrieveChildMenu(int Id, List<Menu> listmenu)
        {           
            var query = from a in db.Menus
                        where a.ParentId == Id
                        select a;

            if (query.Count() > 0)
            {
                foreach(var ch in query)
                {
                    listmenu.Add(ch);
                    RetrieveChildMenu(ch.MenuId, listmenu);
                } 
            }

            return listmenu;
        }


        public static int RetrieveChildLevel(int Id, int level)
        {

            var current = db.Menus.Find(Id);

            var parent = db.Menus.Where(p => p.MenuId == current.ParentId).ToList();


            if (parent.Count() > 0)
            {
                level++;
             return  RetrieveChildLevel(parent.First().MenuId, level);
            }

            return level;
        }

        public static bool IsNumber(string level)
        {
            try
            {
                int num= Convert.ToInt16(level);
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }

}
