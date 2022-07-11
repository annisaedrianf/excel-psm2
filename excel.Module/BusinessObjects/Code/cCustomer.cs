// Class Name : cCustomer.cs 
// Project Name : EXELS 
// Last Update : 4/19/2022 3:14:30 PM  
 
// Gen Code Version : 1.0.0.0  
 
// Revise ==> Revice date : 4/19/2022 3:14:30 PM 
 // Updated :   
//======================================================================== 
 
using System; 
using DevExpress.Xpo; 
using DevExpress.Persistent.Base; 
using System.ComponentModel; 
using DevExpress.Persistent.Validation; 
using DevExpress.ExpressApp.DC; 
using DevExpress.ExpressApp.ConditionalAppearance; 
using DevExpress.ExpressApp; 
using DevExpress.Data.Filtering; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;

namespace exels.Module.BusinessObjects 
{ 
   [DefaultClassOptions] 
   [ImageName("ModelEditor_Views")] 
   [DefaultProperty("customer_name")]
   [NavigationItem("Data")]
   // Standard Document
   [System.ComponentModel.DisplayName("Customer")]
   public class cCustomer : XPObject
   {
     public cCustomer(Session session) : base(session) 
     {
       // This constructor is used when an object is loaded from a persistent storage.
       // Do not place any code here.
     }
     public override void AfterConstruction()
     {
       base.AfterConstruction();
       // Place here your initialization code.
       //SecuritySystem.CurrentUserName
       //LastUpdateUser = Session.GetObjectByKey<GPUser>(SecuritySystem.CurrentUserId);
       string tUser = SecuritySystem.CurrentUserName.ToString();
            //LastUpdateUser = Session.FindObject<GPUser>(new BinaryOperator("UserName", SecuritySystem.CurrentUserName.ToString())); 
            // LastUpdateUser = Session.FindObject<GPUser>(new BinaryOperator("UserName", tUser)); 
            // LastUpdate = DateTime.Now; 
            id_customer = Number();
        } 
     protected override void OnSaving()
     {
       base.OnSaving();
     } 
     protected override void OnSaved()
     {
       base.OnSaved();
     } 
     protected override void OnDeleting()
     {
       base.OnDeleting();
     } 
     protected override void OnDeleted()
     {
       base.OnDeleted();
     } 
     public void Sync()
     {
     } 
     [Appearance("VisiblecCustomerOID", Visibility = ViewItemVisibility.Hide)] 
     public int Oid 
     { 
         get { return base.Oid; }
         set { base.Oid = value; }
     }
     // 
     // Notes for cCustomer : 
        private String _id_customer;
        [XafDisplayName("Customer's ID"), ToolTip("Customer's ID")]
        [Appearance("cCustomerid_customer", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String id_customer
        {
            get { return _id_customer; }
            set { SetPropertyValue(nameof(id_customer), ref _id_customer, value); }
        }

        public virtual string Number()
        {
            string sNumer = "";
            int sRun = 1;
            XPCollection<cCustomer> xpDM = new XPCollection<cCustomer>(Session);
            //string sNumberMax = (string)xpDM.Max(x => x.NomorDM)
            string sNumberMax = "";
            try
            {
                sNumberMax = xpDM
               //SelectMany(c => c.).

               .Max(o => o.id_customer.Trim());
            }
            catch (Exception e)
            {
                sNumberMax = "";
            }
            if (sNumberMax != null)
            {
                try

                {
                    sRun = System.Convert.ToInt32(sNumberMax) + 1;
                }
                catch (Exception e)
                {
                    sNumberMax = "";
                }
            }
            sNumer = $"{sRun.ToString("00000")}";

            return sNumer;
        }
        // 
        // Notes for cCustomer : 
        private string _customer_name; 
     [XafDisplayName("Name"), ToolTip("Customer's Name")] 
     // [Appearance("cCustomercustomer_name", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(100)] 
     public  string customer_name
     { 
       get { return _customer_name; } 
       set { SetPropertyValue(nameof(customer_name), ref _customer_name, value); } 
     } 
     // 
     // Notes for cCustomer : 
     private string _customer_phone; 
     [XafDisplayName("Phone Number"), ToolTip("Customer's Phone Number")] 
     // [Appearance("cCustomercustomer_phone", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(15)] 
     public  string customer_phone
     { 
       get { return _customer_phone; } 
       set { SetPropertyValue(nameof(customer_phone), ref _customer_phone, value); } 
     } 
     // 
     // Notes for cCustomer : 
     private string _customer_email; 
     [XafDisplayName("Email Address"), ToolTip("Customer's Email")] 
     // [Appearance("cCustomercustomer_email", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(100)] 
     public  string customer_email
     { 
       get { return _customer_email; } 
       set { SetPropertyValue(nameof(customer_email), ref _customer_email, value); } 
     } 
     // 
     // Notes for cCustomer : 
     private string _customer_address; 
     [XafDisplayName("Home Address"), ToolTip("Customer's Address")] 
     // [Appearance("cCustomercustomer_address", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(255)] 
     public  string customer_address
     { 
       get { return _customer_address; } 
       set { SetPropertyValue(nameof(customer_address), ref _customer_address, value); } 
     } 
   } 
} 
