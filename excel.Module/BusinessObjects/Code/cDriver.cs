// Class Name : cDriver.cs 
// Project Name : EXELS 
// Last Update : 4/19/2022 3:13:51 PM  
 
// Gen Code Version : 1.0.0.0  
 
// Revise ==> Revice date : 4/19/2022 3:13:51 PM 
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
   [DefaultProperty("driver_name")]
   [NavigationItem("Data")]
   // Standard Document
   [System.ComponentModel.DisplayName("Driver")]
   public class cDriver : XPObject
   {
     public cDriver(Session session) : base(session) 
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
            id_driver = Number();
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
     [Appearance("VisiblecDriverOID", Visibility = ViewItemVisibility.Hide)] 
     public int Oid 
     { 
         get { return base.Oid; }
         set { base.Oid = value; }
     }
        // 
        // Notes for cDriver : 
        private String _id_driver;
        [XafDisplayName("Driver's ID"), ToolTip("Driver's ID")]
        [Appearance("cDriverid_driver", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String id_driver
        {
            get { return _id_driver; }
            set { SetPropertyValue(nameof(id_driver), ref _id_driver, value); }
        }

        public virtual string Number()
        {
            string sNumer = "";
            int sRun = 1;
            XPCollection<cDriver> xpDM = new XPCollection<cDriver>(Session);
            //string sNumberMax = (string)xpDM.Max(x => x.NomorDM)
            string sNumberMax = "";
            try
            {
                sNumberMax = xpDM
               //SelectMany(c => c.).

               .Max(o => o.id_driver.Trim());
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
        // Notes for cDriver : 
        private string _driver_name; 
     [XafDisplayName("Name"), ToolTip("Driver's Name")] 
     // [Appearance("cDriverdriver_name", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(100)] 
     public  string driver_name
     { 
       get { return _driver_name; } 
       set { SetPropertyValue(nameof(driver_name), ref _driver_name, value); } 
     } 
     // 
     // Notes for cDriver : 
     private string _driver_phone; 
     [XafDisplayName("Phone Number"), ToolTip("Driver's Phone")] 
     // [Appearance("cDriverdriver_phone", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(15)] 
     public  string driver_phone
     { 
       get { return _driver_phone; } 
       set { SetPropertyValue(nameof(driver_phone), ref _driver_phone, value); } 
     } 
     // 
     // Notes for cDriver : 
     private string _driver_license_number; 
     [XafDisplayName("Driver's ID/License Number"), ToolTip("Driver's ID/License Number")] 
     // [Appearance("cDriverdriver_license_number", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(50)] 
     public  string driver_license_number
     { 
       get { return _driver_license_number; } 
       set { SetPropertyValue(nameof(driver_license_number), ref _driver_license_number, value); } 
     } 
     // 
     // Notes for cDriver : 
     private string _truck_number; 
     [XafDisplayName("Truck Plates"), ToolTip("Truck Plates")] 
     // [Appearance("cDrivertruck_number", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(50)] 
     public  string truck_number
     { 
       get { return _truck_number; } 
       set { SetPropertyValue(nameof(truck_number), ref _truck_number, value); } 
     } 
   } 
} 
