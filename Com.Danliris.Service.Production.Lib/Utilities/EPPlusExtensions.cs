using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Utilities
{
    public static class EPPlusExtensions
    {
        public static ExcelRangeBase LoadFromCollectionFiltered<T>(this ExcelRangeBase @this, IEnumerable<T> collection, bool printHeader, TableStyles style) where T : class
        {
            var type = typeof(T);
            MemberInfo[] membersToInclude = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !Attribute.IsDefined(p, typeof(EpplusIgnore)))
                .ToArray();
            
            return @this.LoadFromCollection<T>(collection, printHeader,
                style,
                BindingFlags.Instance | BindingFlags.Public,
                membersToInclude);
        }
    }
}
