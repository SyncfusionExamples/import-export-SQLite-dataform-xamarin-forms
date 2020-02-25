using System;
using System.Collections.Generic;
using System.Text;

namespace DataFormXamarin
{
    public class ContactsInfo
    {
        private string name;
        private string middleName;
        private string lastName;
        private int contactNo;
        private string email;
        private string address;
        private DateTime? birthDate;
        private string groupName;

        public ContactsInfo()
        {

        }

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
            }
        }

        public int ContactNumber
        {
            get { return contactNo; }
            set
            {
                this.contactNo = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
            }
        }

        public DateTime? BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
            }
        }
    }

}