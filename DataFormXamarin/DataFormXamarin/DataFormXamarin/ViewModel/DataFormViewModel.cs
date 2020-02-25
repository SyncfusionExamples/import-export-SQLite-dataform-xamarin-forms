using Syncfusion.XForms.DataForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace DataFormXamarin
{
    public class DataFormViewModel : INotifyPropertyChanged
    {
        private ContactsInfo contacts;
        public ContactsInfo Contact
        {
            get { return this.contacts; }
            set {
                this.contacts = value;
                this.RaisePropertychanged("Contact");
            }
        }

        public Command SaveCommand { get; set; }

        public DataFormViewModel()
        {
            ContactsInfo contact = new ContactsInfo();
            contact.Name = "Jhon";
            contact.ContactNumber = 12345;
            contact.Email = "abc@mail.com";
            contact.BirthDate = new DateTime(1990, 01, 01);

            App.Database.SaveItemAsync(contact);
            this.GetDataModel();

            SaveCommand = new Command<SfDataForm>(SaveInputValues);
        }

        /// <summary>
        /// Gets the data from database
        /// </summary>
        private async void GetDataModel()
        {
            var dataObject = await App.Database.GetItemsAsync();
            this.Contact = dataObject.FirstOrDefault();
        }

        /// <summary>
        /// Get input values from dataform and save into database
        /// </summary>
        /// <param name="dataForm"></param>
        private void SaveInputValues(SfDataForm dataForm)
        {
            var dataObject = dataForm.DataObject as ContactsInfo;
            App.Database.SaveItemAsync(dataObject);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertychanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
