using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPOS.Entities
{
    class StaffMember
    {
        private int staffMember_ID;
        private string firstName;
        private string middleName;
        private string lastName;
        private string displayName;
        private byte[] image;

        public int StaffMember_ID
        {
            get { return staffMember_ID; }
            set { staffMember_ID = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
