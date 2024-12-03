using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utility
{
    public class QueryUtility
    {
        public string GetQueryGetAll()
        {
            return "select * from Products";
        }

        public string GetQueryById()
        {
            return @"select Id,PhoneName,PhoneColor,PhoneIMEI,PhoneIsGlobal,PhoneBattery,PhoneChip,IsExisting
                   from Products
                   where Id = @id";

        }
        public string GetQueryByName()
        {
            return @"select Id,PhoneName,PhoneColor,PhoneIMEI,PhoneIsGlobal,PhoneBattery,PhoneChip,IsExisting
                   from Products
                   where PhoneName = @PhoneName";

        }
        public string GetQueryInsert()
        {
            return @"insert Products(PhoneName,PhoneColor,PhoneBattery,PhoneChip,PhoneIMEI,PhoneIsGlobal,IsExisting)
                   values (@PhoneName,@PhoneColor,@PhoneBattery,@PhoneChip,@PhoneIMEI,@PhoneIsGlobal,@IsExisting)";

        }

        public string GetQueryUpdate()
        {
            return @"update Products
                   SET PhoneName = @PhoneName, PhoneColor = @PhoneColor, PhoneIMEI = @PhoneIMEI, PhoneIsGlobal = @PhoneIsGlobal, PhoneBattery = @PhoneBattery, PhoneChip = @PhoneChip, IsExisting = @IsExisting
                   where Id = @Id";
        }
        public string GetQueryDelete()
        {
            return @"Delete Products
                   where Id = @Id";
        }
    }
}
