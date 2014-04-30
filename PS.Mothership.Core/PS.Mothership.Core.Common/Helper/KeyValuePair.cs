﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PS.Mothership.Core.Common.Helper
{
    public class KeyValuePair
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public static List<KeyValuePair> ListFrom<T>()
        {
            var array = (T[])(Enum.GetValues(typeof(T)).Cast<T>());
            return array
              .Select(a => new KeyValuePair
              {
                  Key = a.ToString(),
                 // Name = a.ToString().SplitCapitalizedWords(),
                  Value = Convert.ToInt32(a)
              })
                .OrderBy(kvp => kvp.Name)
               .ToList();

        }

       /* private ICollection<dynamic> GetUserStatusOptionCollection()
        {
            var data = new Collection<dynamic>();
            var result = (Enum.GetValues(typeof(StatusOptionFlagEnum)).Cast<long>().Select(d => new
            {
                RoleGuid = d,
                Name = Enum.GetName(typeof(StatusOptionFlagEnum), d)
            })).ToList();

            // Add to collection
            data.AddRange(result);
            return data;
        }        */
      
    }

  /*  public class SomeClass
    {
        private List<SomeClass> _list; 
        public int RoleGuid { get; set; }
        public string Name { get; set; }
        public bool IsLoginEnabled { get; set; }
        public List<SomeClass> list
        {
            get
            {
                if (_list == null)
                {
                    _list = new List<SomeClass>
                    {
                        new SomeClass
                        {
                        RoleGuid = 0,
                        IsLoginEnabled = true,
                        Name = "test"
                        },
                        new SomeClass()
                        {

                        }
                    };
                }
                
                return _list;
            }
        }
    }*/
}
