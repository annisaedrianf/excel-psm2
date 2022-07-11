// Class Name : cAdministrator.cs 
// Project Name : EXELS 
// Last Update : 4/19/2022 3:12:30 PM  
 
// Gen Code Version : 1.0.0.0  
 
// Revise ==> Revice date : 4/19/2022 3:12:28 PM 
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
   [DefaultProperty("name_admin")]
   [NavigationItem("Data")]
   // Standard Document
   [System.ComponentModel.DisplayName("Administrator")]
   public class cAdministrator : XPObject
   {
     public cAdministrator(Session session) : base(session) 
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
            id_admin = Number();
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
     [Appearance("VisiblecAdministratorOID", Visibility = ViewItemVisibility.Hide)] 
     public int Oid 
     { 
         get { return base.Oid; }
         set { base.Oid = value; }
     }
     // 
     // Notes for cAdministrator : 
        private String _id_admin;
        [XafDisplayName("Administrator's ID"), ToolTip("Administrator's ID")]
        [Appearance("cAdministratorid_admin", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String id_admin
        {
            get { return _id_admin; }
            set { SetPropertyValue(nameof(id_admin), ref _id_admin, value); }
        }

        public virtual string Number()
        {
            string sNumer = "";
            int sRun = 1;
            XPCollection<cAdministrator> xpDM = new XPCollection<cAdministrator>(Session);
            //string sNumberMax = (string)xpDM.Max(x => x.NomorDM)
            string sNumberMax = "";
            try
            {
                sNumberMax = xpDM
               //SelectMany(c => c.).

               .Max(o => o.id_admin.Trim());
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
            sNumer = $"{sRun.ToString("0")}";

            return sNumer;
        }

        // 
        // Notes for cAdministrator : 
        private string _name_admin; 
     [XafDisplayName("Administrator's name"), ToolTip("Administrator's name")] 
     // [Appearance("cAdministratorname_admin", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(100)] 
     public  string name_admin
     { 
       get { return _name_admin; } 
       set { SetPropertyValue(nameof(name_admin), ref _name_admin, value); } 
     } 
     // 
     // Notes for cAdministrator : 
     private string _phone_admin; 
     [XafDisplayName("Administrator's phone"), ToolTip("Administrator's phone")] 
     // [Appearance("cAdministratorphone_admin", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(15)] 
     public  string phone_admin
     { 
       get { return _phone_admin; } 
       set { SetPropertyValue(nameof(phone_admin), ref _phone_admin, value); } 
     } 
     // 
     // Notes for cAdministrator : 
     private string _email_admin; 
     [XafDisplayName("Administrator's email"), ToolTip("Administrator's email")] 
     // [Appearance("cAdministratoremail_admin", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(100)] 
     public  string email_admin
     { 
       get { return _email_admin; } 
       set { SetPropertyValue(nameof(email_admin), ref _email_admin, value); } 
     } 
     // 
     // Notes for cAdministrator : 
     private string _username; 
     [XafDisplayName("Administrator's username"), ToolTip("Administrator's username")] 
     // [Appearance("cAdministratorusername", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(25)] 
     public  string username
     { 
       get { return _username; } 
       set { SetPropertyValue(nameof(username), ref _username, value); } 
     } 
     // 
     // Notes for cAdministrator : 
     private string _password; 
     [XafDisplayName("Administrator's password"), ToolTip("Administrator's password")] 
     // [Appearance("cAdministratorpassword", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(25)] 
     public  string password
     { 
       get { return _password; } 
       set { SetPropertyValue(nameof(password), ref _password, value); } 
     } 
   } 
} 
